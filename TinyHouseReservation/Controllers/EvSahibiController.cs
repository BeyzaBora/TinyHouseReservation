using Microsoft.AspNetCore.Mvc;
using TinyHouseReservations.DataAccess;
using TinyHouseReservations.Models;

namespace TinyHouseReservations.Controllers
{
    public class EvSahibiController : Controller
    {
        private readonly EvRepository _evRepo = new EvRepository();

        public IActionResult Index()
        {
            var kullaniciId = HttpContext.Session.GetInt32("KullaniciID");

            if (kullaniciId == null)
                return RedirectToAction("Giris", "Kullanici");

            var evler = _evRepo.EvleriGetir(kullaniciId.Value);
            return View(evler);
        }

        public IActionResult YeniIlan()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YeniIlan(Ev ev)
        {
            var kullaniciId = HttpContext.Session.GetInt32("KullaniciID");

            if (kullaniciId == null)
                return RedirectToAction("Giris", "Kullanici");

            ev.EvSahibiID = kullaniciId.Value;

            if (ModelState.IsValid)
            {
                _evRepo.EvEkle(ev);
                return RedirectToAction("Index");
            }

            return View(ev);
        }

        public IActionResult IlanGuncelle(int id)
        {
            var ev = _evRepo.EvGetirById(id);
            return View(ev);
        }

        [HttpPost]
        public IActionResult IlanGuncelle(Ev ev)
        {
            if (ModelState.IsValid)
            {
                _evRepo.EvGuncelle(ev);
                return RedirectToAction("Index");
            }

            return View(ev);
        }

        public IActionResult IlanSil(int id)
        {
            _evRepo.EvSil(id);
            return RedirectToAction("Index");
        }
    }
}
