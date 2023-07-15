using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.CellBlocks
{
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

      public CellBlock CellBlock { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CellBlock == null)
            {
                return NotFound();
            }

            var cellblock = await _context.CellBlock.FirstOrDefaultAsync(m => m.Id == id);
            if (cellblock == null)
            {
                return NotFound();
            }
            else 
            {
                CellBlock = cellblock;
            }
            return Page();
        }
    }
}
