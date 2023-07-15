namespace Prison.Models {

    public enum Cuisine {
        Polish,
        French,
        Italian
    }

    public class Cook : Employee {
        public Cuisine Cuisine { get; set; }
    }
}
