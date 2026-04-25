using System.ComponentModel.DataAnnotations;

namespace PetLuv.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string? ProductName { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? Description { get; set; }

        public string? ImageURL { get; set; }

        public string? Category { get; set; }
    }
}