using Microsoft.AspNetCore.Mvc;

namespace NetCore.Hosting.Controllers
{
    public class HomeController : Controller
    {
        private readonly DefaultDataProtectorProvider _dataProtector;

        public HomeController(DefaultDataProtectorProvider dataProtector)
        {
            _dataProtector = dataProtector;
            var protectedPayload = _dataProtector.Protect("Hello World!");
            var unprotectedPayload = _dataProtector.Unprotect(protectedPayload);
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Gonder(int islemId, int kullaniciId)
        {
            ViewBag.IslemId = islemId;
            ViewBag.KullaniciId = kullaniciId;
            return View();
        }
    }
}
