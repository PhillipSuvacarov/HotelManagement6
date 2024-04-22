using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement6.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagement6.Pages.guest_info
{
    

    


public class CreateModel : PageModel
    {
        
        private readonly HotelManagement6.Models.HmContext _context;
        
        public CreateModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            return Page();
        }


        [BindProperty]
        public Guest Guest { get; set; } = default!;

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync([FromForm] int roomNumber, [FromForm] DateTime checkinDate, 
            [FromForm] DateTime checkoutDate, [FromForm] decimal price)
        {
          if (!ModelState.IsValid || _context.Guests == null || Guest == null)
            {
                return Page();
            }

            // checks for user age and if they are at least 18
            DateTime currentDate = DateTime.Today;
            DateTime dob = Convert.ToDateTime(Guest.DateOfBirth);
            int age = currentDate.Year - dob.Year;

            if (currentDate.Month < dob.Month || (currentDate.Month == dob.Month && currentDate.Day < dob.Day))
                age--;

            if (age < 18)
            {
                ModelState.AddModelError("Guest.DateOfBirth", "Guest must be at least 18 years old.");
                return Page();
            }

            //creates reservation instance to pull checkin and checkout dates
            Reservation r = new Reservation();
            r.CheckIn = (checkinDate);
            r.CheckOut = (checkoutDate);

            // this gets the contents from room
            var room = _context.Rooms.Where(x => x.RoomNumber == roomNumber).FirstOrDefault();
            

            if (room != null) {
                r.Rooms.Add(room);
                r.Price = room.Price;
            }
            else
            {
                return NotFound();
            }




           
            TempData["roomNumber"] = roomNumber;
            TempData["CheckinDate"] = checkinDate;
            TempData["CheckoutDate"] = checkoutDate;
            
            _context.Reservations.Add(r);
            _context.Guests.Add(Guest);
            await _context.SaveChangesAsync();

            // Now that guest and reservation Id has been made add it to join table
            int guestId = Guest.Id; 
            int reservationId = r.ReservationId; 

            // Create a new GuestReservationAsc object
            Guestreservationasc guestReservation = new Guestreservationasc
            {
                GuestId = guestId,
                ReservationId = reservationId
            };
            //Add context to guest
            _context.Guestreservationascs.Add(guestReservation);
            await _context.SaveChangesAsync();
            TempData["ReservationId"] = reservationId;
            var prices = HttpContext.Request.Query["price"];


            return RedirectToPage("/reservation_payment/create",new {prices = price, reservationId = reservationId }); 
        }
    }
}
