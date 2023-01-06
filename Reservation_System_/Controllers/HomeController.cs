using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservation_System_.Data;
using Reservation_System_.Models;
using Reservation_System_.Models.ViewModels;
using System.Diagnostics;

namespace Reservation_System_.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;




        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationContext)
        {
            _context = applicationContext;
            _logger = logger;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservation.ToListAsync());

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