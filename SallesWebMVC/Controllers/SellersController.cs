using Microsoft.AspNetCore.Mvc;
using SallesWebMVC.Models;
using SallesWebMVC.Models.ViewModels;
using SallesWebMVC.Services;

namespace SallesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellersService _sellersService;
        private readonly DepartmentServices _departmentServices;
        public SellersController(SellersService sellersService, DepartmentServices departmentServices)
        {
            _sellersService = sellersService;
            _departmentServices = departmentServices;
        }
        public IActionResult Index()
        {           
            var list = _sellersService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            var departments = _departmentServices.FindAll();
            var viewModel = new SellerFormViewModel
            {
                Departments = departments
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if(id == null) return NotFound();
            var obj = _sellersService.FindById(id.Value);
            if(obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellersService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) return NotFound();
            return View(obj);
        }
    }
}
