using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;
using Microsoft.AspNetCore.Identity;
using HotelManagement6.Areas.Identity.Data;

namespace HotelManagement6.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;
        private readonly UserManager<MySqlIdentityUser> _userManager;

        public IndexModel(HotelManagement6.Models.HmContext context, UserManager<MySqlIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Aspnetuser> Aspnetuser { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Aspnetusers != null)
            {
                Aspnetuser = await _context.Aspnetusers.ToListAsync();
            }
        }
    }
}