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
    public class DeleteModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DeleteModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ReeducationStaff ReeducationStaff { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ReeducationStaff == null)
            {
                return NotFound();
            }

            var reeducationstaff = await _context.ReeducationStaff.FirstOrDefaultAsync(m => m.Id == id);

            if (reeducationstaff == null)
            {
                return NotFound();
            }
            else 
            {
                ReeducationStaff = reeducationstaff;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ReeducationStaff == null)
            {
                return NotFound();
            }
            var reeducationStaff = await _context.ReeducationStaff.Include(x => x.Programs).SingleAsync(x => x.Id == id);

            if (reeducationStaff != null)
            {

                if (reeducationStaff.Programs != null) {
                    var programs = _context.ReeducationProgram.Where(x => reeducationStaff.Programs.Contains(x)).ToList();
                    programs.ForEach(program => {
                        if (program.ReeducationStaff.Contains(reeducationStaff)) {
                            program.ReeducationStaff.Remove(reeducationStaff);
                        }
                    });
                }

                ReeducationStaff = reeducationStaff;
                _context.ReeducationStaff.Remove(ReeducationStaff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
