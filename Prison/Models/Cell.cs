namespace Prison.Models {
    public class Cell {
        public int Id { get; set; }
        public int MaximumCapacity { get; set; }
        public ICollection<Prisoner> Prisoners { get; set; }
        public CellBlock CellBlock { get; set; }

        public bool CanAddPrisoner() {
            if (Prisoners.Count < MaximumCapacity) {
                return true;
            }

            return false;
        }
    }
}
