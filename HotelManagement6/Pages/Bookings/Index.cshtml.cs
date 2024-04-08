using HotelManagement6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement6.Pages.Bookings
{


    
    
    public class IndexModel : PageModel
    {
        private readonly HmContext _context;

        public IndexModel(HmContext context)
        {
            _context = context;
            CheckInDate = DateTime.Today;
            CheckOutDate = DateTime.Today.AddDays(1);
        }
        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }
        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }

        public int roomNumber {  get; set; }

        public decimal Price {  get; set; }

        public IEnumerable<Room> AvailableRooms { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            AvailableRooms = GetAvailableRooms(roomNumber, CheckInDate, CheckOutDate, Price);

            // Pass the available rooms to the view or perform other operations
            return Page();
        }
        private IEnumerable<Room> GetAvailableRooms(int roomNumber, DateTime checkInDate, DateTime checkOutDate, decimal Price)
        {
            var allRoomsAndPrices = _context.Rooms
                .ToList();


            return allRoomsAndPrices;
           //  When I connect table properly then use 
           //       var bookedRoomIds = context.Bookings
           // .Where(b => b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)
           //.Select(b => b.RoomId)
           // .ToList();

           //       var availableRooms = allRoomsAndPrices
           //           .Where(r => !bookedRoomIds.Contains(r.RoomId))
           //          .ToList();

           //      return availableRooms;

        }
    }
}
