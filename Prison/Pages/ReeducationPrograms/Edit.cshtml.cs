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

namespace Prison.Pages.ReeducationPrograms
{
    public class EditModel : ProgramPageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public EditModel(Prison.Data.PrisonContext context)
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

            var reeducationprogram =  await _context.ReeducationProgram
                .Include(x => x.ReeducationStaff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reeducationprogram == null)
            {
                return NotFound();
            }
            ReeducationProgram = reeducationprogram;
            PopulateProgramAssignedStaff(_context, ReeducationProgram);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedStaff)
        {

            if (id == null) {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var programToUpdate = await _context.ReeducationProgram
                .Include(x => x.ReeducationStaff)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (programToUpdate == null) {
                return NotFound();
            }

            _context.Attach(programToUpdate).State = EntityState.Modified;

            try
            {
                UpdateProgramStaff(selectedStaff, programToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReeducationProgramExists(programToUpdate.Id))
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

        public void UpdateProgramStaff(string[] selectedStaff, ReeducationProgram programToUpdate) {
            if (selectedStaff == null) {
                programToUpdate.ReeducationStaff = new List<ReeducationStaff>();
                return;
            }

            Console.WriteLine(selectedStaff.Count());

            if (programToUpdate.ReeducationStaff == null) {
                programToUpdate.ReeducationStaff = new List<ReeducationStaff>();
            }

            var programStaff = new HashSet<int>(programToUpdate.ReeducationStaff.Select(c => c.Id));

            foreach (var staff in _context.ReeducationStaff) {
                if (selectedStaff.Contains(staff.Id.ToString())) {
                    if (!programStaff.Contains(staff.Id)) {
                        programToUpdate.ReeducationStaff.Add(staff);
                    }
                }
                else {
                    if (programStaff.Contains(staff.Id)) {
                        var staffToRemove = programToUpdate.ReeducationStaff.First(x => x.Id == staff.Id);
                        programToUpdate.ReeducationStaff.Remove(staffToRemove);
                    }
                }
            }
        }

        private bool ReeducationProgramExists(int id)
        {
          return (_context.ReeducationProgram?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
