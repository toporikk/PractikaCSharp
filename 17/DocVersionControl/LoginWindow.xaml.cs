using System.Windows;
using DocVersionControl.Services;

namespace DocVersionControl;

public partial class LoginWindow : Window
{
    private UserService _userService;

    public LoginWindow()
    {
        InitializeComponent();
        _userService = new UserService();
    }

    private void btnLogin_Click(object sender, RoutedEventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Password;

        if (_userService.Login(username, password))
        {
            var mainWindow = new MainWindow(_userService);
            mainWindow.Show();
            Close();
        }
        else
        {
            lblError.Text = "❌ Неверный логин или пароль!";
        }
    }

    private void btnRegister_Click(object sender, RoutedEventArgs e)
    {
        var registerWindow = new RegisterWindow();
        registerWindow.ShowDialog();
    }
}