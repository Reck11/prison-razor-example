using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.ReeducationMeetings
{
    public class CreateModel : MeetingPageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public CreateModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public SelectList ProgramNameSL { get; set; }

        public IActionResult OnGet()
        {
            var reeducationMeeting = new ReeducationMeeting();
            reeducationMeeting.Prisoners = new List<Prisoner>();
            PopulateMeetingAssignedPrisoners(_context, reeducationMeeting);
            PopulateProgramDropDown(_context);
            return Page();
        }

        [BindProperty]
        public ReeducationMeeting ReeducationMeeting { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedPrisoners)
        {

            ReeducationMeeting.Program = _context.ReeducationProgram.Find(ReeducationMeeting.ProgramId);
            _context.ReeducationProgram.Load();

            if (selectedPrisoners.Length > 0) {
                ReeducationMeeting.Prisoners = new List<Prisoner>();
                _context.Prisoner.Load();
            }

            foreach (var prisoner in selectedPrisoners) {
                var foundPrisoner = await _context.Prisoner.FindAsync(int.Parse(prisoner));
                if (foundPrisoner != null) {
                    ReeducationMeeting.Prisoners.Add(foundPrisoner);
                }
            }

            if (IsDateTaken(_context, ReeducationMeeting.Time)) {
                ModelState.AddModelError(string.Empty, "The date is taken.");
                return Page();
            }

            if (!ModelState.IsValid || _context.ReeducationMeeting == null || ReeducationMeeting == null)
            {
                PopulateMeetingAssignedPrisoners(_context, ReeducationMeeting);
                PopulateProgramDropDown(_context, ReeducationMeeting);
                return Page();
            }

            _context.ReeducationMeeting.Add(ReeducationMeeting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public void PopulateProgramDropDown(PrisonContext _context, object selected = null) {
            var programsQuery = _context.ReeducationProgram.OrderBy(x => x.Name);

            ProgramNameSL = new SelectList(programsQuery, nameof(ReeducationProgram.Id), nameof(ReeducationProgram.Name), selected);
        }
    }
}
