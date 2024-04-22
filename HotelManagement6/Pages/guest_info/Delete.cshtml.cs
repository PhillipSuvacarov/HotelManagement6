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


            

            // remove reservation from reservation table finding the id
            var reservations = _context.Guestreservationascs.Where(x => x.GuestId == id);

            _context.Guestreservationascs.RemoveRange(reservations);

           

            // removed elements from reservationpayments

            var reservationpayment = _context.Reservationpayments.Where(x => x.ReservationId == id);

            _context.Reservationpayments.RemoveRange(reservationpayment);

           

            // remove elements from reservation table

            var reservationsID = _context.Reservations.Where(x => x.ReservationId == id);

            _context.Reservations.RemoveRange(reservationsID);

           


            var guest = await _context.Guests.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (guest != null)

            {

                _context.Guests.Remove(guest);

            }

            _context.SaveChanges();

            return RedirectToPage("./Index");

        }

    }

}