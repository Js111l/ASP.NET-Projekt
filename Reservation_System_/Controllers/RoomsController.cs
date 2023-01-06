
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservation_System_.Data;
using Reservation_System_.Models.ViewModels;
using System_rezerwacji_sal.Models;

namespace Reservation_System_.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }




        public async Task<IActionResult> SearchRoom()
        {


            var seats = new List<int> { 30, 40, 60, 100 };
            var types = new List<string> { "Laboratourium", "Sala wykładowa" };
            var projector = new List<string> { "Tak", "Nie" };
            var air = new List<string> { "Tak", "Nie" };
            var model = new RoomViewModel();

            model.selectSeats = new List<SelectListItem>();
            model.selectType = new List<SelectListItem>();
            model.selectProjector = new List<SelectListItem>();
            model.selectAirConditioner = new List<SelectListItem>();



            foreach (var numb in seats)
            {
                model.selectSeats.Add(new SelectListItem { Text = numb.ToString() });
            }
            foreach (var type in types)
            {
                model.selectType.Add(new SelectListItem { Text = type });
            }
            foreach (var str in projector)
            {
                model.selectProjector.Add(new SelectListItem { Text = str });
            }
            foreach (var str in air)
            {
                model.selectAirConditioner.Add(new SelectListItem { Text = str });
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> SearchRoom(RoomViewModel model)
        {
            var selectedNumber = model.selectedNumber;
            var selectedType = "lab";

            if (model.selectedType.Equals("Sala wykładowa"))
            {
                selectedType = "lecture hall";
            }

            var projector = false;
            if (model.optionProjector.Equals("Tak"))
            {
                projector = true;
            }
            var airCond = false;
            if (model.optionAirCond.Equals("Tak"))
            {
                airCond = true;
            }


            var resultFromContext = _context.Room
                                            .Where(x =>
                                                    x.numberOfSeats == selectedNumber
                                                    && x.roomType.Equals(selectedType)
                                                    && x.hasAirConditioner == airCond
                                                    && x.hasProjector == projector
                                                    ).ToList().Count();


            if (resultFromContext >= 1)
            {
                return View("RoomList", await _context
                                            .Room
                                            .Where(x => x.numberOfSeats == selectedNumber
                                                    && x.roomType.Equals(selectedType)
                                                    && x.hasAirConditioner == airCond
                                                    && x.hasProjector == projector
                                                    ).ToListAsync());
            }
            else
                return View("NoRoomFound", await _context
                                                  .Room
                                                  .Where(x => x.numberOfSeats == selectedNumber
                                                         && x.roomType == selectedType)
                                                        .ToListAsync());
        }

        public async Task<IActionResult> NoRoomFound()
        {
            return View(await _context.Room.ToListAsync());
        }

        public async Task<IActionResult> RoomList()
        {
            return View(await _context.Room.ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Room.ToListAsync());
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,number,numberOfSeats,hasProjector,roomType,hasAirConditioner")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,number,numberOfSeats,hasProjector,roomType,hasAirConditioner")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.Id))
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
            return View(room);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Room == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Room == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Room'  is null.");
            }
            var room = await _context.Room.FindAsync(id);
            if (room != null)
            {
                _context.Room.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.Id == id);
        }
    }
}
