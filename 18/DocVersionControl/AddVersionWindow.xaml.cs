using System.Windows;

namespace DocVersionControl;

public partial class AddVersionWindow : Window
{
    public string VersionNumber { get; private set; } = "";
    public string Description { get; private set; } = "";

    public AddVersionWindow()
    {
        InitializeComponent();
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNumber.Text))
        {
            MessageBox.Show("Введите номер версии!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        VersionNumber = txtNumber.Text.Trim();
        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ?
            "Нет описания" : txtDescription.Text;
        DialogResult = true;
        Close();
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}