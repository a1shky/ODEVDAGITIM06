using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (ders == null) return NotFound();
            return View(ders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Ders ders)
        {
            if (id != ders.DersId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _dersRepository.Update(ders);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_dersRepository.GetById(id) == null) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }

        // --- AJAX İÇİN ÖZEL SİLME METODU (JSON DÖNER) ---
        [HttpPost]
        public IActionResult SilAjax(int id)
        {
            var ders = _dersRepository.GetById(id);
            if (ders != null)
            {
                _dersRepository.Delete(id);
                // HTML yerine JSON dönüyoruz ki AJAX anlasın
                return Json(new { success = true, message = "Ders başarıyla silindi." });
            }
            return Json(new { success = false, message = "Silinecek ders bulunamadı." });
        }
    }
}