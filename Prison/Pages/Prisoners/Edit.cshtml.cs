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

namespace Prison.Pages.Prisoners
{
    public class EditModel : PrisonerPage {
        private readonly Prison.Data.PrisonContext _context;

        public EditModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prisoner Prisoner { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Prisoner == null)
            {
                return NotFound();
            }

            var prisoner =  await _context.Prisoner.FirstOrDefaultAsync(m => m.Id == id);
            if (prisoner == null)
            {
                return NotFound();
            }
            Prisoner = prisoner;
           ViewData["CellId"] = new SelectList(_context.Cell, "Id", "Id");
           ViewData["CrimeId"] = new SelectList(_context.Crime, "Id", "Name");
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

            _context.Attach(Prisoner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrisonerExists(Prisoner.Id))
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

        private bool PrisonerExists(int id)
        {
          return (_context.Prisoner?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
