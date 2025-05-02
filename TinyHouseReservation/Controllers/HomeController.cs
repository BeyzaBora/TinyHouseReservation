using Microsoft.AspNetCore.Mvc;
using TinyHouseReservations.DataAccess;  // Kullan�c� verisi i�in repository
using TinyHouseReservations.Models;     // Kullan�c� modelini kullanabilmek i�in

namespace TinyHouseReservations.Controllers
{
    public class HomeController : Controller
    {
        private readonly KullaniciRepository _repo;

        public HomeController()
        {
            _repo = new KullaniciRepository(); // Repository'i ba�lat�yoruz
        }

        public IActionResult Index()
        {
            // Veritaban�ndaki t�m kullan�c�lar� al�yoruz
            var kullanicilar = _repo.GetAll();
            return View(kullanicilar); // Kullan�c� listesini view'a g�nderiyoruz
        }

        // Login sayfas�na y�nlendiren metod
        public IActionResult Login()
        {
            return View();
        }

        // Login i�lemini ger�ekle�tiren metod
        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            // Girilen email ve �ifreyi kontrol ediyoruz
            var kullanici = _repo.GirisYap(email, sifre);

            if (kullanici != null)
            {
                // Kullan�c� giri�i ba�ar�l�ysa, veriyi session'a kaydediyoruz
                HttpContext.Session.SetInt32("KullaniciID", kullanici.KullaniciID);
                return RedirectToAction("Index");
            }

            // Hata mesaj�
            ViewBag.Hata = "Ge�ersiz e-posta veya �ifre.";
            return View();
        }
    }
}
