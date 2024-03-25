using Kafein.Business;
using Kafein.Database;
using Kafein.Eticaret.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Kafein.Eticaret.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductManager _productManager;

        private Dictionary<int,int> GetBasket()
        {
            var sepetListe = new Dictionary<int, int>();

            if (Request.Cookies.TryGetValue("sepet", out var mevcutSepet))
            {
                sepetListe = JsonSerializer.Deserialize<Dictionary<int, int>>(mevcutSepet);
            }

            return sepetListe;
        }

        public ProductsController(IProductManager productManager)
        {
            this._productManager = productManager;
        }

        public IActionResult Index()
        {
            var urunListesi = _productManager.GetUrunList();

            return View(urunListesi);
        }

        public IActionResult AddToBasket(int id)
        {
            var sepetListe = GetBasket();

            if (sepetListe.ContainsKey(id))
            {
                sepetListe[id] = sepetListe[id] + 1;
            }
            else
                sepetListe.Add(id, 1);

            Response.Cookies.Append("sepet", JsonSerializer.Serialize(sepetListe));

            return RedirectToAction("Index");
        }

        public IActionResult Basket()
        {
            var sepetListe = GetBasket();
            
            var urunIdList = sepetListe.Select(z => z.Key).ToList();  

            var urunListesi = _productManager.GetUrunList(z => urunIdList.Contains(z.Id)).ToList();

            ViewBag.Basket = sepetListe;

            return View(urunListesi);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Checkout(CardInformationDto card)
        {
            Response.Cookies.Delete("sepet");




            return View();
        }
    }
}
