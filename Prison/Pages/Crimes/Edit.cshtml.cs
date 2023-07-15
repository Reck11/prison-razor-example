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

namespace Prison.Pages.Crimes
{
    public class EditModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public EditModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Crime Crime { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Crime == null)
            {
                return NotFound();
            }

            var crime =  await _context.Crime.FirstOrDefaultAsync(m => m.Id == id);
            if (crime == null)
            {
                return NotFound();
            }
            Crime = crime;
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

            _context.Attach(Crime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrimeExists(Crime.Id))
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

        private bool CrimeExists(int id)
        {
          return (_context.Crime?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
