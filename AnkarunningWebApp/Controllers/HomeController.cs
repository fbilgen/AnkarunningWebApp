using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnkarunningWebApp.Models;

namespace AnkarunningWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Trainings()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Events()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Profiles() {
            ViewData["Message"] = "Your Profiles page.";
            return View();
        }

        public IActionResult Photos() {
            ViewData["Message"] = "Your photos description page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
