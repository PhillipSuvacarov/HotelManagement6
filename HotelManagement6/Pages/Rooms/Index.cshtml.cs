using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;
using System.Configuration;
using System.Data;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagement6.Pages.Rooms
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public IndexModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
            
            if (_context.Rooms != null)
            {
                Room = await _context.Rooms
                .Include(r => r.RoomType).ToListAsync();
            }
        }
    }
}
