using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.ViewModels;
using System.Threading.Tasks;

namespace ODEVDAGITIM06.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    OgrenciNo = model.OgrenciNo
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));

                    if (!await _roleManager.RoleExistsAsync("Ogrenci"))
                        await _roleManager.CreateAsync(new IdentityRole("Ogrenci"));

                    await _userManager.AddToRoleAsync(user, "Ogrenci");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Ogrenci");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Kullanıcıyı email ile bul (model.Username email olarak kullanılıyor)
                    var user = await _userManager.FindByEmailAsync(model.Username);

                    if (user != null)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Ogrenci");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Giriş başarısız. E-posta veya şifre hatalı.");
            }
            return View(model);
        }

        // --- DÜZELTİLEN LOGOUT METODU ---
        // HttpGet ekleyerek linke tıklandığında çalışmasını sağlıyoruz.
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // Çıkış yapınca ana sayfaya gönder
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}