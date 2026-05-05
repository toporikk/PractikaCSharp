using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DocumentVersionControl.Models;
using DocumentVersionControl.Services;

namespace DocumentVersionControl.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly VersionService _versionService;
        private ObservableCollection<VersionModel> _versions;
        private VersionModel _selectedVersion;
        private bool _isLoading;
        private string _loadingMessage;
        private string _statusText;

        public ObservableCollection<VersionModel> Versions
        {
            get => _versions;
            set
            {
                _versions = value;
                OnPropertyChanged();
                UpdateStatusText();
            }
        }

        public VersionModel SelectedVersion
        {
            get => _selectedVersion;
            set
            {
                _selectedVersion = value;
                OnPropertyChanged();
                UpdateStatusText();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string LoadingMessage
        {
            get => _loadingMessage;
            set
            {
                _loadingMessage = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddVersionCommand { get; set; }
        public ICommand ViewHistoryCommand { get; set; }
        public ICommand DeleteVersionCommand { get; set; }

        public MainViewModel()
        {
            _versionService = new VersionService();
            _versions = new ObservableCollection<VersionModel>();

            AddVersionCommand = new RelayCommand(_ => AddVersionAsync());
            ViewHistoryCommand = new RelayCommand(_ => ViewHistory());
            DeleteVersionCommand = new RelayCommand(_ => DeleteVersionAsync(), _ => SelectedVersion != null);

            // Загружаем данные
            Task.Run(async () => await LoadVersionsAsync());
        }

        public async Task LoadVersionsAsync()
        {
            IsLoading = true;
            LoadingMessage = "Загрузка списка версий...";

            try
            {
                var loadedVersions = await _versionService.GetVersionsAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Versions.Clear();
                    foreach (var version in loadedVersions)
                    {
                        Versions.Add(version);
                    }
                    UpdateStatusText();
                });
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    StatusText = "Ошибка загрузки";
                });
            }
            finally
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    IsLoading = false;
                    LoadingMessage = "";
                });
            }
        }

        private async void AddVersionAsync()
        {
            var addWindow = new AddVersionWindow();
            addWindow.Owner = Application.Current.MainWindow;

            if (addWindow.ShowDialog() == true && addWindow.IsConfirmed)
            {
                IsLoading = true;
                LoadingMessage = "Сохранение новой версии...";

                var newVersion = new VersionModel
                {
                    VersionNumber = addWindow.VersionNumber,
                    Description = addWindow.Description,
                    FilePath = addWindow.FilePath,
                    CreatedBy = "Текущий пользователь",
                    CreatedDate = DateTime.Now
                };

                bool success = await _versionService.AddVersionAsync(newVersion);

                if (success)
                {
                    await LoadVersionsAsync();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show($"Версия {addWindow.VersionNumber} успешно добавлена!",
                                      "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    });
                }

                IsLoading = false;
            }
        }

        private async void DeleteVersionAsync()
        {
            if (SelectedVersion == null) return;

            var result = MessageBox.Show($"Удалить версию {SelectedVersion.VersionNumber}?",
                                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                IsLoading = true;
                LoadingMessage = "Удаление версии...";

                bool success = await _versionService.DeleteVersionAsync(SelectedVersion.Id);

                if (success)
                {
                    await LoadVersionsAsync();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Версия удалена", "Успех",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    });
                }

                IsLoading = false;
            }
        }

        private void ViewHistory()
        {
            string history = "История версий:\n\n";
            foreach (var version in Versions)
            {
                history += $"• {version.VersionNumber} - {version.CreatedDate:dd.MM.yyyy} - {version.Description}\n";
            }
            MessageBox.Show(history, "История документа", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateStatusText()
        {
            if (SelectedVersion != null)
                StatusText = $"Выбрана версия: {SelectedVersion.VersionNumber}";
            else
                StatusText = $"Всего версий: {Versions.Count}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}