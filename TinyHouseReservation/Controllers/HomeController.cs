using Microsoft.AspNetCore.Mvc;
using TinyHouseReservations.DataAccess;  // Kullanıcı verisi için repository
using TinyHouseReservations.Models;     // Kullanıcı modelini kullanabilmek için

namespace TinyHouseReservations.Controllers
{
    public class HomeController : Controller
    {
        private readonly KullaniciRepository _repo;

        public HomeController()
        {
            _repo = new KullaniciRepository(); // Repository'i başlatıyoruz
        }

        public IActionResult Index()
        {
            // Veritabanındaki tüm kullanıcıları alıyoruz
            var kullanicilar = _repo.GetAll();
            return View(kullanicilar); // Kullanıcı listesini view'a gönderiyoruz
        }

        // Login sayfasına yönlendiren metod
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            var kullanici = _repo.GirisYap(email, sifre);

            if (kullanici != null)
            {
                Console.WriteLine("Giriş Başarılı: " + kullanici.Email + " - RolID: " + kullanici.RolID);

                HttpContext.Session.SetInt32("KullaniciID", kullanici.KullaniciID);
                HttpContext.Session.SetInt32("RolID", kullanici.RolID);

                if (kullanici.RolID == 2) // Ev Sahibi
                {
                    HttpContext.Session.SetInt32("EvSahibiID", kullanici.KullaniciID);
                    Console.WriteLine("EvSahibiID yazıldı: " + kullanici.KullaniciID);
                    return RedirectToAction("Index", "EvSahibi");
                }
                else if (kullanici.RolID == 1)  // Admin
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (kullanici.RolID == 3)  // Kiracı
                {
                    return RedirectToAction("Index", "Kiraci");
                }
            }

            ViewBag.Hata = "Geçersiz e-posta veya şifre.";
            return View();
        }

    }
}
