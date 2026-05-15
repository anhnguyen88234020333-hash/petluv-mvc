using System.ComponentModel.DataAnnotations;

namespace PetLuv.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        
        // Liên kết với bảng chi tiết
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}