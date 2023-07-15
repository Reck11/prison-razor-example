using System.ComponentModel.DataAnnotations;

namespace Prison.Models {
    public class CellBlock {
        public int Id { get; set; }
        [MaxLength(1)]
        public string Name { get; set; }
        public ICollection<Cell> Cells { get; set; }
        public ICollection<Guard>? Patrols { get; set; }
    }
}
