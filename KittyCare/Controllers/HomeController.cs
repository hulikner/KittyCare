using KittyCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KittyCare.Repositories;

namespace KittyCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatRepository _catRepo;

        public HomeController(ILogger<HomeController> logger, ICatRepository catRepository)
        {
            _logger = logger;
            _catRepo = catRepository;
        }

        public ActionResult Index()
        {
            

            List<Cat> cats = _catRepo.GetAllCats();

            return View(cats);
        }
        

        public IActionResult Privacy()
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
