using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Admin")] // Bu controller'a sadece Admin girebilir
    public class TeslimController : Controller
    {
        private readonly ITeslimRepository _teslimRepository;

        // Constructor'a (Yapıcı Metot) Teslim Repository'yi enjekte ediyoruz
        public TeslimController(ITeslimRepository teslimRepository)
        {
            _teslimRepository = teslimRepository;
        }

        // GET: /Teslim
        // Anasayfa (Teslimleri Listeleme)
        public IActionResult Index()
        {
            // Repository'de yazdığımız Teslim, Ödev ve Ders bilgisini getiren metodu kullanıyoruz
            var teslimler = _teslimRepository.GetAllWithOdevDers();
            return View(teslimler);
        }

        // GET: /Teslim/NotVer/5
        // Not verme sayfasını GÖSTERİR (formu mevcut verilerle doldurur)
        public IActionResult NotVer(int id)
        {
            // İki seviyeli Include'u burada kullanamayız, sadece tekil kayıt çekiyoruz.
            // Bu yüzden View'e gitmeden önce Teslim'i tek başına çekmek yeterli.
            var teslim = _teslimRepository.GetById(id);
            if (teslim == null)
            {
                return NotFound();
            }
            // NOT: Öğrenci ve Ödev bilgisi View'de gösterilecek ama şu an sadece ID var.
            // Final aşamasında Identity'yi ekleyince öğrenci adını da çekeceğiz.
            return View(teslim);
        }

        // POST: /Teslim/NotVer/5
        // Not verme formundan gelen veriyi KAYDEDER
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
                // Mevcut teslimi veritabanından çek (ki OlusturmaTarihi, DosyaYolu vs. kaybolmasın)
                var mevcutTeslim = _teslimRepository.GetById(id);
                if (mevcutTeslim == null) return NotFound();

                // SADECE NOT ALANINI güncelliyoruz
                mevcutTeslim.Not = teslim.Not;

                // Güncellemeyi kaydet
                _teslimRepository.Update(mevcutTeslim);

                // İşlem bittiyse Listeye geri dön
                return RedirectToAction(nameof(Index));
            }

            // Model geçerli değilse, formu hatalarla geri göster
            return View(teslim);
        }
    }
}