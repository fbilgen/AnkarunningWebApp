using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ankarunning.Web.Models;

namespace Ankarunning.Web.Controllers
{
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Trainings() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        public IActionResult Trainings(TrainingViewModel model) {

            if (ModelState.IsValid) {
                var mymodel = model;
                return RedirectToAction("Club");
            }

            return View(model);

        }

        public IActionResult Events() {
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

        public IActionResult Club() {
            ViewData["Message"] = "Your club description page.";
            return View();
        }

        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
