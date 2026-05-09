using System;

namespace DocVersionControl.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "User";
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
    }
}