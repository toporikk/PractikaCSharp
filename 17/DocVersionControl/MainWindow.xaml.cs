using DocVersionControl.Models;
using DocVersionControl.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DocVersionControl;

public partial class MainWindow : Window
{
    private UserService _userService;
    private DocumentService _documentService;
    private InterprocessService _interprocessService;
    private Document? _selectedDocument;
    private List<string> _notifications = new();
    private CancellationTokenSource? _cts;

    public MainWindow(UserService userService)
    {
        InitializeComponent();
        _userService = userService;
        _documentService = new DocumentService();
        _interprocessService = new InterprocessService();
        _cts = new CancellationTokenSource();

        lstVersions.SelectionChanged += lstVersions_SelectionChanged;
        // Подписываемся на события
        _interprocessService.NotificationReceived += OnNotificationReceived;

        // Запускаем прослушивание уведомлений
        _interprocessService.StartNotificationListener();

        LoadDocuments();

        lblStatus.Text = $"✅ Вошел: {_userService.GetCurrentUserName()} | {DateTime.Now}";
    }

    private void LoadDocuments()
    {
        try
        {
            var docs = _documentService.GetUserDocuments(_userService.GetCurrentUserName());
            lstDocuments.ItemsSource = docs;

            if (docs.Any())
            {
                lstDocuments.SelectedIndex = 0;
            }
            else
            {
                _selectedDocument = null;
                lblDocTitle.Text = "Нет документов";
                lstVersions.ItemsSource = null;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки документов: {ex.Message}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnNotificationReceived(string docName, string notification)
    {
        Dispatcher.Invoke(() =>
        {
            _notifications.Insert(0, notification);
            if (_notifications.Count > 10)
                _notifications.RemoveAt(10);
            lstNotifications.ItemsSource = null;
            lstNotifications.ItemsSource = _notifications;
        });
    }

    private void lstDocuments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        _selectedDocument = lstDocuments.SelectedItem as Document;

        if (_selectedDocument != null)
        {
            lblDocTitle.Text = $"📄 {_selectedDocument.Name}";
            lstVersions.ItemsSource = _selectedDocument.Versions;
        }
        else
        {
            lblDocTitle.Text = "Выберите документ";
            lstVersions.ItemsSource = null;
        }
    }

    private void lstVersions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (lstVersions.SelectedItem is DocumentVersion selectedVersion)
        {
            // Находим Border выбранного элемента и запускаем анимацию подсветки
            var listBoxItem = lstVersions.ItemContainerGenerator.ContainerFromItem(selectedVersion) as System.Windows.Controls.ListBoxItem;
            if (listBoxItem != null)
            {
                var border = FindVisualChild<Border>(listBoxItem, "VersionBorder");
                if (border != null)
                {
                    var highlightStoryboard = this.Resources["HighlightVersion"] as System.Windows.Media.Animation.Storyboard;
                    if (highlightStoryboard != null)
                    {
                        Storyboard.SetTarget(highlightStoryboard, border);
                        highlightStoryboard.Begin();
                    }
                }
            }
        }
    }

    // Вспомогательный метод для поиска визуального элемента по имени
    private T? FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T tChild && tChild.Name == name)
                return tChild;

            var result = FindVisualChild<T>(child, name);
            if (result != null)
                return result;
        }
        return null;
    }

    private void btnAddDoc_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var addWindow = new AddDocumentWindow();
            if (addWindow.ShowDialog() == true)
            {
                _documentService.AddDocument(addWindow.DocumentName, _userService.GetCurrentUserName());
                LoadDocuments();
                _interprocessService.SendNotification(addWindow.DocumentName, "создание", _userService.GetCurrentUserName());
                MessageBox.Show($"Документ '{addWindow.DocumentName}' создан!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void btnAddVersion_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedDocument == null)
        {
            MessageBox.Show("Сначала выберите документ, к которому хотите добавить версию!",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var addVersionWindow = new AddVersionWindow();
            if (addVersionWindow.ShowDialog() == true)
            {
                _documentService.AddVersion(
                    _selectedDocument.Id,
                    addVersionWindow.VersionNumber,
                    addVersionWindow.Description,
                    _userService.GetCurrentUserName(),
                    $"Версия {addVersionWindow.VersionNumber}: {addVersionWindow.Description}\nСоздано: {DateTime.Now}\nАвтор: {_userService.GetCurrentUserName()}"
                );

                LoadDocuments();

                // Восстанавливаем выбор документа
                var updatedDoc = _documentService.GetDocument(_selectedDocument.Id);
                if (updatedDoc != null)
                {
                    _selectedDocument = updatedDoc;
                    lstDocuments.SelectedItem = _selectedDocument;
                }

                _interprocessService.SendNotification(_selectedDocument.Name, "новая версия",
                    _userService.GetCurrentUserName());

                MessageBox.Show($"Версия {addVersionWindow.VersionNumber} успешно добавлена!",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении версии: {ex.Message}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void btnDeleteDoc_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedDocument == null)
        {
            MessageBox.Show("Сначала выберите документ для удаления!",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var result = MessageBox.Show($"Удалить документ '{_selectedDocument.Name}' и все его версии?",
            "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                string docName = _selectedDocument.Name;
                _documentService.DeleteDocument(_selectedDocument.Id);
                LoadDocuments();

                // Очищаем выбранный документ
                _selectedDocument = null;
                lblDocTitle.Text = "Выберите документ";
                lstVersions.ItemsSource = null;

                _interprocessService.SendNotification(docName, "удаление", _userService.GetCurrentUserName());

                MessageBox.Show("Документ успешно удален!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void btnChat_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedDocument == null)
        {
            MessageBox.Show("Сначала выберите документ для открытия чата!",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var chatWindow = new ChatWindow(_selectedDocument.Id, _selectedDocument.Name,
                _userService.GetCurrentUserName(), _interprocessService);
            chatWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при открытии чата: {ex.Message}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void btnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadDocuments();
    }

    protected override void OnClosed(EventArgs e)
    {
        _cts?.Cancel();
        _interprocessService.Dispose();
        base.OnClosed(e);
    }
}