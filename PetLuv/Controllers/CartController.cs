using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLuv.Data; // Đảm bảo đúng namespace Data của nhóm bạn nhé

namespace PetLuv.Controllers
{
    public class CartController : Controller
    {
        private readonly PetLuvDbContext _context;

        public CartController(PetLuvDbContext context)
        {
            _context = context;
        }

        // GET: /Cart
        public async Task<IActionResult> Index()
        {
            // Chúng ta lấy đúng UserId = 1 như trong SQL của bạn
            int currentUserId = 1;

            var cartItems = await _context.Carts
                .Include(c => c.Product) // Đưa thông tin sản phẩm vào
                .Where(c => c.UserId == currentUserId)
                .ToListAsync();

            return View(cartItems);
        }

        // ================= THÊM TỪ ĐÂY =================

        // Action: Cập nhật số lượng sản phẩm trong giỏ hàng
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Action: Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ================= HẾT ĐOẠN THÊM =================
    }
}