using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagement6.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace HotelManagement6.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly HotelManagement6.Models.HmContext _context;
        public readonly List<SelectListItem> roomtypes;


        public CreateModel(HotelManagement6.Models.HmContext context)
        {
            _context = context;
            roomtypes = _context.Roomtypes.Select(x => new SelectListItem
            {
                Value = x.RoomTypeId.ToString(),
                Text = x.RoomTypeId.ToString()
            }).ToList();
        }

        public IActionResult OnGet()
        {
        ViewData["RoomTypeId"] = new SelectList(_context.Roomtypes, "RoomTypeId", "RoomTypeId");
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            Room.RoomType = _context.Roomtypes.Where(x => x.RoomTypeId == Room.RoomTypeId).FirstOrDefault();
            TryValidateModel(Room);
            ;
            var check = ModelState.IsValid;
            if (!ModelState.IsValid || _context.Rooms == null || Room == null)
            {
                return Page();
            }

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
