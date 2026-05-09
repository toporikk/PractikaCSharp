using Newtonsoft.Json;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Text;

namespace DocVersionControl.Services;

public class InterprocessService : IDisposable
{
    private NamedPipeServerStream? _pipeServer;
    private MemoryMappedFile? _mmf;
    private FileSystemWatcher? _watcher;
    private bool _isDisposed = false;
    private CancellationTokenSource? _cts;
    private Task? _pipeTask;

    public event Action<string, string, DateTime>? ChatMessageReceived;
    public event Action<string, string>? NotificationReceived;
    public event Action<string, string>? FileChanged;

    private readonly string _basePipeName = "DocVersionControl_Chat";
    private readonly string _notificationMmfName = "DocVersionControl_Notifications";

    // ========== NAMED PIPES (Чат для документа) ==========

    public async Task StartChatServerAsync(int documentId, string currentUser)
    {
        string pipeName = $"{_basePipeName}_{documentId}";
        _cts = new CancellationTokenSource();

        _pipeTask = Task.Run(async () =>
        {
            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    using var server = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 10, PipeTransmissionMode.Message);

                    // Ожидаем подключения с таймаутом
                    using var timeoutCts = new CancellationTokenSource(5000);
                    using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, timeoutCts.Token);

                    try
                    {
                        await server.WaitForConnectionAsync(linkedCts.Token);
                        _ = Task.Run(() => HandlePipeConnection(server, currentUser, documentId));
                    }
                    catch (OperationCanceledException)
                    {
                        // Таймаут или отмена - продолжаем цикл
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Pipe server error: {ex.Message}");
                    await Task.Delay(1000, _cts.Token);
                }
            }
        });
    }

    private async Task HandlePipeConnection(NamedPipeServerStream pipe, string currentUser, int documentId)
    {
        try
        {
            using var reader = new StreamReader(pipe);
            using var writer = new StreamWriter(pipe) { AutoFlush = true };

            // Отправляем приветствие
            var welcomeMsg = new ChatMessage
            {
                Sender = "Система",
                Content = $"{currentUser} присоединился к чату",
                Timestamp = DateTime.Now
            };
            await writer.WriteLineAsync(JsonConvert.SerializeObject(welcomeMsg));

            while (pipe.IsConnected && !_cts?.Token.IsCancellationRequested == true)
            {
                try
                {
                    if (reader.EndOfStream) break;

                    string? messageJson = await reader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(messageJson))
                    {
                        var message = JsonConvert.DeserializeObject<ChatMessage>(messageJson);
                        if (message != null)
                        {
                            ChatMessageReceived?.Invoke(message.Sender, message.Content, message.Timestamp);
                        }
                    }
                }
                catch (IOException)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handle connection error: {ex.Message}");
        }
    }

    public async Task SendChatMessageAsync(int documentId, string sender, string content)
    {
        string pipeName = $"{_basePipeName}_{documentId}";

        try
        {
            using var client = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
            using var timeoutCts = new CancellationTokenSource(2000);

            await client.ConnectAsync(timeoutCts.Token);

            using var writer = new StreamWriter(client) { AutoFlush = true };
            var message = new ChatMessage
            {
                Sender = sender,
                Content = content,
                Timestamp = DateTime.Now
            };
            await writer.WriteLineAsync(JsonConvert.SerializeObject(message));
        }
        catch (OperationCanceledException)
        {
            // Чат-сервер не запущен - игнорируем
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Send message error: {ex.Message}");
        }
    }

    // ========== MEMORY-MAPPED FILES (Уведомления) - исправлено для .NET 10 ==========

    public void SendNotification(string documentName, string action, string user)
    {
        try
        {
            // Создаем MemoryMappedFile
            _mmf = MemoryMappedFile.CreateOrOpen(_notificationMmfName, 4096);
            using var accessor = _mmf.CreateViewAccessor();

            string notification = $"{DateTime.Now:HH:mm:ss} - {user}: {action} документа '{documentName}'";
            byte[] bytes = Encoding.UTF8.GetBytes(notification);

            // Записываем длину сообщения и само сообщение
            accessor.Write(0, bytes.Length);
            accessor.WriteArray(4, bytes, 0, bytes.Length);

            NotificationReceived?.Invoke(documentName, notification);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MMF send error: {ex.Message}");
        }
    }

    public void StartNotificationListener()
    {
        Task.Run(() =>
        {
            while (!_isDisposed)
            {
                try
                {
                    // Пытаемся открыть существующий MemoryMappedFile
                    MemoryMappedFile? existingMmf = null;
                    try
                    {
                        existingMmf = MemoryMappedFile.OpenExisting(_notificationMmfName);
                    }
                    catch (FileNotFoundException)
                    {
                        // Файл еще не создан
                    }

                    if (existingMmf != null)
                    {
                        using (existingMmf)
                        using (var accessor = existingMmf.CreateViewAccessor())
                        {
                            // Читаем длину сообщения
                            int length = accessor.ReadInt32(0);
                            if (length > 0 && length < 4096)
                            {
                                byte[] bytes = new byte[length];
                                accessor.ReadArray(4, bytes, 0, length);
                                string notification = Encoding.UTF8.GetString(bytes);

                                if (!string.IsNullOrEmpty(notification))
                                {
                                    NotificationReceived?.Invoke("", notification);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"MMF listen error: {ex.Message}");
                }

                Thread.Sleep(1000);
            }
        });
    }

    // ========== FILE SYSTEM WATCHER ==========

    public void StartFileWatcher(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _watcher = new FileSystemWatcher();
            _watcher.Path = path;
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size;
            _watcher.Filter = "*.txt";
            _watcher.IncludeSubdirectories = true;

            _watcher.Changed += OnFileChanged;
            _watcher.Created += OnFileChanged;
            _watcher.Deleted += OnFileChanged;
            _watcher.Renamed += OnFileRenamed;

            _watcher.EnableRaisingEvents = true;
            Console.WriteLine($"FileWatcher запущен для: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FileWatcher error: {ex.Message}");
        }
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        try
        {
            FileChanged?.Invoke(e.Name ?? "unknown", $"Файл {e.ChangeType}: {e.Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"OnFileChanged error: {ex.Message}");
        }
    }

    private void OnFileRenamed(object sender, RenamedEventArgs e)
    {
        try
        {
            FileChanged?.Invoke(e.Name ?? "unknown", $"Файл переименован: {e.OldName} -> {e.Name}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"OnFileRenamed error: {ex.Message}");
        }
    }

    public void StopFileWatcher()
    {
        if (_watcher != null)
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
            _watcher = null;
        }
    }

    public async Task StopChatServerAsync()
    {
        _cts?.Cancel();
        if (_pipeTask != null)
        {
            try
            {
                await _pipeTask;
            }
            catch (OperationCanceledException) { }
        }
        _pipeServer?.Dispose();
    }

    public void Dispose()
    {
        _isDisposed = true;
        _cts?.Cancel();
        _cts?.Dispose();
        _pipeServer?.Dispose();
        _mmf?.Dispose();
        StopFileWatcher();
    }
}

public class ChatMessage
{
    public string Sender { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime Timestamp { get; set; }
}