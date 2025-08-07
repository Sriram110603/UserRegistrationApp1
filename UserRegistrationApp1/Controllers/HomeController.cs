using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationApp1.Models;

namespace UserRegistrationApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About() {
            return View();
        }
        public IActionResult Suggestions()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Reviews()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
