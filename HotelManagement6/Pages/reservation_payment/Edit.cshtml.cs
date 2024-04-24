using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.reservation_payment
{
    public class EditModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public EditModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservationpayment Reservationpayment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reservationpayments == null)
            {
                return NotFound();
            }

            var reservationpayment =  await _context.Reservationpayments.FirstOrDefaultAsync(m => m.Id == id);
            if (reservationpayment == null)
            {
                return NotFound();
            }
            Reservationpayment = reservationpayment;
           ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            Reservationpayment.Reservation = _context.Reservations.Where(x => x.ReservationId == Reservationpayment.ReservationId)
                .FirstOrDefault();
            ; TryValidateModel(Reservationpayment);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Reservationpayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationpaymentExists(Reservationpayment.Id))
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

        private bool ReservationpaymentExists(int id)
        {
          return (_context.Reservationpayments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
