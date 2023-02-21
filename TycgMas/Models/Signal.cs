namespace TycgMas.Models
{
    public class Signal
    {
        public DateTime CreateDateTime { get; set; }
        public string name { get; set; } = string.Empty;
        public List<string> SignalName { get; set; } = new List<string>() { "name", "flow", "flow_Unite", "sensor1", "sensor1_Unite", "sensor2", "sensor2_Unite", "connection" };
        public List<Equipment> Equipments { get; set; } = new List<Equipment>();
    }
    public class Equipment
    {
        public string name { get; set; } = string.Empty;
        public string Flow { get; set; } = string.Empty;
        public string Flow_Unite { get; set; } = string.Empty;
        public string Sensor1 { get; set; } = string.Empty;
        public string Sensor1_Unite { get; set; } = string.Empty;
        public string Sensor2 { get; set; } = string.Empty;
        public string Sensor2_Unite { get; set; } = string.Empty;
        /// <summary>
        /// 連線狀態
        /// </summary>
        public string Connection { get; set; } = string.Empty;
    }
}
