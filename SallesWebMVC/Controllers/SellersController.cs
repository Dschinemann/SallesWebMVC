using Microsoft.AspNetCore.Mvc;
using SallesWebMVC.Models;
using SallesWebMVC.Models.ViewModels;
using SallesWebMVC.Services;
using SallesWebMVC.Services.Exceptions;
using System.Diagnostics;

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
            if (!ModelState.IsValid)
            {
                var departments = _departmentServices.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) return RedirectToAction(nameof(Error), new { Message = "Id not found" });
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
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            List<Department> departments = _departmentServices.FindAll();
            SellerFormViewModel model = new SellerFormViewModel
            {
                Seller = obj,
                Departments = departments,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentServices.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

                if (id != seller.Id) return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
                try
                {
                    _sellersService.update(seller);
                    return RedirectToAction(nameof(Index));
                }
                catch (NotFoundExceptions e)
                {
                    return RedirectToAction(nameof(Error), new { Message = e.Message });
                }
                catch (DbConcurrencyException e)
                {
                    return RedirectToAction(nameof(Error), new { Message = e.Message });
                }
            }

            public IActionResult Error(string message)
            {
                var viewModel = new ErrorViewModel
                {
                    Message = message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //Id da requisição
                };
                return View(viewModel);
            }
        }
    }
