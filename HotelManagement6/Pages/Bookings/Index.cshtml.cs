using HotelManagement6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement6.Pages.Bookings
{



    public class IndexModel : PageModel
    {
        [BindProperty]
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public IEnumerable<Room> AvailableRooms { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
             AvailableRooms = await GetAvailableRooms(CheckInDate, CheckOutDate);

            // Pass the available rooms to the view or perform other operations
            return Page();
        }
        private async Task<IEnumerable<Room>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            using (var context = new HmContext())
            {
                var allRoomsAndPrices = context.Rooms
                    .ToList();
           

                return allRoomsAndPrices;
                // When I connect table properly then use 
                //     var bookedRoomIds = context.Bookings
                //.Where(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)
                //.Select(b => b.RoomId)
                //.ToList();

                //     var availableRooms = allRoomsAndPrices
                //         .Where(r => !bookedRoomIds.Contains(r.RoomId))
                //         .ToList();

                //     return availableRooms;
            }
        }
    }
}
