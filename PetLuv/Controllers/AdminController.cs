using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Thư viện để dùng [Authorize]

namespace PetLuv.Controllers
{
    // PHẦN CỦA PHỤNG: Chỉ những ai có Role là "Admin" trong DB mới được vào đây
    [Authorize(Roles = "Admin")] 
    public class AdminController : Controller
    {
        // Trang chủ của Admin (Dashboard)
        public IActionResult Index()
        {
            return View();
        }

        // Bạn có thể thêm các trang khác như Quản lý sản phẩm ở đây
    }
}