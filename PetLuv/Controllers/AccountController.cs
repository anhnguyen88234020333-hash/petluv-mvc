using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication; // Thêm dòng này để làm Login/Logout
using PetLuv.Data; // Để dùng được database
using PetLuv.Models; // Để dùng được mấy cái ViewModel
using System.Security.Claims; // Để dùng cho Cookie đăng nhập

namespace PetLuv.Controllers
{
    public class AccountController : Controller
    {
        private readonly PetLuvDbContext _context;

        // lấy Database vào Controller
        public AccountController(PetLuvDbContext context)
        {
            _context = context;
        }

        // --- PHẦN ĐĂNG KÝ ---

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email này có ai dùng chưa
                var userExist = _context.Users.Any(u => u.Email == model.Email);
                if (userExist)
                {
                    // Lỗi email tồn tại
                    ModelState.AddModelError("", "Email này có người dùng rồi bae ơi!");
                    return View(model);
                }

                // Nếu OK thì tạo User mới để lưu vào DB
                var newUser = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password // Bước đầu lưu thẳng
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        // --- PHẦN ĐĂNG NHẬP ---

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tìm xem có ông User nào khớp cả Email và Password không
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Tạo thẻ bài thông tin và cấp quyền Admin luôn
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FullName ?? ""),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, "Admin") // Phát thẻ Admin để Phụng cho qua cửa bảo vệ
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                    await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                    // KIỂM TRA: Nếu là Admin thực thụ thì cho vào trang quản trị Admin liền
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    // Còn nếu là Khách hàng bình thường thì mới cho về trang chủ Home mua hàng
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Sai Email hoặc Mật khẩu rồi bae!");
            }
            return View(model);
        }

        // --- PHẦN ĐĂNG XUẤT ---
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }

        // --- PHẦN ĐỔI MẬT KHẨU ---

        [HttpGet]
        public IActionResult ChangePassword() => View();

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            // 1. Kiểm tra xem người dùng đã đăng nhập chưa thông qua Email lưu trong Cookie
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login");
            }

            // 2. Tìm người dùng đó trong Database
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null || user.Password != oldPassword)
            {
                ModelState.AddModelError("", "Mật khẩu cũ không chính xác nha bae!");
                return View();
            }

            // 3. Kiểm tra mật khẩu mới có khớp nhau không
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu mới nhập lại không khớp kìa!");
                return View();
            }

            // 4. Tiến hành cập nhật và lưu xuống SQL Server
            user.Password = newPassword;
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "Đổi mật khẩu thành công rồi nè! 🎉";
            return View();
        }

        // PHẦN QUÊN MẬT KHẨU ---

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email, string fullName, string newPassword, string confirmPassword)
        {
            // 1. Kiểm tra xem có tài khoản nào khớp cả Email và Họ tên không
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.FullName == fullName);

            if (user == null)
            {
                ModelState.AddModelError("", "Thông tin Email hoặc Họ tên không khớp với hệ thống nha bae!");
                return View();
            }

            // 2. Kiểm tra mật khẩu mới nhập lại có khớp nhau không
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu mới nhập lại không khớp kìa!");
                return View();
            }

            // 3. Cập nhật mật khẩu mới vào SQL Server
            user.Password = newPassword;
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "Đặt lại mật khẩu thành công! Giờ bạn có thể đăng nhập bằng mật khẩu mới rồi đó. 🎉";
            return View();
        }
    
   
        // CHỨC NĂNG: KHÁCH HÀNG XEM & CẬP NHẬT THÔNG TIN CÁ NHÂN
        
        // 1. GIAO DIỆN XEM THÔNG TIN (GET)
        [HttpGet]
        public IActionResult Profile()
        {
            // Lấy Email của người dùng đang đăng nhập từ Cookie
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login");
            }

            // Tìm user đó trong Database
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Trả dữ liệu User về trang giao diện
        }

        // 2. XỬ LÝ LƯU THÔNG TIN CẢI CHỈNH (POST)
        [HttpPost]
        public async Task<IActionResult> Profile(string fullName)
        {
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }

            // Tiến hành cập nhật trường Họ tên mới
            user.FullName = fullName;

            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync(); // Lưu thay đổi xuống SQL Server
                ViewBag.SuccessMessage = "Cập nhật thông tin cá nhân thành công rồi nè Boss! 🎉";
            }

            return View(user);
        }
    }
}