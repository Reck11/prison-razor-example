using Microsoft.AspNetCore.Mvc.RazorPages;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.ReeducationPrograms {
    public class ProgramPageModel : PageModel {
        public List<ProgramAssignedStaff> ProgramAssignedStaff { get; set; }

        public void PopulateProgramAssignedStaff(PrisonContext context, ReeducationProgram program) {
            var allStaff = context.ReeducationStaff;
            var programStaff = new HashSet<int>();
            if (program.ReeducationStaff != null) {
                programStaff = new HashSet<int>(program.ReeducationStaff.Select(c => c.Id));
            }

            ProgramAssignedStaff = new();
            foreach (var staff in allStaff) {
                ProgramAssignedStaff.Add(new ProgramAssignedStaff {
                    StaffId = staff.Id,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Assigned = programStaff.Contains(staff.Id)
                });
            }
        }
    }
}
