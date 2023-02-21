namespace TycgMas.Models
{
    public class CT_Scatter
    {
        public List<DateTime> CreateDataTime { get; set; } = new List<DateTime>();
        public List<decimal> Inlet_WB_Temp { get; set; } = new List<decimal>();
        public List<decimal> Outlet_WB_Temp { get; set; } = new List<decimal>();
        public List<decimal> Range { get; set; } = new List<decimal>();
        public List<decimal> Apper { get; set; } = new List<decimal>();
        public List<decimal> Cold_Water_Temp { get; set; } = new List<decimal>();
        public List<decimal> Hot_Water_Temp { get; set; } = new List<decimal>();
        public List<decimal> Flow_Rate { get; set; } = new List<decimal>();
        public List<decimal> Flow_Power { get; set; } = new List<decimal>();
        public List<decimal> Flow_Fq { get; set; } = new List<decimal>();
        public List<decimal> Pump_Power { get; set; } = new List<decimal>();
        public List<decimal> Pump_Fq { get; set; } = new List<decimal>();
    }
}
