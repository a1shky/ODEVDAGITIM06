using Microsoft.AspNetCore.Mvc;
using System.Diagnostics; // Bunu da ekleyelim, standarttýr
using ODEVDAGITIM06.Models; // ErrorViewModel için

namespace ODEVDAGITIM06.Controllers
{
    public class HomeController : Controller
    {
        // Index metodunu geri döndürdük
        // Artýk /Login'e yönlendirmiyor, kendi ana sayfasýný (Views/Home/Index.cshtml) gösteriyor
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // ErrorViewModel'i Models klasörüne eklememiz gerekebilir,
            // ama þimdilik bu satýrý yoruma alalým, sadece View döndürsün.
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View();
        }
    }
}