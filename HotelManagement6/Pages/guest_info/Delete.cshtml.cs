using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.guest_info
{
    public class DeleteModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public DeleteModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
            
        }

        [BindProperty]
      public Guest Guest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.Id == id);

            if (guest == null)
            {
                return NotFound();
            }
            else 
            {
                Guest = guest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }
            // remove from guestreservationasc
            
            var reservations = _context.Guestreservationascs.Where(x => x.GuestId == id);
            
            _context.Guestreservationascs.RemoveRange(reservations);
            

            //var roomReservations = _context.Roomreservationints.Where(x => x.R);
            //_context.RoomReservationInts.RemoveRange(roomReservations);


            // remove reservation from reservation table finding the id
            var reservationsID = _context.Reservations.Where(x => x.ReservationId == id);
            _context.Reservations.RemoveRange(reservationsID);
            _context.SaveChanges();


            var guest = await _context.Guests.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
