namespace Prison.Models {
    public class Visitor : Person {
        public string Address { get; set; }
        public ICollection<Prisoner>? Prisoners { get; set; }
        public ICollection<PrisonerVisitorRelation>? PrisonerVisitorRelations { get; set; }
    }
}
