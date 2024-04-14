using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.guest_info
{
    public class EditModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public EditModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guest Guest { get; set; } = default!;

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        [BindProperty]
        public Room Room { get; set; } = default!;
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
            if (guest == null)
            {
                return NotFound();
            }
            Guest = guest;
            if (guest.Guestreservationascs.Any())
            {
                Reservation = guest.Guestreservationascs.First().Reservation;
                Room = Reservation.Rooms.FirstOrDefault();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Guest).State = EntityState.Modified;
            if (Reservation != null)
            {
                _context.Attach(Reservation).State = EntityState.Modified;
            }
            if (Room != null)
            {
                _context.Attach(Room).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(Guest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GuestExists(int id)
        {
          return (_context.Guests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
