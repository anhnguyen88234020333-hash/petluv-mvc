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

        // TÍCH HỢP TÌM KIẾM, LỌC GIÁ VÀ DANH MỤC CHO 32 SẢN PHẨM 
        
        public IActionResult Index(string searchTerm, string priceRange, string category)
        {
           
            var products = _context.Products.AsQueryable();

            //lọc theo Danh mục (Thức ăn chó, Thức ăn mèo, Phụ kiện)
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
                ViewBag.SelectedCategory = category; 
            }

            // 2. Xử lý Lọc theo Tên sản phẩm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.ProductName.Contains(searchTerm));
                ViewBag.SearchTerm = searchTerm; 
            }

            
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
                ViewBag.SelectedPrice = priceRange; 
            }

            
            var data = products.ToList();

            
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.ImageURL))
                {
                    item.ImageURL = item.ImageURL.Trim();
                }
            }

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