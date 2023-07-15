using Microsoft.AspNetCore.Mvc.RazorPages;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.ReeducationMeetings {
    public class MeetingPageModel : PageModel {
        public List<MeetingAssignedPrisoner> MeetingAssignedPrisoners;

        public void PopulateMeetingAssignedPrisoners(PrisonContext context, ReeducationMeeting reeducationMeeting) {
            var allPrisoners = context.Prisoner;
            var meetingPrisoners = new HashSet<int>();
            if (reeducationMeeting.Prisoners != null) {
                meetingPrisoners = new HashSet<int>(reeducationMeeting.Prisoners.Select(c => c.Id));
            }

            MeetingAssignedPrisoners = new();
            foreach (var prisoner in allPrisoners) {
                MeetingAssignedPrisoners.Add(new MeetingAssignedPrisoner {
                    PrisonerId = prisoner.Id,
                    FirstName = prisoner.FirstName,
                    LastName = prisoner.LastName,
                    Assigned = meetingPrisoners.Contains(prisoner.Id)
                });
            }
        }

        public static bool IsDateTaken(PrisonContext context, DateTime date) {
            var allMeetings = context.ReeducationMeeting;
            DateTime dateOnly = date.Date;
            if (allMeetings.Any(x => x.Time.Date == dateOnly)) {
                return true;
            }
            return false;
        }
    }
}
