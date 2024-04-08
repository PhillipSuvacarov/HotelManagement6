using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement6.Models;

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
        public async Task<IActionResult> OnPostAsync([FromForm] int roomNumber, [FromForm] DateTime checkinDate, [FromForm] DateTime checkoutDate, [FromForm] decimal price)
        {
          if (!ModelState.IsValid || _context.Guests == null || Guest == null)
            {
                return Page();
            }
            


            Reservation r = new Reservation();
            r.CheckIn = DateOnly.FromDateTime(checkinDate);
            r.CheckOut = DateOnly.FromDateTime(checkoutDate);
            




            // tihs gets the contents from room
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
            //this does not work becasue tempdata does not support decimal type
            //TempData["price"] = r.Price;
            _context.Reservations.Add(r);
            _context.Guests.Add(Guest);
            await _context.SaveChangesAsync();

            // Now that guest and reservation Id has been made add it to join table
            int guestId = Guest.Id; 
            int reservationId = r.Id; 

            // Create a new GuestReservationAsc object
            Guestreservationasc guestReservation = new Guestreservationasc
            {
                GuestId = guestId,
                ReservationId = reservationId
            };
            _context.Guestreservationascs.Add(guestReservation);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index"); // Pass the room number to the Index page
        }
    }
}
