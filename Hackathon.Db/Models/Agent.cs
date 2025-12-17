namespace Hackathon.Db.Models
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public List<Call>? Calls { get; set; }
    }
}
