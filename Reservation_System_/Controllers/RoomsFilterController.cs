using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservation_System_.Data;
using Reservation_System_.Models;
using Reservation_System_.Models.ViewModels;


namespace Reservation_System_.Controllers
{
    public class RoomsFilterController : Controller
    {
        private readonly ApplicationDbContext _context;


        public RoomsFilterController(ApplicationDbContext context)
        {
            _context = context;

        }


        public async Task<IActionResult> RoomListNumberOfSeatsAsc()
        {
            return View(await _context.Room.ToListAsync());
        }
        public async Task<IActionResult> RoomListNumberOfSeatsDesc()
        {
            return View(await _context.Room.ToListAsync());
        }
        public async Task<IActionResult> RoomListRoomNumberDesc()
        {
            return View(await _context.Room.ToListAsync());
        }
        public async Task<IActionResult> RoomListRoomNumberAsc()
        {
            return View(await _context.Room.ToListAsync());
        }

    }
}
