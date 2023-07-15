namespace Prison.Models {
    
    public enum Weapon {
        Unarmed,
        Nightstick,
        Tazer,
        Pistol
    }

    public class Guard : Employee {
        public Weapon Weapon { get; set; }
        public ICollection<CellBlock> Blocks;
    }
}
