using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Cells
{
    public class DeleteModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DeleteModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Cell Cell { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cell == null)
            {
                return NotFound();
            }

            var cell = await _context.Cell.FirstOrDefaultAsync(m => m.Id == id);

            if (cell == null)
            {
                return NotFound();
            }
            else 
            {
                Cell = cell;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cell == null)
            {
                return NotFound();
            }
            var cell = await _context.Cell.FindAsync(id);

            if (cell != null)
            {
                Cell = cell;
                _context.Cell.Remove(Cell);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
