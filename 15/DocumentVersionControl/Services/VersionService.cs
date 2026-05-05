using DocumentVersionControl.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentVersionControl.Services
{
    public class VersionService
    {
        // Симуляция базы данных
        private ObservableCollection<VersionModel> _versions;

        public VersionService()
        {
            _versions = new ObservableCollection<VersionModel>();

            // Тестовые данные
            _versions.Add(new VersionModel
            {
                Id = 1,
                VersionNumber = "v1.0",
                CreatedDate = DateTime.Now.AddDays(-10),
                Description = "Первая версия документа",
                FilePath = "docs/v1.0.docx",
                FileSize = 1024000,
                CreatedBy = "Иванов И."
            });

            _versions.Add(new VersionModel
            {
                Id = 2,
                VersionNumber = "v1.1",
                CreatedDate = DateTime.Now.AddDays(-5),
                Description = "Исправление ошибок",
                FilePath = "docs/v1.1.docx",
                FileSize = 1056000,
                CreatedBy = "Иванов И."
            });
        }

        // Асинхронная загрузка версий
        public async Task<ObservableCollection<VersionModel>> GetVersionsAsync()
        {
            // Симуляция задержки сети/базы данных
            await Task.Delay(1500);
            return _versions;
        }

        // Асинхронное добавление версии
        public async Task<bool> AddVersionAsync(VersionModel newVersion)
        {
            await Task.Delay(500); // Симуляция сохранения

            newVersion.Id = _versions.Count + 1;
            newVersion.CreatedDate = DateTime.Now;
            _versions.Add(newVersion);

            return true;
        }

        // Асинхронное удаление версии
        public async Task<bool> DeleteVersionAsync(int versionId)
        {
            await Task.Delay(300);

            var versionToRemove = _versions.FirstOrDefault(v => v.Id == versionId);
            if (versionToRemove != null)
            {
                _versions.Remove(versionToRemove);
                return true;
            }
            return false;
        }

        // Получение истории (синхронный метод для привязки)
        public ObservableCollection<VersionModel> GetVersions()
        {
            return _versions;
        }
    }
}