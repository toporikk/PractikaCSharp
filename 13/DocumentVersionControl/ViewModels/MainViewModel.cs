using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DocumentVersionControl.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<DocumentVersion> Versions { get; set; }
        public ICommand AddVersionCommand { get; set; }
        public ICommand ViewHistoryCommand { get; set; }
        public ICommand DeleteVersionCommand { get; set; }

        private DocumentVersion _selectedVersion;
        public DocumentVersion SelectedVersion
        {
            get { return _selectedVersion; }
            set
            {
                _selectedVersion = value;
            }
        }

        public MainViewModel()
        {
            Versions = new ObservableCollection<DocumentVersion>();

            Versions.Add(new DocumentVersion
            {
                Id = 1,
                VersionNumber = "v1.0",
                CreatedDate = DateTime.Now,
                Description = "Первая версия",
                FilePath = "doc_v1.pdf"
            });

            AddVersionCommand = new RelayCommand(AddVersion);
            ViewHistoryCommand = new RelayCommand(ViewHistory);
            DeleteVersionCommand = new RelayCommand(DeleteVersion, CanDeleteVersion);
        }

        private void AddVersion(object parameter)
        {
            var addWindow = new AddVersionWindow();
            addWindow.Owner = Application.Current.MainWindow;

            bool? result = addWindow.ShowDialog();

            if (result == true && addWindow.IsConfirmed)
            {
                int newId = Versions.Count + 1;

                Versions.Add(new DocumentVersion
                {
                    Id = newId,
                    VersionNumber = addWindow.VersionNumber,
                    CreatedDate = DateTime.Now,
                    Description = addWindow.Description,
                    FilePath = addWindow.FilePath
                });

                MessageBox.Show($"Версия {addWindow.VersionNumber} успешно добавлена!",
                              "Успех",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);
            }
        }

        private void ViewHistory(object parameter)
        {
            MessageBox.Show($"Всего версий: {Versions.Count}", "История");
        }

        private bool CanDeleteVersion(object parameter)
        {
            return SelectedVersion != null;
        }

        private void DeleteVersion(object parameter)
        {
            if (SelectedVersion != null)
            {
                if (MessageBox.Show($"Удалить версию {SelectedVersion.VersionNumber}?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Versions.Remove(SelectedVersion);
                }
            }
        }
    }
}