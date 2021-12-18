using Microsoft.AspNetCore.Mvc;

namespace SallesWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
