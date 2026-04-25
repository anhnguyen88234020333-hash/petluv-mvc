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
                    // Tạo (Cookie) để web nhớ mình
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.FullName??"") };
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                    await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home"); // Đăng nhập xong thì về trang chủ
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
    }
}