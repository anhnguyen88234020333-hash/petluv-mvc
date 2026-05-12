using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLuv.Data;
using PetLuv.Models; // Ngọc nhớ thêm dòng này để nó hiểu bảng Cart và Product

namespace PetLuv.Controllers
{
    public class CartController : Controller
    {
        private readonly PetLuvDbContext _context;

        public CartController(PetLuvDbContext context)
        {
            _context = context;
        }

        // 1. Hiển thị trang Giỏ hàng
        public async Task<IActionResult> Index()
        {
            int currentUserId = 1; // Giả lập User ID của Ngọc

            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == currentUserId)
                .ToListAsync();

            return View(cartItems);
        }

        // 2. HÀM QUAN TRỌNG: Tiếp nhận yêu cầu "Thêm vào giỏ" từ các nút bấm
        public async Task<IActionResult> AddToCart(int productId)
        {
            int currentUserId = 1;

            // Kiểm tra xem món này đã có trong giỏ chưa
            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == currentUserId && c.ProductId == productId);

            if (cartItem == null)
            {
                // Nếu chưa có: Tạo mới
                var newItem = new Cart
                {
                    UserId = currentUserId,
                    ProductId = productId,
                    Quantity = 1
                };
                _context.Carts.Add(newItem);
            }
            else
            {
                // Nếu có rồi: Tăng số lượng lên 1
                cartItem.Quantity += 1;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Thêm xong nhảy thẳng vào trang giỏ hàng luôn
        }

        // 3. Cập nhật số lượng
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

        // 4. Xóa sản phẩm khỏi giỏ
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
    }
}