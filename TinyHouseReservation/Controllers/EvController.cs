using Microsoft.AspNetCore.Mvc;
using TinyHouseReservations.DataAccess;
using TinyHouseReservations.Models;

namespace TinyHouseReservations.Controllers
{
    public class EvController : Controller
    {
        private readonly EvRepository _evRepo = new EvRepository();

        public IActionResult Index()
        {
//burada evleri listeliyorsanız giriş yaptığınız ev sahibinin Id si ile evleriGetir metoduna gönderin

            var evler = _evRepo.EvleriGetir(2);
            return View(evler);
        }
        // buraya ev ekleme metodu yazın ama önce ev ekleme sayfasını oluşturun
        // ev ekleme metodunda hangi kullanıcıyı sessionda tutuyorsanız
        // yani giriş yapan kullanıcının Id sini bulup o Kullanıcı Id ile Ev oluştur metodunu yazın.

        public IActionResult EvEkle(Ev ev)
        {
//buradaki datayı ön yüzde oluşturduğunuz formdan almanız lazım.
            ev.Baslik = "Ev Başlığı";
            ev.Aciklama = "Ev Açıklaması";
            ev.Konum = "Ev Konumu";
            ev.Fiyat = 1000;
            ev.Durum = true;
            ev.GorselYolu = "ev.jpg";
            ev.EvSahibiID = 2; // Giriş yapan kullanıcının ID'si
            // Ev ekleme işlemi
            _evRepo.EvEkle(ev);

            return RedirectToAction("Index");
        }
    }
}
