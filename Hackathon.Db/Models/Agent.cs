namespace Hackathon.Db.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public List<Call>? Calls { get; set; }
    }
}
