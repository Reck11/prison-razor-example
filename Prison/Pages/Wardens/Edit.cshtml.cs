using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Wardens
{
    public class EditModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public EditModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Warden Warden { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Warden == null)
            {
                return NotFound();
            }

            var warden =  await _context.Warden.FirstOrDefaultAsync(m => m.Id == id);
            if (warden == null)
            {
                return NotFound();
            }
            Warden = warden;
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

            _context.Attach(Warden).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WardenExists(Warden.Id))
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

        private bool WardenExists(int id)
        {
          return (_context.Warden?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
