using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using WebServer.Models;


namespace WebServer.Controllers
{
    public class MonitoringController : Controller
    {
        private Monitoring monitoring;
        public IActionResult Index() //monitoring
        {
            return View();
        }

        public IActionResult Check(string directoryPath)
        {
            // Создайте экземпляр Monitoring и начните мониторинг указанной директории
            WebServer.Models.Monitoring monitoring = new WebServer.Models.Monitoring(directoryPath);

            // Обработчик события создания файла
            monitoring.FileCreated += (sender, e) =>
            {
                Console.WriteLine($"File created: {e.FullPath}");
                // Ваш код обработки события создания файла
            };

            // Обработчик события удаления файла
            monitoring.FileDeleted += (sender, e) =>
            {
                Console.WriteLine($"File deleted: {e.FullPath}");
                // Ваш код обработки события удаления файла
            };

            // Обработчик события изменения файла
            monitoring.FileChanged += (sender, e) =>
            {
                Console.WriteLine($"File changed: {e.FullPath}");
                // Ваш код обработки события изменения файла
            };

            // Обработчик события переименования файла
            monitoring.FileRenamed += (sender, e) =>
            {
                Console.WriteLine($"File renamed: {e.OldFullPath} to {e.FullPath}");
                // Ваш код обработки события переименования файла
            };

            // Ваш код обработки запроса

            return RedirectToAction("Results"); // Замените "Index" на действие, которое вы хотите выполнить после мониторинга
        }

        public IActionResult Results()
        {
            if (monitoring != null)
            {
                var results = monitoring.GetResults();
                return Json(results);
            }

            return Json(new List<string>());
        }
    }
}
