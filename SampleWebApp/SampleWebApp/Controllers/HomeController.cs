using Microsoft.AspNetCore.Mvc;
using SampleWebApp.Models;
using SampleWebApp.Services.Contracts;
using System.Diagnostics;

namespace SampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itemService;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IItemService itemService, IConfiguration configuration)
        {
            _logger = logger;
            _itemService = itemService;
            _config = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemService.GetAll();
            ViewBag.ConnectionString = _config.GetConnectionString("DefaultConnection");
            return View(items);
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