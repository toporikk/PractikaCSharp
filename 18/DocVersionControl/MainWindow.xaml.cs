using System.Windows;
using System.Windows.Media.Animation;
using DocVersionControl.Models;
using DocVersionControl.Data;
using Microsoft.EntityFrameworkCore;

namespace DocVersionControl
{
    public partial class MainWindow : Window
    {
        private string _currentUser;
        private Services.InterprocessService _interprocessService;
        private AppDbContext _context;
        private List<Document> _documents = new();
        private Document? _selectedDocument;
        private List<string> _notifications = new();

        public MainWindow(string username)
        {
            InitializeComponent();
            _currentUser = username;
            _interprocessService = new Services.InterprocessService();
            _context = new AppDbContext();

            _interprocessService.NotificationReceived += OnNotificationReceived;
            _interprocessService.StartNotificationListener();

            LoadDocuments();
            lblStatus.Text = $"✅ Вошел: {_currentUser} | {DateTime.Now}";
        }

        private void OnNotificationReceived(string docName, string notification)
        {
            Dispatcher.Invoke(() =>
            {
                _notifications.Insert(0, notification);
                if (_notifications.Count > 10) _notifications.RemoveAt(10);
                lstNotifications.ItemsSource = null;
                lstNotifications.ItemsSource = _notifications;
            });
        }

        private async void LoadDocuments()
        {
            _documents = await _context.Documents
                .Where(d => d.OwnerUsername == _currentUser || d.CreatedBy == _currentUser)
                .Include(d => d.Versions)
                .ToListAsync();

            lstDocuments.ItemsSource = null;
            lstDocuments.ItemsSource = _documents;

            if (_documents.Any())
                lstDocuments.SelectedIndex = 0;
            else
            {
                _selectedDocument = null;
                lblDocTitle.Text = "Нет документов";
                lstVersions.ItemsSource = null;
            }
        }

        private void lstDocuments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedDocument = lstDocuments.SelectedItem as Document;

            if (_selectedDocument != null)
            {
                lblDocTitle.Text = $"📄 {_selectedDocument.Name}";
                lstVersions.ItemsSource = null;
                lstVersions.ItemsSource = _selectedDocument.Versions.OrderByDescending(v => v.CreatedDate).ToList();
            }
            else
            {
                lblDocTitle.Text = "Выберите документ";
                lstVersions.ItemsSource = null;
            }
        }

        private void lstVersions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private async void btnAddDoc_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddDocumentWindow();
            if (addWindow.ShowDialog() == true)
            {
                var newDoc = new Document
                {
                    Name = addWindow.DocumentName,
                    CreatedBy = _currentUser,
                    OwnerUsername = _currentUser,
                    CreatedDate = DateTime.Now
                };
                await _context.Documents.AddAsync(newDoc);
                await _context.SaveChangesAsync();

                _interprocessService.SendNotification(addWindow.DocumentName, "создание", _currentUser);
                LoadDocuments();
                MessageBox.Show($"Документ '{addWindow.DocumentName}' создан!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void btnAddVersion_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDocument == null)
            {
                MessageBox.Show("Сначала выберите документ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var addVersionWindow = new AddVersionWindow();
            if (addVersionWindow.ShowDialog() == true)
            {
                var newVersion = new DocumentVersion
                {
                    DocumentId = _selectedDocument.Id,
                    VersionNumber = addVersionWindow.VersionNumber,
                    Description = addVersionWindow.Description,
                    CreatedBy = _currentUser,
                    CreatedDate = DateTime.Now,
                    FilePath = ""
                };
                await _context.DocumentVersions.AddAsync(newVersion);
                await _context.SaveChangesAsync();

                _selectedDocument = await _context.Documents.Include(d => d.Versions).FirstOrDefaultAsync(d => d.Id == _selectedDocument.Id);
                lstVersions.ItemsSource = null;
                lstVersions.ItemsSource = _selectedDocument?.Versions.OrderByDescending(v => v.CreatedDate).ToList();

                _interprocessService.SendNotification(_selectedDocument.Name, "новая версия", _currentUser);
                MessageBox.Show($"Версия {addVersionWindow.VersionNumber} добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void btnDeleteDoc_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDocument == null) return;

            var result = MessageBox.Show($"Удалить '{_selectedDocument.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string docName = _selectedDocument.Name;
                _context.Documents.Remove(_selectedDocument);
                await _context.SaveChangesAsync();

                _interprocessService.SendNotification(docName, "удаление", _currentUser);
                LoadDocuments();
                MessageBox.Show("Документ удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDocument == null)
            {
                MessageBox.Show("Выберите документ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var chatWindow = new ChatWindow(_selectedDocument.Id, _selectedDocument.Name, _currentUser, _interprocessService);
            chatWindow.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadDocuments();
        }

        protected override void OnClosed(EventArgs e)
        {
            _interprocessService.Dispose();
            _context.Dispose();
            base.OnClosed(e);
        }
    }
}