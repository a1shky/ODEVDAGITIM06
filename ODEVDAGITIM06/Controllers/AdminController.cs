using Microsoft.AspNetCore.Authorization; // 1. YENİ EKLENDİ
using Microsoft.AspNetCore.Mvc;

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Admin")] // 2. YENİ EKLENDİ - Bu controller'ı ve içindeki her şeyi kilitle.
                                 // Sadece 'Admin' rolüne sahip olanlar girebilir.
    public class AdminController : Controller
    {
        // Burası Admin Panelimizin Ana Sayfası olacak
        public IActionResult Index()
        {
            return View();
        }
    }
}