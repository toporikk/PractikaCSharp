using System.Windows;

namespace DocVersionControl;

public partial class ChatWindow : Window
{
    private int _documentId;
    private string _documentName;
    private string _currentUser;
    private Services.InterprocessService _interprocessService;
    private List<ChatMessageDisplay> _messages = new();

    public ChatWindow(int documentId, string documentName, string currentUser, Services.InterprocessService interprocessService)
    {
        InitializeComponent();
        _documentId = documentId;
        _documentName = documentName;
        _currentUser = currentUser;
        _interprocessService = interprocessService;

        lblDocName.Text = $"💬 Обсуждение: {_documentName}";

        // Подписываемся на события чата
        _interprocessService.ChatMessageReceived += OnNewMessage;

        // Запускаем сервер чата для этого документа
        _ = _interprocessService.StartChatServerAsync(_documentId, _currentUser);

        // Добавляем системное сообщение
        AddMessage("Система", $"Чат для документа '{_documentName}' открыт", DateTime.Now);
    }

    private void OnNewMessage(string sender, string content, DateTime timestamp)
    {
        Dispatcher.Invoke(() =>
        {
            AddMessage(sender, content, timestamp);
        });
    }

    private void AddMessage(string sender, string content, DateTime timestamp)
    {
        _messages.Add(new ChatMessageDisplay
        {
            Sender = sender,
            Content = content,
            Timestamp = timestamp.ToString("HH:mm:ss")
        });

        lstMessages.ItemsSource = null;
        lstMessages.ItemsSource = _messages;

        // Прокручиваем вниз
        if (lstMessages.Items.Count > 0)
            lstMessages.ScrollIntoView(lstMessages.Items[lstMessages.Items.Count - 1]);
    }

    private async void btnSend_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtMessage.Text))
            return;

        string message = txtMessage.Text.Trim();
        txtMessage.Clear();

        await _interprocessService.SendChatMessageAsync(_documentId, _currentUser, message);
        AddMessage(_currentUser, message, DateTime.Now);
    }

    protected override void OnClosed(EventArgs e)
    {
        _interprocessService.ChatMessageReceived -= OnNewMessage;
        base.OnClosed(e);
    }
}

public class ChatMessageDisplay
{
    public string Sender { get; set; } = "";
    public string Content { get; set; } = "";
    public string Timestamp { get; set; } = "";
}