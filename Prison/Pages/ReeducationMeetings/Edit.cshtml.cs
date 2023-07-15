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
    public class EditModel : MeetingPageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public EditModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReeducationMeeting ReeducationMeeting { get; set; } = default!;

        public SelectList ProgramNameSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ReeducationMeeting == null)
            {
                return NotFound();
            }

            var reeducationmeeting =  await _context.ReeducationMeeting
                .Include(x => x.Program)
                .Include(x => x.Prisoners)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reeducationmeeting == null)
            {
                return NotFound();
            }
            ReeducationMeeting = reeducationmeeting;
            PopulateMeetingAssignedPrisoners(_context, ReeducationMeeting);
            PopulateProgramDropDown(_context, ReeducationMeeting.ProgramId);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedPrisoners)
        {

            if (id == null) {
                return NotFound();
            }

            var meetingToUpdate = await _context.ReeducationMeeting
                .Include(x => x.Program)
                .Include(x => x.Prisoners)
                .FirstOrDefaultAsync(x => x.Id == id);

            meetingToUpdate.Program = await _context.ReeducationProgram.FindAsync(ReeducationMeeting.ProgramId);

            if (meetingToUpdate == null) {
                return NotFound();
            }

            if (IsDateTaken(_context, ReeducationMeeting.Time)) {
                ModelState.AddModelError(string.Empty, "The date is taken.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                PopulateProgramDropDown(_context, meetingToUpdate.ProgramId);
                return Page();
            }

            _context.Attach(meetingToUpdate).State = EntityState.Modified;

            try
            {
                UpdateMeetingPrisoners(selectedPrisoners, meetingToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReeducationMeetingExists(meetingToUpdate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public void UpdateMeetingPrisoners(string[] selectedPrisoners, ReeducationMeeting meetingToUpdate) {
            if (selectedPrisoners == null) {
                meetingToUpdate.Prisoners = new List<Prisoner>();
                return;
            }

            Console.WriteLine(selectedPrisoners.Count());

            if (meetingToUpdate.Prisoners == null) {
                meetingToUpdate.Prisoners = new List<Prisoner>();
            }

            var meetingPrisoners = new HashSet<int>(meetingToUpdate.Prisoners.Select(c => c.Id));

            foreach (var prisoner in _context.Prisoner) {
                if (selectedPrisoners.Contains(prisoner.Id.ToString())) {
                    if (!meetingPrisoners.Contains(prisoner.Id)) {
                        meetingToUpdate.Prisoners.Add(prisoner);
                    }
                }
                else {
                    if (meetingPrisoners.Contains(prisoner.Id)) {
                        var programToRemove = meetingToUpdate.Prisoners.First(x => x.Id == prisoner.Id);
                        meetingToUpdate.Prisoners.Remove(programToRemove);
                    }
                }
            }
        }

        public void PopulateProgramDropDown(PrisonContext _context, object selected = null) {
            var programsQuery = _context.ReeducationProgram.OrderBy(x => x.Name);

            ProgramNameSL = new SelectList(programsQuery, nameof(ReeducationProgram.Id), nameof(ReeducationProgram.Name), selected);
        }

        private bool ReeducationMeetingExists(int id)
        {
          return (_context.ReeducationMeeting?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
