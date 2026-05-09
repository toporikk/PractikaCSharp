using System.Collections.ObjectModel;
using System.Windows.Input;
using DocVersionControl.Models;
using DocVersionControl.Repositories;
using DocVersionControl.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace DocVersionControl.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private readonly DocumentRepository _documentRepo;
        private readonly VersionRepository _versionRepo;

        public ObservableCollection<Document> Documents { get; set; } = new();
        public ObservableCollection<DocumentVersion> Versions { get; set; } = new();

        private Document? _selectedDocument;
        public Document? SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                _selectedDocument = value;
                OnPropertyChanged();
                System.Windows.Application.Current.Dispatcher.InvokeAsync(() => LoadVersionsForDocumentAsync());
            }
        }

        private DocumentVersion? _selectedVersion;
        public DocumentVersion? SelectedVersion
        {
            get => _selectedVersion;
            set
            {
                _selectedVersion = value;
                OnPropertyChanged();
            }
        }

        private string _currentUser = "";
        public string CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); }
        }

        public ICommand LoadDocumentsCommand { get; }
        public ICommand AddDocumentCommand { get; }
        public ICommand DeleteDocumentCommand { get; }
        public ICommand AddVersionCommand { get; }
        public ICommand DeleteVersionCommand { get; }

        public MainViewModel(AppDbContext context, string username)
        {
            _context = context;
            _documentRepo = new DocumentRepository(context);
            _versionRepo = new VersionRepository(context);
            CurrentUser = username;

            LoadDocumentsCommand = new Command(async () => await LoadDocumentsAsync());
            AddDocumentCommand = new Command(async () => await AddDocumentAsync());
            DeleteDocumentCommand = new Command(async () => await DeleteDocumentAsync(), () => SelectedDocument != null);
            AddVersionCommand = new Command(async () => await AddVersionAsync(), () => SelectedDocument != null);
            DeleteVersionCommand = new Command(async () => await DeleteVersionAsync(), () => SelectedVersion != null);

            System.Windows.Application.Current.Dispatcher.InvokeAsync(async () => await LoadDocumentsAsync());
        }

        public MainViewModel() : this(new AppDbContext(), "") { }

        private async Task LoadDocumentsAsync()
        {
            var docs = await _documentRepo.GetUserDocumentsAsync(CurrentUser);
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Documents.Clear();
                foreach (var doc in docs)
                    Documents.Add(doc);
            });
        }

        private async Task AddDocumentAsync()
        {
            var addWindow = new AddDocumentWindow();
            if (addWindow.ShowDialog() == true)
            {
                var newDoc = new Document
                {
                    Name = addWindow.DocumentName,
                    CreatedBy = CurrentUser,
                    OwnerUsername = CurrentUser,
                    CreatedDate = DateTime.Now
                };
                await _documentRepo.AddAsync(newDoc);
                await _documentRepo.SaveAsync();
                await LoadDocumentsAsync();
            }
        }

        private async Task DeleteDocumentAsync()
        {
            if (SelectedDocument == null) return;
            _documentRepo.Delete(SelectedDocument);
            await _documentRepo.SaveAsync();
            await LoadDocumentsAsync();
            SelectedDocument = null;
        }

        private async Task AddVersionAsync()
        {
            if (SelectedDocument == null) return;
            var addVersionWindow = new AddVersionWindow();
            if (addVersionWindow.ShowDialog() == true)
            {
                var newVersion = new DocumentVersion
                {
                    DocumentId = SelectedDocument.Id,
                    VersionNumber = addVersionWindow.VersionNumber,
                    Description = addVersionWindow.Description,
                    CreatedBy = CurrentUser,
                    CreatedDate = DateTime.Now,
                    FilePath = ""
                };
                await _versionRepo.AddAsync(newVersion);
                await _versionRepo.SaveAsync();
                await LoadVersionsForDocumentAsync();
            }
        }

        private async Task DeleteVersionAsync()
        {
            if (SelectedVersion == null) return;
            _versionRepo.Delete(SelectedVersion);
            await _versionRepo.SaveAsync();
            await LoadVersionsForDocumentAsync();
        }

        private async Task LoadVersionsForDocumentAsync()
        {
            if (SelectedDocument == null)
            {
                await System.Windows.Application.Current.Dispatcher.InvokeAsync(() => Versions.Clear());
                return;
            }
            var versions = await _versionRepo.GetDocumentVersionsAsync(SelectedDocument.Id);
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Versions.Clear();
                foreach (var v in versions)
                    Versions.Add(v);
            });
        }
    }

    public class BaseViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
    }

    public class Command : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;

        public Command(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute();
        public async void Execute(object? parameter) => await _execute();
        public event EventHandler? CanExecuteChanged;
    }
}