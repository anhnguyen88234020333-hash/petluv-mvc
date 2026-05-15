using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetLuv.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}