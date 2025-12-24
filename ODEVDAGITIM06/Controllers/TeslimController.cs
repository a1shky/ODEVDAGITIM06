using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;
using System.IO; // Dosya işlemleri için gerekli

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeslimController : Controller
    {
        private readonly ITeslimRepository _teslimRepository;

        public TeslimController(ITeslimRepository teslimRepository)
        {
            _teslimRepository = teslimRepository;
        }

        // GET: /Teslim
        public IActionResult Index()
        {
            var teslimler = _teslimRepository.GetAllWithOdevDers();
            return View(teslimler);
        }

        // GET: /Teslim/NotVer/5
        public IActionResult NotVer(int id)
        {
            var teslim = _teslimRepository.GetByIdWithDetails(id);
            if (teslim == null)
            {
                return NotFound();
            }
            return View(teslim);
        }

        // POST: /Teslim/NotVer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NotVer(int id, Teslim teslim)
        {
            if (id != teslim.TeslimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mevcutTeslim = _teslimRepository.GetById(id);
                if (mevcutTeslim == null) return NotFound();

                mevcutTeslim.Not = teslim.Not;

                _teslimRepository.Update(mevcutTeslim);

                return RedirectToAction(nameof(Index));
            }

            var teslimDetayli = _teslimRepository.GetByIdWithDetails(id);
            return View(teslimDetayli);
        }

        // --- SİLME İŞLEMİ (DÜZELTİLDİ) ---
        // GET: /Teslim/Sil/5
        public IActionResult Sil(int id)
        {
            // 1. Dosya adını bulmak için kaydı çekiyoruz
            var teslim = _teslimRepository.GetById(id);

            if (teslim != null)
            {
                // 2. Eğer fiziksel dosya varsa sunucudan (wwwroot/uploads) siliyoruz
                if (!string.IsNullOrEmpty(teslim.DosyaYolu))
                {
                    try
                    {
                        // Veritabanındaki yolun başındaki / işaretini temizleyip tam yolu buluyoruz
                        string tamYol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", teslim.DosyaYolu.TrimStart('/'));

                        if (System.IO.File.Exists(tamYol))
                        {
                            System.IO.File.Delete(tamYol);
                        }
                    }
                    catch
                    {
                        // Dosya silinirken hata olsa bile (örn: dosya zaten yoksa) devam et, veritabanından silinsin.
                    }
                }

                // 3. Veritabanından siliyoruz
                // HATA BURADAYDI: Artık nesneyi değil, direkt ID'yi gönderiyoruz.
                _teslimRepository.Delete(id);
            }

            // 4. Listeye geri dön
            return RedirectToAction(nameof(Index));
        }
    }
}