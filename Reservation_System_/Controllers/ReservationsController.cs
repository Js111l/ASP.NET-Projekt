using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservation_System_.Data;
using Reservation_System_.Models.ViewModels;
using System_rezerwacji_sal.Models;

namespace Reservation_System_.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowReservation(int id)
        {
            return View("Index", id);

        }
        public async Task<IActionResult> FilterByDate(int id)
        {
            return View("Index", id);
        }

        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.Reservation.Where(x => x.RoomId == id).ToListAsync());

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public IActionResult Create()
        {

            return View();
        }
        public IActionResult UnsuccessfulReservation()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,Name,StartReservation,EndReservation")]
        Reservation reservation, int id)
        {

            var reserationDates = _context.Reservation.Where(x => x.StartReservation.Day.Equals(reservation.StartReservation.Day)).ToArray();

            foreach (var item in reserationDates)
            {
                if (reservation.StartReservation <= item.StartReservation && reservation.EndReservation >= item.EndReservation)
                {
                    return View("UnsuccessfulReservation");
                }

                if (reservation.StartReservation >= item.StartReservation && reservation.StartReservation <= item.EndReservation
                    && reservation.EndReservation >= item.EndReservation)
                {
                    return View("UnsuccessfulReservation");

                }
                if (reservation.StartReservation <= item.StartReservation && reservation.EndReservation <= item.EndReservation)
                {
                    return View("UnsuccessfulReservation");

                }
            }
            reservation.Name = User.Identity.Name;
            reservation.RoomId = id;
            if (ModelState.ErrorCount <= 1)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return View("SuccessReservation", await _context.Reservation.Where(x => x.Equals(reservation)).ToListAsync());
            }
            else
            {
                return View("Create");
            }


        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartReservation,EndReservation")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}