using System.ComponentModel.DataAnnotations;

namespace Prison.Models {
    
    public enum QualificationsType {
        Psychologist = 1,
        [Display(Name="Math Teacher")]
        MathTeacher = 2,
        [Display(Name = "Workshop Teacher")]
        WorkshopTeacher = 3,
    }
    
    public class ReeducationStaff : Employee {

        [Required]
        [Display(Name ="Typ kwalifikacji")]
        public QualificationsType QualificationsType { get; set; }
        public ICollection<ReeducationProgram>? Programs { get; set; }

    }

    public class StaffAssignedProgram {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}
