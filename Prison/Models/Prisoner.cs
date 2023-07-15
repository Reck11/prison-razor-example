using System.ComponentModel.DataAnnotations.Schema;

namespace Prison.Models {
    public class Prisoner : Person {
        public int RegistrationNumber { get; set; }
        public int SecurityLevel { get => Crime.SecurityLevel; }
        public DateTime ImprisonmentStartDate { get; set; }
        public DateTime ImprisonmentEndDate { get; set; }
        [ForeignKey("CrimeId")]
        public Crime Crime { get; set; }
        public int CrimeId { get; set; }
        [ForeignKey("CellId")]
        public Cell Cell { get; set; }
        public int CellId { get; set; }
        public ICollection<ReeducationMeeting>? Meetings { get; set; }
        public ICollection<Visitor>? Visitors { get; set; }
        public ICollection<PrisonerVisitorRelation>? PrisonerVisitorRelations { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }
}
