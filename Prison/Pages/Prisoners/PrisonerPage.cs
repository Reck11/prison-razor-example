using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Prisoners {
    public class PrisonerPage : PageModel {

        public void MovePrisonerToCell(PrisonContext context, Prisoner prisoner, Cell cell) {

            Prisoner foundPrisoner = context.Prisoner.FirstOrDefaultAsync(x => x.Id == prisoner.Id).Result;
            if (foundPrisoner.Cell == cell) {
                Console.WriteLine("Prisoner is already in that cell");
                return;
            }

            if (cell.CanAddPrisoner()) {
                prisoner.Cell = cell;
                cell.Prisoners.Add(prisoner);
            }
            else {
                ModelState.AddModelError(string.Empty, "The cell is already at maximum capacity.");
            }
        }

        public void AssignToReeducationMeeting(PrisonContext context, Prisoner prisoner, ReeducationMeeting reeducationMeeting) {
            Prisoner foundPrisoner = context.Prisoner.FirstOrDefaultAsync(x => x.Id == prisoner.Id).Result;
            if (foundPrisoner.Meetings.Contains(reeducationMeeting)) {
                Console.WriteLine("Prisoner is already assigned to that meeting");
                return;
            }

            if (reeducationMeeting.CanAddPrisoner()) {
                prisoner.Meetings.Add(reeducationMeeting);
                reeducationMeeting.Prisoners.Add(prisoner);
            }
            else {
                ModelState.AddModelError(string.Empty, "The meeting is already at maximum capacity.");
            }
        }
    }
}
