using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MavenGenerator.Models;
using MavenGenerator.Models.Data;
using MavenGenerator.Scripts;

namespace MavenGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(MavenGeneratorViewModel model)
        {
            ViewData["Stage"] = 1;
            return View(model);
        }

        public IActionResult Result(MavenGeneratorViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index");
            return View("_Result");
        }

        [HttpPost]
        public IActionResult PluginAmountGenerate(MavenGeneratorViewModel model)
        {
            ViewData["Stage"] = 2;
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult MavenGenerate(MavenGeneratorViewModel model)
        {
            ViewData["CalculatedMarkup"] = MavenMarkupGenerator.Create(model);
            return View("_Result", model);
        }

        [HttpPost]
        public IActionResult Reset()
        {
            ViewData["Stage"] = 1;
            return View("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
