using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ODEVDAGITIM06.ViewModels;
using System.Security.Claims;

namespace ODEVDAGITIM06.Controllers
{
    public class LoginController : Controller
    {
        // Burası /Login/Index adresine (GET) isteği geldiğinde
        // Kullanıcıya giriş formunu gösterecek.
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Burası kullanıcı formdaki 'Giriş Yap' butonuna bastığında
        // (POST) çalışacak.
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            // Gelen form geçerli mi? (Required alanlar dolu mu?)
            if (ModelState.IsValid)
            {
                // Ara Sınav için 'Identity' (veritabanı) kullanmıyoruz.
                // Kullanıcıyı elle (manuel) kontrol ediyoruz.
                // Normalde burası veritabanına gider, biz şimdilik sabitliyoruz.
                if (model.Username == "admin" && model.Password == "12345")
                {
                    // === KULLANICI DOĞRU - GİRİŞ İŞLEMİNİ BAŞLAT ===

                    // 1. Kullanıcının "Kimlik Kartını" (Claims) oluşturuyoruz.
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim("FullName", "Sistem Yöneticisi"),
                        new Claim(ClaimTypes.Role, "Admin"), // Kullanıcıya 'Admin' rolü veriyoruz.
                    };

                    // 2. Bu kimlik kartından bir "Oturum" (Identity) oluşturuyoruz.
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // 3. Giriş parametrelerini ayarlıyoruz.
                    var authProperties = new AuthenticationProperties
                    {
                        // Tarayıcı kapansa bile oturumu açık tut (opsiyonel)
                        // IsPersistent = true, 
                    };

                    // 4. HttpContext'e bu oturumla 'Giriş Yap' (SignIn) komutunu veriyoruz.
                    // Bu komut, tarayıcıya o şifreli cookie'yi (çerezi) gönderir.
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // 5. Giriş başarılı, kullanıcıyı Admin ana sayfasına yönlendir.
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    // === KULLANICI ADI VEYA ŞİFRE YANLIŞ ===
                    ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
                    return View(model);
                }
            }

            // Form geçerli değilse (örn: boş alan varsa), formu tekrar göster.
            return View(model);
        }

        // /Login/Logout adresine gidildiğinde çalışır
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Oturumu kapatır (Cookie'yi siler)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Kullanıcıyı ana sayfaya yönlendir
            return RedirectToAction("Index", "Home");
        }

        // Yetkisiz erişim denemesinde buraya yönlendirilir
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}