using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.guest_info
{
    public class DeleteModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public DeleteModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Guest Guest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.Id == id);

            if (guest == null)
            {
                return NotFound();
            }
            else 
            {
                Guest = guest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Guests == null)
            {
                return NotFound();
            }
            var guest = await _context.Guests.FindAsync(id);

            if (guest != null)
            {
                Guest = guest;
                _context.Guests.Remove(Guest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
