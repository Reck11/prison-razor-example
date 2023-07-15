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
    public class DetailsModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public DetailsModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public ReeducationMeeting ReeducationMeeting { get; set; } = default!; 
        public List<Prisoner> Prisoners { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ReeducationMeeting == null)
            {
                return NotFound();
            }

            var reeducationmeeting = await _context.ReeducationMeeting.Include(x => x.Prisoners).FirstOrDefaultAsync(m => m.Id == id);
            if (reeducationmeeting == null)
            {
                return NotFound();
            }
            else 
            {
                ReeducationMeeting = reeducationmeeting;
                if (ReeducationMeeting.Prisoners != null) {
                    Prisoners = new(ReeducationMeeting.Prisoners);
                }
                else {
                    Prisoners = new();
                }
            }
            return Page();
        }
    }
}
