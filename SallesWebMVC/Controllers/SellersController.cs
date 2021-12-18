using Microsoft.AspNetCore.Mvc;
using SallesWebMVC.Models;
using SallesWebMVC.Services;

namespace SallesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellersService _sellersService;
        public SellersController(SellersService sellersService)
        {
            _sellersService = sellersService;
        }
        public IActionResult Index()
        {           
            var list = _sellersService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
