namespace Prison.Models {

    public class Crime {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int MinimalPunishment { get; set; }
        public int SecurityLevel { get; set; } = 1;
    }
}
