using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Prison.Models {
    public class ReeducationMeeting {

        public int Id { get; set; }
        public DateTime Time { get; set; }
        [Display(Name = "Maks. liczba więźniów")]
        public int MaximumNumberOfPrisoners { get; set; }
        [ForeignKey("ProgramId")]
        public ReeducationProgram? Program { get; set; }
        public int ProgramId { get; set; }
        public ICollection<Prisoner>? Prisoners { get; set; }

        public bool CanAddPrisoner() {
            if (Prisoners.Count < MaximumNumberOfPrisoners) {
                return true;
            }

            return false;
        }
    }

    public class MeetingAssignedPrisoner {
        public int PrisonerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Assigned { get; set; }
    }
}
