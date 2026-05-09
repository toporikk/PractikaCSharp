using System.IO;
using Newtonsoft.Json;
using DocVersionControl.Models;

namespace DocVersionControl.Services;

public class DocumentService
{
    private readonly string _filePath;
    private readonly string _documentsFolder;
    private List<Document> _documents = new();

    public DocumentService()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(appDirectory, "documents.json");
        _documentsFolder = Path.Combine(appDirectory, "Documents");

        if (!Directory.Exists(_documentsFolder))
            Directory.CreateDirectory(_documentsFolder);

        Console.WriteLine($"Файл документов: {_filePath}");
        LoadDocuments();
    }

    private void LoadDocuments()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _documents = JsonConvert.DeserializeObject<List<Document>>(json) ?? new();
                Console.WriteLine($"Загружено {_documents.Count} документов");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            _documents = new List<Document>();
        }
    }

    private void SaveDocuments()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_documents, Formatting.Indented);
            File.WriteAllText(_filePath, json);
            Console.WriteLine($"Сохранено {_documents.Count} документов");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }

    public List<Document> GetDocuments() => _documents;

    public List<Document> GetUserDocuments(string username) =>
        _documents.Where(d => d.OwnerUsername == username || d.CreatedBy == username).ToList();

    public void AddDocument(string name, string createdBy)
    {
        try
        {
            int newId = _documents.Count > 0 ? _documents.Max(d => d.Id) + 1 : 1;
            var doc = new Document
            {
                Id = newId,
                Name = name,
                CreatedBy = createdBy,
                OwnerUsername = createdBy,
                CreatedDate = DateTime.Now
            };
            _documents.Add(doc);

            // Создаем файл документа
            string docPath = Path.Combine(_documentsFolder, $"doc_{newId}.txt");
            File.WriteAllText(docPath, $"Документ: {name}\nСоздан: {DateTime.Now}\nАвтор: {createdBy}\n\nСодержание:\n");

            SaveDocuments();
            Console.WriteLine($"Документ добавлен: {name} (ID: {newId})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления: {ex.Message}");
        }
    }

    public void AddVersion(int documentId, string versionNumber, string description, string createdBy, string content)
    {
        try
        {
            var doc = _documents.FirstOrDefault(d => d.Id == documentId);
            if (doc == null) return;

            int newId = doc.Versions.Count > 0 ? doc.Versions.Max(v => v.Id) + 1 : 1;

            // Сохраняем файл версии
            string versionPath = Path.Combine(_documentsFolder, $"doc_{documentId}_v{versionNumber.Replace("/", "_")}.txt");
            File.WriteAllText(versionPath, content);

            doc.Versions.Add(new DocumentVersion
            {
                Id = newId,
                DocumentId = documentId,
                VersionNumber = versionNumber,
                Description = description,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                FilePath = versionPath
            });
            SaveDocuments();
            Console.WriteLine($"Версия добавлена: {versionNumber} для документа {documentId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка добавления версии: {ex.Message}");
        }
    }

    public Document? GetDocument(int id) => _documents.FirstOrDefault(d => d.Id == id);

    public void DeleteDocument(int documentId)
    {
        try
        {
            var doc = _documents.FirstOrDefault(d => d.Id == documentId);
            if (doc != null)
            {
                // Удаляем файлы документа
                string docPath = Path.Combine(_documentsFolder, $"doc_{documentId}.txt");
                if (File.Exists(docPath)) File.Delete(docPath);

                foreach (var version in doc.Versions)
                {
                    if (File.Exists(version.FilePath)) File.Delete(version.FilePath);
                }

                _documents.Remove(doc);
                SaveDocuments();
                Console.WriteLine($"Документ удален: ID {documentId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления: {ex.Message}");
        }
    }
}