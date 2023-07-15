using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Visitors
{
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

      public Visitor Visitor { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Visitor == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitor.FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }
            else 
            {
                Visitor = visitor;
            }
            return Page();
        }
    }
}
