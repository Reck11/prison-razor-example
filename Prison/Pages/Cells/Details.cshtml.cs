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
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

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
    }
}
