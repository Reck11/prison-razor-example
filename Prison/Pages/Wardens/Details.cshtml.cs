using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Wardens
{
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

      public Warden Warden { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Warden == null)
            {
                return NotFound();
            }

            var warden = await _context.Warden.FirstOrDefaultAsync(m => m.Id == id);
            if (warden == null)
            {
                return NotFound();
            }
            else 
            {
                Warden = warden;
            }
            return Page();
        }
    }
}
