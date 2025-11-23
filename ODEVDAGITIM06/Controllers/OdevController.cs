using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Hata yakalama için bu eklendi
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OdevController : Controller
    {
        private readonly IOdevRepository _odevRepository;
        private readonly IDersRepository _dersRepository;

        public OdevController(IOdevRepository odevRepository, IDersRepository dersRepository)
        {
            _odevRepository = odevRepository;
            _dersRepository = dersRepository;
        }

        // GET: /Odev
        public IActionResult Index()
        {
            var odevler = _odevRepository.GetAllWithDers();
            return View(odevler);
        }

        // GET: /Odev/Create
        public IActionResult Create()
        {
            ViewBag.DersListesi = new SelectList(_dersRepository.GetAll(), "DersId", "DersAdi");
            return View();
        }

        // POST: /Odev/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Odev odev)
        {
            if (ModelState.IsValid)
            {
                odev.OlusturmaTarihi = DateTime.Now;
                _odevRepository.Add(odev);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DersListesi = new SelectList(_dersRepository.GetAll(), "DersId", "DersAdi", odev.DersId);
            return View(odev);
        }


        // YENİ EKLENDİ - GET: /Odev/Edit/5
        // Düzenleme sayfasını GÖSTERİR (formu mevcut verilerle doldurur)
        public IActionResult Edit(int id)
        {
            var odev = _odevRepository.GetById(id); // Ödevi ID'ye göre bul
            if (odev == null)
            {
                return NotFound(); // Bulamazsa 404
            }

            // Düzenleme formuna da Ders listesi lazım (dropdown için)
            // 'odev.DersId' parametresi, o anki dersin seçili gelmesini sağlar
            ViewBag.DersListesi = new SelectList(_dersRepository.GetAll(), "DersId", "DersAdi", odev.DersId);
            return View(odev); // Bulursa, o ödevin bilgilerini View'e gönder
        }

        // YENİ EKLENDİ - POST: /Odev/Edit/5
        // Düzenleme formundan gelen veriyi KAYDEDER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Odev odev)
        {
            if (id != odev.OdevId)
            {
                return NotFound(); // Formdaki ID ile URL'deki ID uyuşmazsa 404
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // ÖNEMLİ: Formdan 'OlusturmaTarihi' gelmediği için
                    // onu veritabanından alıp korumamız lazım.
                    // Şimdilik basit repository'miz tüm nesneyi günceller.
                    // Bu yüzden formda OlusturmaTarihi'ni gizli taşımalıyız (bir sonraki adımda yapacağız).
                    _odevRepository.Update(odev);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index)); // İşlem bittiyse Listeye geri dön
            }

            // Model geçerli değilse, formu hatalarla geri göster (dropdown'ı TEKRAR doldur)
            ViewBag.DersListesi = new SelectList(_dersRepository.GetAll(), "DersId", "DersAdi", odev.DersId);
            return View(odev);
        }


        // AJAX ile Silme Metodu (Bu zaten vardı)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var odev = _odevRepository.GetById(id);
                if (odev == null)
                {
                    return Json(new { success = false, message = "Ödev bulunamadı." });
                }

                _odevRepository.Delete(odev);

                return Json(new { success = true, message = "Ödev başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Silme işlemi sırasında bir hata oluştu." });
            }
        }

    }
}