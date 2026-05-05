using System;

namespace DocumentVersionControl.Models
{
    public class VersionModel
    {
        public int Id { get; set; }
        public string VersionNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string CreatedBy { get; set; }
    }
}