using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Edit metodundaki hata yakalama için
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DersController : Controller
    {
        private readonly IDersRepository _dersRepository;

        public DersController(IDersRepository dersRepository)
        {
            _dersRepository = dersRepository;
        }

        // --- BU METOTLAR AYNI KALDI ---
        // (Index'ten 'TempData' kontrolü kaldırıldı, temizlendi)
        public IActionResult Index()
        {
            var dersler = _dersRepository.GetAll();
            return View(dersler);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ders ders)
        {
            if (ModelState.IsValid)
            {
                _dersRepository.Add(ders);
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }

        public IActionResult Edit(int id)
        {
            var ders = _dersRepository.GetById(id);
            if (ders == null)
            {
                return NotFound();
            }
            return View(ders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Ders ders)
        {
            if (id != ders.DersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dersRepository.Update(ders);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }
        // --- BU METOTLAR AYNI KALDI ---


        // GERİ DÖNÜLDÜ: BİZİM ÇALIŞAN MODERN $.ajax YÖNTEMİMİZ
        // Bu metot, JavaScript'e (AJAX'a) JSON olarak cevap verir.
        // [ValidateAntiForgeryToken] KULLANMIYORUZ (basit AJAX çağrısı)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var ders = _dersRepository.GetById(id);
                if (ders == null)
                {
                    // Bulamazsa JS'e hata JSON'ı döndür
                    return Json(new { success = false, message = "Ders bulunamadı." });
                }

                // Dersi sil
                _dersRepository.Delete(ders);

                // JS'e başarılı olduğuna dair bir JSON mesajı döndür
                return Json(new { success = true, message = "Ders başarıyla silindi." });
            }
            catch (Exception ex)
            {
                // Hata olursa (örn: bu derse bağlı ödevler varsa)
                // JS'e hata JSON'ı döndür
                return Json(new { success = false, message = "Silme işlemi başarısız. Bu derse bağlı ödevler olabilir." });
            }
        }
        // DEĞİŞİKLİĞİN SONU

    }
}