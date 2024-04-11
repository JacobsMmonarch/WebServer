// HomeController.cs
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class HomeMonitoring : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    public class MonitoringMonitoring : Controller
    {
        private Monitoring monitoring;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(string directoryPath)
        {
            monitoring = new Monitoring(directoryPath);
            monitoring.FileCreated += (sender, e) => { /* Send event using SignalR or other method */ };
            monitoring.FileDeleted += (sender, e) => { /* Send event using SignalR or other method */ };
            monitoring.FileChanged += (sender, e) => { /* Send event using SignalR or other method */ };
            monitoring.FileRenamed += (sender, e) => { /* Send event using SignalR or other method */ };

            return RedirectToAction("Results");
        }

        public IActionResult Results()
        {
            if (monitoring != null)
            {
                var results = monitoring.GetResults();
                return View(results);
            }

            return View(new List<string>());
        }
    }
}
