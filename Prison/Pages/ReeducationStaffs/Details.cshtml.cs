using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.ReeducationStaffs
{
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public ReeducationStaff ReeducationStaff { get; set; } = default!; 
        public List<ReeducationProgram> ReeducationPrograms { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ReeducationStaff == null)
            {
                return NotFound();
            }

            var reeducationstaff = await _context.ReeducationStaff.Include(x => x.Programs).FirstOrDefaultAsync(m => m.Id == id);
            if (reeducationstaff == null)
            {
                return NotFound();
            }
            else 
            {
                ReeducationStaff = reeducationstaff;
                if (ReeducationStaff.Programs != null) {
                    ReeducationPrograms = new(ReeducationStaff.Programs);
                }
                else {
                    ReeducationPrograms = new();
                }
            }
            return Page();
        }
    }
}
