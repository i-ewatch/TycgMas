namespace TycgMas.Models
{
    public class Chart
    {
        public List<string> xAxis { get; set; } = new List<string>();
        public List<object> series { get; set; } = new List<object>();
    }
}
