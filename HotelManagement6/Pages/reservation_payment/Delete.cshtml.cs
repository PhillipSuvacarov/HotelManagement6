using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.reservation_payment
{
    public class DeleteModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public DeleteModel(HotelManagement6.Models.HmContext context)
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

            var reservationpayment = await _context.Reservationpayments.FirstOrDefaultAsync(m => m.Id == id);

            if (reservationpayment == null)
            {
                return NotFound();
            }
            else 
            {
                Reservationpayment = reservationpayment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Reservationpayments == null)
            {
                return NotFound();
            }
            var reservationpayment = await _context.Reservationpayments.FindAsync(id);

            if (reservationpayment != null)
            {
                Reservationpayment = reservationpayment;
                _context.Reservationpayments.Remove(Reservationpayment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
