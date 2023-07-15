using Prison.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Prison.Data;

namespace Prison.Pages.ReeducationStaffs {
    public class StaffProgramsPageModel : PageModel {
        public List<StaffAssignedProgram> StaffAssignedPrograms;
        
        public void PopulateStaffAsignedPrograms(PrisonContext context, ReeducationStaff reeducationStaff) {
            var allPrograms = context.ReeducationProgram;
            var staffPrograms = new HashSet<int>();
            if (reeducationStaff.Programs != null) {
                staffPrograms = new HashSet<int>(reeducationStaff.Programs.Select(c => c.Id));
            }

            StaffAssignedPrograms = new();
            foreach (var program in allPrograms) {
                StaffAssignedPrograms.Add(new StaffAssignedProgram {
                    ProgramId = program.Id,
                    Name = program.Name,
                    Assigned = staffPrograms.Contains(program.Id)
                });
            }
        }
    }
}
