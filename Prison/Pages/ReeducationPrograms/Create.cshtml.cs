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
using Prison.Pages.ReeducationStaffs;

namespace Prison.Pages.ReeducationPrograms
{
    public class CreateModel : ProgramPageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public CreateModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var program = new ReeducationProgram();
            program.ReeducationStaff = new List<ReeducationStaff>();

            PopulateProgramAssignedStaff(_context, program);
            return Page();
        }

        [BindProperty]
        public ReeducationProgram ReeducationProgram { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedStaff)
        {

            var newProgram = ReeducationProgram;

            if (selectedStaff.Length > 0) {
                newProgram.ReeducationStaff = new List<ReeducationStaff>();
                _context.ReeducationProgram.Load();
            }

            foreach (var staff in selectedStaff) {
                var foundStaff = await _context.ReeducationStaff.FindAsync(int.Parse(staff));
                if (foundStaff != null) {
                    newProgram.ReeducationStaff.Add(foundStaff);
                }
            }

            if (!ModelState.IsValid || _context.ReeducationProgram == null || ReeducationProgram == null)
            {
                PopulateProgramAssignedStaff(_context, newProgram);
                return Page();
            }

            _context.ReeducationProgram.Add(newProgram);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
