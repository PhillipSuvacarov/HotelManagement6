using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement6.Models;

namespace HotelManagement6.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;

        public EditModel(HotelManagement6.Models.HmContext context)
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

            var aspnetuser =  await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Id == id);
            if (aspnetuser == null)
            {
                return NotFound();
            }
            Aspnetuser = aspnetuser;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Aspnetuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspnetuserExists(Aspnetuser.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AspnetuserExists(string id)
        {
          return (_context.Aspnetusers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
