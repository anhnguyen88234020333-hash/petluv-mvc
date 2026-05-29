using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetLuv.Data;
using PetLuv.Models;
using Microsoft.AspNetCore.Authorization;

namespace PetLuv.Controllers
{
    public class CartController : Controller
    {
        private readonly PetLuvDbContext _context;

        public CartController(PetLuvDbContext context)
        {
            _context = context;
        }
        // TÍCH HỢP LUỒNG CỔNG THANH TOÁN VNPAY TEST
 
        public IActionResult PaymentViaVnPay(decimal totalAmount, int orderId)
        {
            string vnp_Returnurl = Url.Action("VnPayCallback", "Cart", null, Request.Scheme)!;
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_TmnCode = "97251525"; // Mã định danh Test mặc định
            string vnp_HashSecret = "CNXMMZSVZMKXFLWXMLXWNXMXWZXMXWZX"; // Chuỗi khóa bảo mật Test

            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((long)(totalAmount * 100)).ToString()); // Số tiền nhân 100 theo quy định VNPAY
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang PetLuv #{orderId}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", orderId.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return Redirect(paymentUrl);
        }

        public IActionResult VnPayCallback()
        {
            string vnp_HashSecret = "CNXMMZSVZMKXFLWXMLXWNXMXWZXMXWZX";
            var vnpayData = Request.Query;
            var vnpay = new VnPayLibrary();

            foreach (var key in vnpayData.Keys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, vnpayData[key]!);
                }
            }

            string inputHash = Request.Query["vnp_SecureHash"]!;
            bool checkSignature = vnpay.ValidateSignature(inputHash, vnp_HashSecret);

            if (checkSignature)
            {
                string vnp_ResponseCode = Request.Query["vnp_ResponseCode"]!;
                string orderId = Request.Query["vnp_TxnRef"]!;

                if (vnp_ResponseCode == "00")
                {
                    // >>> THANH TOÁN THÀNH CÔNG GIAO DỊCH VNPAY <<<
                    ViewBag.Message = $"Thanh toán thành công đơn hàng #{orderId} qua VNPAY rồi nè Boss! 🎉";
                }
                else
                {
                    // >>> THANH TOÁN THẤT BẠI HOẶC BOSS HỦY THAO TÁC <<<
                    ViewBag.Message = $"Giao dịch đơn hàng #{orderId} không thành công hoặc đã bị hủy rồi bồ ơi.";
                }
            }
            else
            {
                ViewBag.Message = "Có lỗi xảy ra trong quá trình kiểm tra chữ ký bảo mật VNPAY.";
            }

            return View();
        }

        // Hiển thị trang Giỏ hàng
        public async Task<IActionResult> Index()
        {
            int currentUserId = 1;

            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == currentUserId)
                .ToListAsync();

            return View(cartItems);
        }

        // Tiếp nhận yêu cầu "Thêm vào giỏ"
        public async Task<IActionResult> AddToCart(int productId)
        {
            int currentUserId = 1;

            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == currentUserId && c.ProductId == productId);

            if (cartItem == null)
            {
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
                cartItem.Quantity += 1;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Cập nhật số lượng sản phẩm trong giỏ
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

        // Xóa sản phẩm khỏi giỏ hàng
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

        // Hiển thị giao diện Nhập địa chỉ & Chọn Phương thức nhận hàng
        public async Task<IActionResult> Checkout()
        {
            int currentUserId = 1;

            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == currentUserId)
                .ToListAsync();

            if (cartItems.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string CustomerName, string Phone, string Address, string PaymentMethod)
        {
            int currentUserId = 1;

            var cartItems = await _context.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == currentUserId)
                .ToListAsync();

            if (cartItems.Count > 0)
            {
                // Bước A: Tính tổng tiền trước để xài chung cho COD hoặc VNPAY
                decimal totalAmount = cartItems.Sum(c => (c.Product.Price * c.Quantity));

                // Bước B: Tạo Đơn hàng mới lưu xuống DB
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    CustomerName = CustomerName,
                    Address = Address,
                    Phone = Phone,
                    TotalAmount = totalAmount,
                    Status = 0 // Đơn hàng mới tạo ở trạng thái Chờ xử lý
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync(); // Lưu dữ liệu xuống SQL để tạo tự động OrderID

                // Bước C: Lưu chi tiết danh sách sản phẩm mua vào bảng phụ OrderDetail
                foreach (var item in cartItems)
                {
                    var detail = new OrderDetail
                    {
                        OrderID = order.OrderID,
                        ProductID = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    };
                    _context.OrderDetails.Add(detail);
                }

                // Bước D: Tiến hành làm sạch giỏ hàng của khách hàng sau khi gom đơn thành công
                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                if (PaymentMethod == "VNPAY")
                {
                    // Đá luồng chuyển hướng trực tiếp qua ngân hàng Sandbox quẹt thẻ
                    return RedirectToAction("PaymentViaVnPay", "Cart", new { totalAmount = totalAmount, orderId = order.OrderID });
                }

                // Luồng COD mặc định gốc: Đưa về trang thông báo cảm ơn
                return View("OrderSuccess", (object)CustomerName);
            }

            return RedirectToAction(nameof(Index));
        }

        // Xem danh sách đơn hàng cá nhân đã mua
        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            int currentUserId = 1;

            var orders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // Xem chi tiết các món trong một đơn hàng cụ thể
        [Authorize]
        public async Task<IActionResult> OrderDetail(int id)
        {
            var details = await _context.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.OrderID == id)
                .ToListAsync();

            ViewBag.OrderId = id;
            return View(details);
        }
    }
}