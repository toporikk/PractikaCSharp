using System;
using System.Collections.Generic;

namespace DocVersionControl.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "";
        public string OwnerUsername { get; set; } = "";
        public List<DocumentVersion> Versions { get; set; } = new List<DocumentVersion>();
    }
}