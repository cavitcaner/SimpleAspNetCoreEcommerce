using Kafein.Database;
using Kafein.Eticaret.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kafein.Eticaret.Controllers
{
    public class AccountController : Controller
    {
        private readonly EticaretDbContext _eticaretDb;
        public AccountController(EticaretDbContext eticaretDb)
        {
            _eticaretDb = eticaretDb;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInfoDto loginInfo)
        {
            var user = _eticaretDb.Kullanicilar.Where(x => x.Email == loginInfo.Email && x.Sifre == loginInfo.Password).FirstOrDefault();

            if (user == null)
                return View(loginInfo);
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Adi),
                    new Claim(ClaimTypes.Surname, user.Soyadi),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                return Redirect("/Home/Index");
            }
        }

        public IActionResult Logout()
        {
            //Çıkış işlemini gerçekleştir.
            return View();
        }
    }
}
