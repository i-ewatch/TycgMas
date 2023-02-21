namespace TycgMas.Models
{
    public class Title
    {
        public DateTime CreateDateTime { get; set; }
        public List<string> TitleName { get; set; } = new List<string>() { "cell_No", "status", "fan_Status", "fan_Power_A", "fan_Fq", "pump_Status", "pump_Power_A", "pump_Fq", "water_Flow", "hot_Water", "cool_Water", "range", "inlet_Air_WB", "approach","dV1","dV2","temp_Abnormal", "fan_Power_Abnormal", "pump_Power_Abnormal", "performance_Abnormal" };
        public List<TitleNum> TitleNums { get; set; } = new List<TitleNum>();
    }
    public class TitleNum
    {
        public List<TitleData> Titles { get; set; } = new List<TitleData>();
    }
    public class TitleData
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string Set_No { get; set; } = string.Empty;
        /// <summary>
        /// 名稱
        /// </summary>
        public string Cell_No { get; set; } = string.Empty;
        /// <summary>
        /// 設備類型
        /// <para> 0 = Set</para>
        /// <para> 1 = Cell</para>
        /// </summary>
        public int Device_Type { get; set; }
        /// <summary>
        /// 運轉狀態
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 風扇運轉狀態
        /// </summary>
        public string Fan_Status { get; set; } = string.Empty;
        /// <summary>
        /// 風扇電流
        /// </summary>
        public string Fan_Power_A { get; set; } = string.Empty;
        /// <summary>
        /// 風扇頻率
        /// </summary>
        public string Fan_Fq { get; set; } = string.Empty;
        /// <summary>
        /// 泵浦狀態
        /// </summary>
        public string Pump_Status { get; set; } = string.Empty;
        /// <summary>
        /// 泵浦電流
        /// </summary>
        public string Pump_Power_A { get; set; } = string.Empty;
        /// <summary>
        /// 泵浦頻率
        /// </summary>
        public string Pump_Fq { get; set; } = string.Empty;
        /// <summary>
        /// 瞬間流量(CMH)
        /// </summary>
        public string Water_Flow { get; set; } = string.Empty;
        /// <summary>
        /// 熱水
        /// </summary>
        public string Hot_Water { get; set; } = string.Empty;
        /// <summary>
        /// 冷水
        /// </summary>
        public string Cool_Water { get; set; } = string.Empty;
        /// <summary>
        /// 水溫差
        /// </summary>
        public string Range { get; set; } = string.Empty;
        /// <summary>
        /// 進風濕球溫度
        /// </summary>
        public string Inlet_Air_WB { get; set; } = string.Empty;
        /// <summary>
        /// 趨近溫度
        /// </summary>
        public string Approach { get; set; } = string.Empty;
        public string DV1 { get; set; } = string.Empty;
        public string DV2 { get; set; } = string.Empty;
        public string Temp_Abnormal { get; set; } = string.Empty;
        public string Fan_Power_Abnormal { get; set; } = string.Empty;
        public string Pump_Power_Abnormal { get; set; } = string.Empty;
        public string Performance_Abnormal { get; set; } = string.Empty;
    }
}
