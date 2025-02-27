using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mission8Assignment.Models;

namespace mission8Assignment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
