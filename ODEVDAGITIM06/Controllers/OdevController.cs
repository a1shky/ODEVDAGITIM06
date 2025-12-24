using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ODEVDAGITIM06.Models;
using ODEVDAGITIM06.Repositories.Interfaces;

namespace ODEVDAGITIM06.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OdevController : Controller
    {
        private readonly IOdevRepository _odevRepository;
        private readonly IDersRepository _dersRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public OdevController(IOdevRepository odevRepository, IDersRepository dersRepository, UserManager<ApplicationUser> userManager)
        {
            _odevRepository = odevRepository;
            _dersRepository = dersRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var odevler = _odevRepository.GetAllWithDersAndOgrenci();
            return View(odevler);
        }

        public IActionResult Create()
        {
            ViewBag.DersListesi = new SelectList(_dersRepository.GetAll(), "DersId", "DersAdi");

            var ogrenciler = _userManager.Users.ToList();
            var ogrenciSelectItems = ogrenciler.Select(u => new
            {
                Id = u.Id,
                AdSoyad = $"{u.Ad} {u.Soyad} ({u.OgrenciNo})"
            });

            ViewBag.OgrenciListesi = new SelectList(ogrenciSelectItems, "Id", "AdSoyad");
            return View();
        }

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
            var ogrenciler = _userManager.Users.ToList();
            var ogrenciSelectItems = ogrenciler.Select(u => new { Id = u.Id, AdSoyad = $"{u.Ad} {u.Soyad} ({u.OgrenciNo})" });
            ViewBag.OgrenciListesi = new SelectList(ogrenciSelectItems, "Id", "AdSoyad", odev.OgrenciId);

            return View(odev);
        }

        public IActionResult Edit(int id)
        {
            var odev = _odevRepository.GetById(id);
            if (odev == null) return NotFound();

            ViewBag.DersListesi = new SelectList(_dersRepository.GetAll(), "DersId", "DersAdi", odev.DersId);
            var ogrenciler = _userManager.Users.ToList();
            var ogrenciSelectItems = ogrenciler.Select(u => new { Id = u.Id, AdSoyad = $"{u.Ad} {u.Soyad} ({u.OgrenciNo})" });
            ViewBag.OgrenciListesi = new SelectList(ogrenciSelectItems, "Id", "AdSoyad", odev.OgrenciId);

            return View(odev);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Odev odev)
        {
            if (id != odev.OdevId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _odevRepository.Update(odev);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_odevRepository.GetById(id) == null) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DersListesi = new SelectList(_dersRepository.GetAll(), "DersId", "DersAdi", odev.DersId);
            var ogrenciler = _userManager.Users.ToList();
            var ogrenciSelectItems = ogrenciler.Select(u => new { Id = u.Id, AdSoyad = $"{u.Ad} {u.Soyad} ({u.OgrenciNo})" });
            ViewBag.OgrenciListesi = new SelectList(ogrenciSelectItems, "Id", "AdSoyad", odev.OgrenciId);

            return View(odev);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _odevRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    } // Class kapatma parantezi
} // Namespace kapatma parantezi