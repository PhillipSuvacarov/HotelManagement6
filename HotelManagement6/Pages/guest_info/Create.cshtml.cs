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
        public async Task<IActionResult> OnPostAsync([FromForm] int roomNumber, DateTime checkinDate, DateTime checkoutDate)
        {
          if (!ModelState.IsValid || _context.Guests == null || Guest == null)
            {
                return Page();
            }

            _context.Guests.Add(Guest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
