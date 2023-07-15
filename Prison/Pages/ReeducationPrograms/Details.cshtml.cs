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
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public ReeducationProgram ReeducationProgram { get; set; } = default!; 
        public List<ReeducationStaff> ReeducationStaff { get; set; }
        public List<ReeducationMeeting> Meetings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ReeducationProgram == null)
            {
                return NotFound();
            }

            var reeducationprogram = await _context.ReeducationProgram
                .Include(x => x.ReeducationStaff)
                .Include(x => x.Meetings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reeducationprogram == null)
            {
                return NotFound();
            }
            else 
            {
                ReeducationProgram = reeducationprogram;
                if (ReeducationProgram.ReeducationStaff != null) {
                    ReeducationStaff = new(ReeducationProgram.ReeducationStaff);
                }
                else {
                    ReeducationStaff = new();
                }

                if (ReeducationProgram.Meetings != null) {
                    Meetings = new(ReeducationProgram.Meetings);
                }
                else {
                    Meetings = new();
                }
            }
            return Page();
        }
    }
}
