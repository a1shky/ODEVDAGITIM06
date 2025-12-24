using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ODEVDAGITIM06.Models;

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KullaniciController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public KullaniciController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Veritabanındaki bütün kullanıcıları (öğrencileri) getir
            var kullanicilar = _userManager.Users.ToList();
            return View(kullanicilar);
        }

        // Kullanıcı Silme (Gerekirse diye ekliyorum)
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}