using Microsoft.AspNetCore.Mvc;
using PetLuv.Data; // Chỗ này để kết nối với file Data của bạn
using PetLuv.Models;

namespace PetLuv.Controllers
{
    public class ProductController : Controller
    {
        private readonly PetLuvDbContext _context;

        public ProductController(PetLuvDbContext context)
        {
            _context = context;
        }

        // Đây là hàm sẽ hiển thị danh sách sản phẩm
        public IActionResult Index()
        {
            var data = _context.Products.ToList(); // Lấy hết sản phẩm từ SQL
            return View(data);
        }
    }
}