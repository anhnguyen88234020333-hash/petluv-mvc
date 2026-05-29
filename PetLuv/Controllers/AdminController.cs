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

        
        

        // 3. PHẦN ADMIN: Xem danh sách tài khoản người dùng trong hệ thống
        public async Task<IActionResult> UsersList()
        {
            // Lấy toàn bộ danh sách User dưới Database lên
            var allUsers = await _context.Users.ToListAsync();
            return View(allUsers);
        }

        // 4. TRANG DANH SÁCH SẢN PHẨM (ADMIN)
        public async Task<IActionResult> ProductsList()
        {
            // Bỏ .Include vì Model Product không cấu hình thuộc tính liên kết Category
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // 5. GIAO DIỆN THÊM MỚI SẢN PHẨM (GET)
        public IActionResult CreateProduct()
        {
            return View();
        }

        // 6. XỬ LÝ LƯU THÊM MỚI SẢN PHẨM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProductsList));
            }
            return View(product);
        }

        // 7. GIAO DIỆN CẬP NHẬT/SỬA SẢN PHẨM (GET)
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // 8. XỬ LÝ LƯU CẬP NHẬT SẢN PHẨM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.ProductID == product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ProductsList));
            }
            return View(product);
        }

        // 9. XỬ LÝ XÓA SẢN PHẨM (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ProductsList));
        }

        // 1. Trang liệt kê danh sách đơn hàng, tìm kiếm và lọc cho Admin
        public async Task<IActionResult> AdminOrder(string searchString, int? statusFilter)
        {
            var orders = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(o => o.CustomerName.Contains(searchString) || o.Phone.Contains(searchString));
            }

            if (statusFilter.HasValue)
            {
                orders = orders.Where(o => o.Status == statusFilter.Value);
            }

            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentFilter = statusFilter;

            var result = await orders.OrderByDescending(o => o.OrderDate).ToListAsync();
            return View(result);
        }

        // 2. Trang xem chi tiết từng đơn hàng phía Admin
        public async Task<IActionResult> AdminOrderDetail(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            var details = await _context.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.OrderID == id)
                .ToListAsync();

            ViewBag.Order = order;
            return View(details);
        }

        // 3. Hàm xử lý cập nhật trạng thái khi Admin chọn duyệt/hủy đơn
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, int newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AdminOrderDetail", new { id = orderId });
        }
    }
}