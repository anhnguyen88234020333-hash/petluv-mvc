using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PetLuv.Data; // Để nhận diện PetLuvDbContext
using PetLuv.Models;

namespace PetLuv.Controllers
{
    // PHẦN CỦA PHỤNG: Chỉ những ai có Role là "Admin" trong DB mới được vào đây
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly PetLuvDbContext _context;

        public AdminController(PetLuvDbContext context)
        {
            _context = context;
        }

        // 1. Trang quản lý đơn hàng của Admin (Thay vì trang Dashboard trống)
        public async Task<IActionResult> Index()
        {
            // Lấy toàn bộ đơn hàng có trong hệ thống, xếp đơn mới nhất lên đầu
            var allOrders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(allOrders);
        }

        // 2. HÀM QUAN TRỌNG: Tiếp nhận yêu cầu đổi trạng thái từ Admin
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, int newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus; // Cập nhật trạng thái mới
                await _context.SaveChangesAsync(); // Lưu xuống SQL
            }

            // Đổi xong nhảy ngược lại trang quản lý đơn hàng liền
            return RedirectToAction(nameof(Index));
        }
        // 3. PHẦN ADMIN: Xem danh sách tài khoản người dùng trong hệ thống
        public async Task<IActionResult> UsersList()
        {
            // Lấy toàn bộ danh sách User dưới Database lên
            var allUsers = await _context.Users.ToListAsync();
            return View(allUsers);
        }
    }
}