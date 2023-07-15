using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Prisoners
{
    public class DetailsModel : PrisonerPage {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

      public Prisoner Prisoner { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Prisoner == null)
            {
                return NotFound();
            }

            var prisoner = await _context.Prisoner.FirstOrDefaultAsync(m => m.Id == id);
            if (prisoner == null)
            {
                return NotFound();
            }
            else 
            {
                Prisoner = prisoner;
            }
            return Page();
        }
    }
}
