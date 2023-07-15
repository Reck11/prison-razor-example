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

namespace Prison.Pages.ReeducationStaffs
{
    public class EditModel : StaffProgramsPageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public EditModel(Prison.Data.PrisonContext context)
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

            var reeducationstaff = await _context.ReeducationStaff
                .Include(x => x.Programs)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reeducationstaff == null)
            {
                return NotFound();
            }
            ReeducationStaff = reeducationstaff;
            PopulateStaffAsignedPrograms(_context, ReeducationStaff);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedPrograms)
        {

            if (id == null) {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var staffToUpdate = await _context.ReeducationStaff
                .Include(x => x.Programs)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (staffToUpdate == null) {
                return NotFound();
            }

            _context.Attach(staffToUpdate).State = EntityState.Modified;

            try
            {
                UpdateStaffPrograms(selectedPrograms, staffToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReeducationStaffExists(staffToUpdate.Id))
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

        public void UpdateStaffPrograms(string[] selectedPrograms, ReeducationStaff staffToUpdate) {
            if (selectedPrograms == null) {
                staffToUpdate.Programs = new List<ReeducationProgram>(); 
                return;
            }

            Console.WriteLine(selectedPrograms.Count());

            if (staffToUpdate.Programs == null) {
                staffToUpdate.Programs = new List<ReeducationProgram>();
            }

            var staffPrograms = new HashSet<int>(staffToUpdate.Programs.Select(c => c.Id));

            foreach (var program in _context.ReeducationProgram) {
                if (selectedPrograms.Contains(program.Id.ToString())) {
                    if (!staffPrograms.Contains(program.Id)) {
                        staffToUpdate.Programs.Add(program);
                    }
                }
                else {
                    if (staffPrograms.Contains(program.Id)) {
                        var programToRemove = staffToUpdate.Programs.First(x => x.Id == program.Id);
                        staffToUpdate.Programs.Remove(programToRemove);
                    }
                }
            }
        }

        private bool ReeducationStaffExists(int id)
        {
          return (_context.ReeducationStaff?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
