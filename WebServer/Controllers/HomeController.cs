/*using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebServer.Models;


namespace WebServer.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
*/
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        private MonitoringController monitoring = new MonitoringController();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Check()
        {
            return View();
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
