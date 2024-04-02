using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public DeleteModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }
            else 
            {
                Reservation = reservation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation != null)
            {
                Reservation = reservation;
                _context.Reservations.Remove(Reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
