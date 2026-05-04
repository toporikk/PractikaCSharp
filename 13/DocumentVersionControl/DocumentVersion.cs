using System;

namespace DocumentVersionControl
{
    public class DocumentVersion
    {
        public int Id { get; set; }
        public string VersionNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
    }
}