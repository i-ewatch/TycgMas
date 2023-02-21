namespace TycgMas.Models
{
    public class DAU
    {
        public DateTime CreateDateTime { get; set; }
        public string name { get; set; } = string.Empty;
        public List<string> SignalName { get; set; } = new List<string>() { "name", "status" };
        public List<state> States { get; set; } = new List<state>();
    }
    public class state
    {
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
