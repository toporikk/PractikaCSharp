using System.ComponentModel.DataAnnotations;

namespace DocVersionControl.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        public string Role { get; set; } = "User";
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
    }
}