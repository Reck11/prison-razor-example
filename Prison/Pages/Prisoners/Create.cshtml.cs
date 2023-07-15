using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Prisoners
{
    public class CreateModel : PrisonerPage
    {
        private readonly Prison.Data.PrisonContext _context;

        public CreateModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CellId"] = new SelectList(_context.Cell, "Id", "Id");
        ViewData["CrimeId"] = new SelectList(_context.Crime, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Prisoner Prisoner { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Prisoner == null || Prisoner == null)
            {
                return Page();
            }

            var cell = Prisoner.Cell;

            MovePrisonerToCell(_context, Prisoner, Prisoner.Cell);

            _context.Prisoner.Add(Prisoner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
