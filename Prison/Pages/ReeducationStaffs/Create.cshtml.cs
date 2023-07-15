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
    public class CreateModel : StaffProgramsPageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public CreateModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var staff = new ReeducationStaff();
            staff.Programs = new List<ReeducationProgram>();

            PopulateStaffAsignedPrograms(_context, staff);
            return Page();
        }

        [BindProperty]
        public ReeducationStaff ReeducationStaff { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedPrograms)
        {

            var newStaff = ReeducationStaff;

            if (selectedPrograms.Length > 0) {
                newStaff.Programs = new List<ReeducationProgram>();
                _context.ReeducationProgram.Load();
            }

            foreach (var program in selectedPrograms) {
                var foundProgram = await _context.ReeducationProgram.FindAsync(int.Parse(program));
                if (foundProgram != null) {
                    newStaff.Programs.Add(foundProgram);
                }
            }

            if (!ModelState.IsValid || _context.ReeducationStaff == null || newStaff == null)
            {
                PopulateStaffAsignedPrograms(_context, newStaff);
                return Page();
            }

            _context.ReeducationStaff.Add(newStaff);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
