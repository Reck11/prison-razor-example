using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Crimes
{
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

      public Crime Crime { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Crime == null)
            {
                return NotFound();
            }

            var crime = await _context.Crime.FirstOrDefaultAsync(m => m.Id == id);
            if (crime == null)
            {
                return NotFound();
            }
            else 
            {
                Crime = crime;
            }
            return Page();
        }
    }
}
