using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLuv.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Cấu hình khóa ngoại liên kết tới bảng Users (class User)
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        // Cấu hình khóa ngoại liên kết tới bảng Products (class Product)
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}