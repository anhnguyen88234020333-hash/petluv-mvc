using Microsoft.AspNetCore.Mvc;
using PetLuv.Data; // Chỗ này để kết nối với file Data
using PetLuv.Models;
using System.Linq; // Thêm thư viện này để dùng được câu lệnh tìm kiếm Where

namespace PetLuv.Controllers
{
    public class ProductController : Controller
    {
        private readonly PetLuvDbContext _context;

        public ProductController(PetLuvDbContext context)
        {
            _context = context;
        }

        // TÍCH HỢP TÌM KIẾM VÀ LỌC GIÁ CHO 16 SẢN PHẨM 
        public IActionResult Index(string searchTerm, string priceRange)
        {
            // 1. Lấy toàn bộ danh sách sản phẩm gốc từ SQL Server lên dưới dạng Queryable để chuẩn bị lọc
            var products = _context.Products.AsQueryable();

            // 2. Xử lý Lọc theo Tên sản phẩm (Search) nế
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.Contains(searchTerm));
                ViewBag.SearchTerm = searchTerm; // Giữ lại chữ đã gõ để nó không bị biến mất sau khi load trang
            }

            // 3. Xử lý Lọc theo Khoảng giá (Price Filter) khi bấm chọn option
            if (!string.IsNullOrEmpty(priceRange))
            {
                switch (priceRange)
                {
                    case "under100":
                        products = products.Where(p => p.Price < 100000);
                        break;
                    case "100to200":
                        products = products.Where(p => p.Price >= 100000 && p.Price <= 200000);
                        break;
                    case "over200":
                        products = products.Where(p => p.Price > 200000);
                        break;
                }
                ViewBag.SelectedPrice = priceRange; // Giữ lại trạng thái lựa chọn của bộ lọc
            }

            // 4. Chuyển kết quả cuối cùng thành danh sách (ToList) và truyền sang cho giao diện hiển thị
            var data = products.ToList();
            return View(data);
        }

        // Hàm hiển thị trang chi tiết 
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound(); // Nếu không tìm thấy thì báo lỗi 404
            }

            return View(product); // Nếu thấy thì mở trang Details.cshtml và truyền dữ liệu qua
        }
    }
}