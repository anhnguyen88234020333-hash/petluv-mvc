using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLuv.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("UserID")] 
        public int Id { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Role { get; set; } 
    }
}