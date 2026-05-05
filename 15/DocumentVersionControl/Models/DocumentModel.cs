using System;
using System.Collections.ObjectModel;

namespace DocumentVersionControl.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public ObservableCollection<VersionModel> Versions { get; set; }

        public DocumentModel()
        {
            Versions = new ObservableCollection<VersionModel>();
        }
    }
}