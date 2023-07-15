namespace Prison.Models {
    
    public enum JobType {
        Laundry,
        Stonequarry
    }
    
    public class Job {
        public int Id { get; set; }
        public JobType JobType { get; set; }
        public int DailyWage { get; set; }
        public ICollection<Prisoner>? Prisoners { get; set; }
    }
}
