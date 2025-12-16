namespace Hackathon.Db.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public bool IsActive { get; set; }

        public int TotalCalls { get; set; }
        public double AverageRating { get; set; }

        public List<Call>? Calls { get; set; }
    }
}
