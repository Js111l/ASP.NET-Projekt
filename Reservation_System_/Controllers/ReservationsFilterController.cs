using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservation_System_.Data;

namespace Reservation_System_.Controllers
{
    public class ReservationsFilterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsFilterController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> ReservationDateDesc()
        {
            return View(await _context.Reservation.ToListAsync());
        }
        public async Task<IActionResult> ReservationDateAsc()
        {
            return View(await _context.Reservation.ToListAsync());

        }
        public async Task<IActionResult> ReservationTimeAsc()
        {
            return View(await _context.Reservation.ToListAsync());
        }
        public async Task<IActionResult> ReservationTimeDesc()
        {
            return View(await _context.Reservation.ToListAsync());
        }



    }
}
