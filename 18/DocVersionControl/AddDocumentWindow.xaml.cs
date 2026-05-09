using System.Windows;

namespace DocVersionControl;

public partial class AddDocumentWindow : Window
{
    public string DocumentName { get; private set; } = "";

    public AddDocumentWindow()
    {
        InitializeComponent();
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Введите название документа!", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        DocumentName = txtName.Text.Trim();
        DialogResult = true;
        Close();
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}