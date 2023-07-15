namespace Prison.Models {
    public class ReeducationProgram {

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public ICollection<ReeducationMeeting>? Meetings { get; set; }
        public ICollection<ReeducationStaff>? ReeducationStaff { get; set; }
    }

    public class ProgramAssignedStaff {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Assigned { get; set; }
    }
}
