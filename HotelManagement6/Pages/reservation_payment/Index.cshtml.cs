using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagement6.Pages.reservation_payment
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public IndexModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        public IList<Reservationpayment> Reservationpayment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Reservationpayments != null)
            {
                Reservationpayment = await _context.Reservationpayments
                 .Include(r => r.Reservation)
        .ThenInclude(gra => gra.Guestreservationascs)
        .ThenInclude(g => g.Guest)
    .Include(r => r.Reservation.Rooms) 
    .ToListAsync();
            }
        }
    }
}
