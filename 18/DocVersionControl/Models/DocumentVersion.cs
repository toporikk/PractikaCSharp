using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocVersionControl.Models
{
    public class DocumentVersion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        public string VersionNumber { get; set; } = "";

        public string Description { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "";
        public string FilePath { get; set; } = "";

        [ForeignKey("DocumentId")]
        public Document? Document { get; set; }
    }
}