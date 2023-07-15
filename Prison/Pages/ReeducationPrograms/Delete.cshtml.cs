using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.ReeducationPrograms
{
    public class DeleteModel : ProgramPageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DeleteModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ReeducationProgram ReeducationProgram { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ReeducationProgram == null)
            {
                return NotFound();
            }

            var reeducationprogram = await _context.ReeducationProgram.FirstOrDefaultAsync(m => m.Id == id);

            if (reeducationprogram == null)
            {
                return NotFound();
            }
            else 
            {
                ReeducationProgram = reeducationprogram;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ReeducationProgram == null)
            {
                return NotFound();
            }
            var reeducationprogram = await _context.ReeducationProgram.Include(x => x.ReeducationStaff).SingleAsync(x => x.Id == id);

            if (reeducationprogram != null)
            {

                if (reeducationprogram.ReeducationStaff != null) {
                    var staff = _context.ReeducationStaff.Where(x => reeducationprogram.ReeducationStaff.Contains(x)).ToList();
                    staff.ForEach(x => {
                        if (x.Programs.Contains(reeducationprogram)) {
                            x.Programs.Remove(reeducationprogram);
                        }
                    });
                }

                ReeducationProgram = reeducationprogram;
                _context.ReeducationProgram.Remove(ReeducationProgram);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
