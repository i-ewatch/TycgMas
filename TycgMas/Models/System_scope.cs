namespace TycgMas.Models
{
    public class System_scope
    {
        public DateTime CreateDateTime { get; set; }
        public List<SetData> SetDatas { get; set; } = new List<SetData>();
    }
    public class SetData
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 風扇電流
        /// </summary>
        public decimal Fan_Power_A { get; set; }
        /// <summary>
        /// 範圍溫度(流量計溫差)
        /// </summary>
        public decimal RangeTemp { get; set; }
        /// <summary>
        /// 出風溼球溫度
        /// </summary>
        public decimal Inlet_Air_WB { get; set; }
        /// <summary>
        /// 趨近溫度
        /// </summary>
        public decimal Approach { get; set; }
        /// <summary>
        /// 熱水
        /// </summary>
        public decimal Hot_Water { get; set; }
        /// <summary>
        /// 冷水
        /// </summary>
        public decimal Cool_Water { get; set; }
        /// <summary>
        /// 泵浦電流
        /// </summary>
        public decimal Pump_Power_A { get; set; }
        /// <summary>
        /// 瞬間流量(CMH)
        /// </summary>
        public decimal Water_Flow { get; set; }
        public List<CellData> CellDatas { get; set; } = new List<CellData>();
    }
    public class CellData
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Cell類型
        /// </summary>
        public int Cell_Type { get; set; } 
        /// <summary>
        /// 進風濕球溫度
        /// </summary>
        public decimal Inlet_Air_WB { get; set; }
    }
}
