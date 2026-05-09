using System.Windows;
using DocVersionControl.Services;

namespace DocVersionControl;

public partial class RegisterWindow : Window
{
    private UserService _userService;

    public RegisterWindow()
    {
        InitializeComponent();
        _userService = new UserService();
    }

    private void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Password;

        if (string.IsNullOrEmpty(username) || username.Length < 3)
        {
            lblError.Text = "❌ Логин должен быть не менее 3 символов!";
            return;
        }

        if (string.IsNullOrEmpty(password) || password.Length < 3)
        {
            lblError.Text = "❌ Пароль должен быть не менее 3 символов!";
            return;
        }

        if (_userService.Register(username, password))
        {
            MessageBox.Show($"✅ Пользователь {username} успешно зарегистрирован!",
                "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
        else
        {
            lblError.Text = "❌ Пользователь с таким логином уже существует!";
        }
    }
}