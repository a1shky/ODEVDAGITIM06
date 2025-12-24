using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR; // SIGNALR İÇİN EKLEDİK
using ODEVDAGITIM06.Hubs;          // SIGNALR İÇİN EKLEDİK
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Ogrenci")]
    public class OgrenciController : Controller
    {
        private readonly IOdevRepository _odevRepository;
        private readonly ITeslimRepository _teslimRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHubContext<BildirimHub> _hubContext; // SIGNALR BAĞLANTISI

        // Constructor'a IHubContext ekledik
        public OgrenciController(IOdevRepository odevRepository, ITeslimRepository teslimRepository, IWebHostEnvironment hostEnvironment, IHubContext<BildirimHub> hubContext)
        {
            _odevRepository = odevRepository;
            _teslimRepository = teslimRepository;
            _hostEnvironment = hostEnvironment;
            _hubContext = hubContext; // Atamasını yaptık
        }

        public IActionResult Index()
        {
            var kullaniciId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var odevler = _odevRepository.GetOdevlerForOgrenci(kullaniciId);
            return View(odevler);
        }

        public IActionResult Yukle(int id)
        {
            var odev = _odevRepository.GetById(id);
            if (odev == null) return NotFound();
            return View(odev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Yukle(int id, IFormFile dosya)
        {
            if (dosya == null || dosya.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir dosya seçiniz.");
                return View(_odevRepository.GetById(id));
            }

            var uzanti = Path.GetExtension(dosya.FileName).ToLower();
            string klasorYolu = Path.Combine(_hostEnvironment.WebRootPath, "odevler");
            if (!Directory.Exists(klasorYolu)) Directory.CreateDirectory(klasorYolu);

            string yeniDosyaAdi = $"Odev_{id}_{Guid.NewGuid().ToString().Substring(0, 5)}{uzanti}";
            string tamYol = Path.Combine(klasorYolu, yeniDosyaAdi);

            using (var stream = new FileStream(tamYol, FileMode.Create))
            {
                await dosya.CopyToAsync(stream);
            }

            var teslim = new Teslim
            {
                OdevId = id,
                OgrenciId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DosyaYolu = "odevler/" + yeniDosyaAdi,
                TeslimTarihi = DateTime.Now
            };

            _teslimRepository.Add(teslim);

            // --- İŞTE KRİTİK SIGNALR SATIRI ---
            // Admin tarafında "YeniOdevGeldi" isimli fonksiyonu tetikliyoruz.
            // Parametre olarak öğrencinin adını gönderiyoruz.
            await _hubContext.Clients.All.SendAsync("YeniOdevGeldi", User.Identity.Name);
            // ----------------------------------

            TempData["SuccessMessage"] = "Ödeviniz başarıyla teslim edildi!";
            return RedirectToAction(nameof(Index));
        }
    }
}