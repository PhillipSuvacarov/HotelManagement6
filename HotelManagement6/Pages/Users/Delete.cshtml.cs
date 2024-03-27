using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public DeleteModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Aspnetuser Aspnetuser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Aspnetusers == null)
            {
                return NotFound();
            }

            var aspnetuser = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Id == id);

            if (aspnetuser == null)
            {
                return NotFound();
            }
            else 
            {
                Aspnetuser = aspnetuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Aspnetusers == null)
            {
                return NotFound();
            }
            var aspnetuser = await _context.Aspnetusers.FindAsync(id);

            if (aspnetuser != null)
            {
                Aspnetuser = aspnetuser;
                _context.Aspnetusers.Remove(Aspnetuser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
