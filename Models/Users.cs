using System.ComponentModel.DataAnnotations;

namespace CRUD_API.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
