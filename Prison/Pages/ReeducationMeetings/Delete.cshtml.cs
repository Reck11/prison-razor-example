using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.ReeducationMeetings
{
    public class DeleteModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DeleteModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ReeducationMeeting ReeducationMeeting { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ReeducationMeeting == null)
            {
                return NotFound();
            }

            var reeducationmeeting = await _context.ReeducationMeeting.FirstOrDefaultAsync(m => m.Id == id);

            if (reeducationmeeting == null)
            {
                return NotFound();
            }
            else 
            {
                ReeducationMeeting = reeducationmeeting;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ReeducationMeeting == null)
            {
                return NotFound();
            }
            var reeducationmeeting = await _context.ReeducationMeeting.FindAsync(id);

            if (reeducationmeeting != null)
            {
                ReeducationMeeting = reeducationmeeting;
                _context.ReeducationMeeting.Remove(ReeducationMeeting);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
