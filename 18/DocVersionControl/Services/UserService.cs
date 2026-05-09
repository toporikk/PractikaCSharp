using System.IO;
using Newtonsoft.Json;
using DocVersionControl.Models;

namespace DocVersionControl.Services;

public class UserService
{
    private readonly string _filePath;
    private List<User> _users = new();
    public User? CurrentUser { get; private set; }

    public UserService()
    {
        // Используем путь к папке приложения
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(appDirectory, "users.json");

        Console.WriteLine($"Файл пользователей: {_filePath}");
        LoadUsers();
    }

    private void LoadUsers()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _users = JsonConvert.DeserializeObject<List<User>>(json) ?? new();
                Console.WriteLine($"Загружено {_users.Count} пользователей");
            }

            if (_users.Count == 0)
            {
                _users = new List<User>
                {
                    new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
                    new User { Id = 2, Username = "user", Password = "user123", Role = "User" }
                };
                SaveUsers();
                Console.WriteLine("Созданы пользователи по умолчанию");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки пользователей: {ex.Message}");
            _users = new List<User>();
        }
    }

    private void SaveUsers()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_users, Formatting.Indented);
            File.WriteAllText(_filePath, json);
            Console.WriteLine($"Сохранено {_users.Count} пользователей в {_filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }

    public bool Register(string username, string password)
    {
        try
        {
            if (_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Пользователь {username} уже существует");
                return false;
            }

            int newId = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(new User
            {
                Id = newId,
                Username = username,
                Password = password,
                Role = "User",
                RegisteredDate = DateTime.Now
            });
            SaveUsers();
            Console.WriteLine($"Зарегистрирован новый пользователь: {username}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка регистрации: {ex.Message}");
            return false;
        }
    }

    public bool Login(string username, string password)
    {
        try
        {
            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            if (user != null)
            {
                CurrentUser = user;
                Console.WriteLine($"Пользователь {username} вошел в систему");
                return true;
            }

            Console.WriteLine($"Неудачная попытка входа: {username}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка входа: {ex.Message}");
            return false;
        }
    }

    public string GetCurrentUserName() => CurrentUser?.Username ?? "Гость";
    public bool IsAdmin() => CurrentUser?.Role == "Admin";
}