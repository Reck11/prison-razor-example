namespace Prison.Models {
    
    public enum Relationship {
        Father,
        Brother,
        Mother,
        Sister,
        Uncle
    }
    
    public class PrisonerVisitorRelation {
        public Prisoner Prisoner { get; set; }
        public Visitor Visitor { get; set; }
        public Relationship Relationship { get; set; }
    }
}
