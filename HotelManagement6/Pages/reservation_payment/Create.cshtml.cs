using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.reservation_payment
{
    public class CreateModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public CreateModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }
        private bool IsValidPaymentDetails(string paymentDetails)
        {
            // Define your desired regex pattern
            var regex = new System.Text.RegularExpressions.Regex(@"^\(\d{4}-\d{4}-\d{4}\) \(\d{2}/\d{2}\) \(\d{3}\)$");
            return regex.IsMatch(paymentDetails);
        }
        public IActionResult OnGet(decimal? price)
        {
            if (TempData.ContainsKey("ReservationId"))
            {
                int reservationId = (int)TempData["ReservationId"];
                Reservationpayment = new Reservationpayment
                {
                    ReservationId = reservationId,
                    Amount = price ?? 0 // Set the Amount property using the price parameter
                };
            }

            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationId", "ReservationId");
            return Page();
        }

        [BindProperty]
        public Reservationpayment Reservationpayment { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            Reservationpayment.Reservation = _context.Reservations.Where(x => x.ReservationId == Reservationpayment.ReservationId)
                .FirstOrDefault();
            ;TryValidateModel(Reservationpayment);
          if (!ModelState.IsValid || _context.Reservationpayments == null || Reservationpayment == null)
            {
                return Page();
            }

            //if (!IsValidPaymentDetails(Reservationpayment.PaymentDetails))
            //{
            //    ModelState.AddModelError("Reservationpayment.PaymentDetails", "Payment details must be in the format (1234-4567-8901) (mm/dd) (CVC)");
            //    return Page();
            //}
            _context.Reservationpayments.Add(Reservationpayment);
            await _context.SaveChangesAsync();
            if (User.IsInRole("Admin")) 

            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Privacy");
            }

                    
        }
        
    }
}
