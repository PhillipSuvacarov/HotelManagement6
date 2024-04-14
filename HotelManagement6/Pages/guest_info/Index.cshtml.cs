using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagement6.Pages.guest_info
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public IndexModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        public IList<Guest> Guest { get;set; } = default!;
        public IList<Reservation> Reservations { get; set; } = default!;
        public IList<Guestreservationasc> GuestReservations { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (!User.IsInRole("Admin"))

            {
                 RedirectToPage("/Index");
            }
            if (_context.Guests != null)
            {
               

                Guest = await _context.Guests
                .Include(g => g.Guestreservationascs)
                .ThenInclude(gra => gra.Reservation)
                    .ThenInclude(r => r.Rooms)
            .ToListAsync();



            }
            
        }  

             
    }
}
