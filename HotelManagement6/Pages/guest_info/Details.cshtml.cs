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
    public class DetailsModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public DetailsModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

      public Guest Guest { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }
            var guest = await _context.Guests
       .Include(g => g.Guestreservationascs)
           .ThenInclude(gra => gra.Reservation)
               .ThenInclude(r => r.Rooms)
       .FirstOrDefaultAsync(m => m.Id == id);
           // var guest = await _context.Guests.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
