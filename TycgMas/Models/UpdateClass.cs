using System;
using System.Collections.Generic;

namespace TycgMas.Models
{
    public class UpdateClass
    {
        public SetDevice SetDevice { get; set; } = new SetDevice();
    }
    /// <summary>
    /// 冷卻水塔
    /// </summary>
    public class SetDevice
    {
        /// <summary>
        /// 上傳時間
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 冷卻水編碼
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 進風溫度
        /// </summary>
        public decimal InputTemp { get; set; }
        /// <summary>
        /// 出風溫度
        /// </summary>
        public decimal OutputTemp { get; set; }
        /// <summary>
        /// 溼球溫度
        /// </summary>
        public decimal InWetBulbTemp { get; set; }
        /// <summary>
        /// 露點溫度
        /// </summary>
        public decimal InDewPointTemp { get; set; }
        /// <summary>
        /// 相對溼度
        /// </summary>
        public decimal InRelativeHumidity { get; set; }
        /// <summary>
        /// 絕對溼度
        /// </summary>
        public decimal InAbsoluteHumidity { get; set; }
        /// <summary>
        /// 熱焓
        /// </summary>
        public decimal InEnthalpy { get; set; }
        /// <summary>
        /// 溼球溫度
        /// </summary>
        public decimal OutWetBulbTemp { get; set; }
        /// <summary>
        /// 露點溫度
        /// </summary>
        public decimal OutDewPointTemp { get; set; }
        /// <summary>
        /// 相對溼度
        /// </summary>
        public decimal OutRelativeHumidity { get; set; }
        /// <summary>
        /// 絕對溼度
        /// </summary>
        public decimal OutAbsoluteHumidity { get; set; }
        /// <summary>
        /// 熱焓
        /// </summary>
        public decimal OutEnthalpy { get; set; }
        /// <summary>
        /// 熱負載率
        /// </summary>
        public decimal HeatLoadRate { get; set; }
        /// <summary>
        /// 電負載率
        /// </summary>
        public decimal ElectricLoadRate { get; set; }
        /// <summary>
        /// 趨近溫度
        /// </summary>
        public decimal Appr { get; set; }
        /// <summary>
        /// 範圍溫度(流量計溫差)
        /// </summary>
        public decimal RangeTemp { get; set; }
        /// <summary>
        /// 焓差
        /// </summary>
        public decimal RangeEnthalpy { get; set; }
        /// <summary>
        /// 流量計設備
        /// </summary>
        public List<FlowDevice> FlowDvices { get; set; } = new List<FlowDevice>();
        public List<CellDevice> CellDevices { get; set; } = new List<CellDevice>();
    }
    /// <summary>
    /// 冰機
    /// </summary>
    public class CellDevice
    {
        /// <summary>
        /// 冰機編碼
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 進風溫度
        /// </summary>
        public decimal InputTemp { get; set; }
        /// <summary>
        /// 出風溫度
        /// </summary>
        public decimal OutputTemp { get; set; }
        /// <summary>
        /// 溼球溫度
        /// </summary>
        public decimal InWetBulbTemp { get; set; }
        /// <summary>
        /// 露點溫度
        /// </summary>
        public decimal InDewPointTemp { get; set; }
        /// <summary>
        /// 相對溼度
        /// </summary>
        public decimal InRelativeHumidity { get; set; }
        /// <summary>
        /// 絕對溼度
        /// </summary>
        public decimal InAbsoluteHumidity { get; set; }
        /// <summary>
        /// 熱焓
        /// </summary>
        public decimal InEnthalpy { get; set; }
        /// <summary>
        /// 溼球溫度
        /// </summary>
        public decimal OutWetBulbTemp { get; set; }
        /// <summary>
        /// 露點溫度
        /// </summary>
        public decimal OutDewPointTemp { get; set; }
        /// <summary>
        /// 相對溼度
        /// </summary>
        public decimal OutRelativeHumidity { get; set; }
        /// <summary>
        /// 絕對溼度
        /// </summary>
        public decimal OutAbsoluteHumidity { get; set; }
        /// <summary>
        /// 熱焓
        /// </summary>
        public decimal OutEnthalpy { get; set; }
        /// <summary>
        /// 啟動狀態
        /// </summary>
        public bool ActionFlag { get; set; }
        /// <summary>
        /// 溫溼度計設備
        /// </summary>
        public List<SenserDevice> SenserDevices { get; set; } = new List<SenserDevice>();
        /// <summary>
        /// 電表設備
        /// </summary>
        public List<ElectricDevice> ElectricDvices { get; set; } = new List<ElectricDevice>();
    }
    /// <summary>
    /// 監測設備
    /// </summary>
    public class Device
    {
        /// <summary>
        /// 設備編碼
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// 設備類型
        /// </summary>
        public int DeviceType { get; set; }
    }
    public class SenserDevice : Device
    {
        /// <summary>
        /// 乾球溫度
        /// </summary>
        public decimal Temp { get; set; }
        /// <summary>
        /// 相對溼度
        /// </summary>
        public decimal Humidity { get; set; }
        /// <summary>
        /// 溼球溫度
        /// </summary>
        public decimal WetBulbTemp { get; set; }
        /// <summary>
        /// 露點溫度
        /// </summary>
        public decimal DewPointTemp { get; set; }
        /// <summary>
        /// 絕對溼度
        /// </summary>
        public decimal AbsoluteHumidity { get; set; }
        /// <summary>
        /// 熱焓
        /// </summary>
        public decimal Enthalpy { get; set; }
        /// <summary>
        /// 最高溫度告警
        /// </summary>
        public decimal MaxTemp { get; set; }
        /// <summary>
        /// 最低溫度告警
        /// </summary>
        public decimal MinTemp { get; set; }

        /// <summary>
        /// 進/出風旗標
        /// </summary>
        public bool In_Out_Flag { get; set; }
        /// <summary>
        /// 錯誤碼類型
        /// </summary>
        public int ErrorType { get; set; }
        /// <summary>
        /// 一面編碼
        /// </summary>
        public int Noodle_Number { get; set; }
        /// <summary>
        /// 連線狀態
        /// </summary>
        public bool ConnectionFlag { get; set; }
    }
    public class FlowDevice : Device
    {
        /// <summary>
        /// 瞬間流量(CMH)
        /// </summary>
        public decimal Flow { get; set; }
        /// <summary>
        /// 瞬間流量(LPM)
        /// </summary>
        public decimal LFlow { get; set; }
        /// <summary>
        /// 累積流量
        /// </summary>
        public decimal FlowTotal { get; set; }
        /// <summary>
        /// 入水溫度
        /// </summary>
        public decimal InputTemp { get; set; }
        /// <summary>
        /// 出水溫度
        /// </summary>
        public decimal OutputTemp { get; set; }
        /// <summary>
        /// 流量百分比%
        /// </summary>
        public decimal FlowPercent { get; set; }
        /// <summary>
        /// 水溫差
        /// </summary>
        public decimal Rang { get; set; }
        /// <summary>
        /// 熱負載率
        /// </summary>
        public decimal HeatLoadRate { get; set; }
        /// <summary>
        /// 熱負載定值
        /// </summary>
        public decimal HeatLoad { get; set; }
        /// <summary>
        /// 最高入水溫度警報(流量計)
        /// </summary>
        public decimal MaxInputTemp { get; set; }
        /// <summary>
        /// 最低入水溫度警報(流量計)
        /// </summary>
        public decimal MinInputTemp { get; set; }
        /// <summary>
        /// 最高出水溫度警報(流量計)
        /// </summary>
        public decimal MaxOutputTemp { get; set; }
        /// <summary>
        /// 最低出水溫度警報(流量計)
        /// </summary>
        public decimal MinOutputTemp { get; set; }
        /// <summary>
        /// 入水錯誤碼類型
        /// </summary>
        public int InErrorType { get; set; }
        /// <summary>
        /// 出水錯誤碼類型
        /// </summary>
        public int OutErrorType { get; set; }
        /// <summary>
        /// 連線狀態
        /// </summary>
        public bool ConnectionFlag { get; set; }
    }
    public class ElectricDevice : Device
    {
        /// <summary>
        /// 變頻旗標
        /// <para>0 = 定頻</para>
        /// <para>1 = 變頻</para>
        /// </summary>
        public bool Fq_Type { get; set; }
        /// <summary>
        /// 設備安裝的位置
        /// <para>0 = 風扇</para>
        /// <para>1 = 水泵</para>
        /// </summary>
        public int Electric_Type { get; set; }
        /// <summary>
        /// R相電壓
        /// </summary>
        public decimal RV { get; set; }
        /// <summary>
        /// S相電壓
        /// </summary>
        public decimal SV { get; set; }
        /// <summary>
        /// T相電壓
        /// </summary>
        public decimal TV { get; set; }
        /// <summary>
        /// R線電壓
        /// </summary>
        public decimal RSV { get; set; }
        /// <summary>
        /// S線電壓
        /// </summary>
        public decimal STV { get; set; }
        /// <summary>
        /// T線電壓
        /// </summary>
        public decimal TRV { get; set; }
        /// <summary>
        /// R相電流
        /// </summary>
        public decimal RA { get; set; }
        /// <summary>
        /// S相電流
        /// </summary>
        public decimal SA { get; set; }
        /// <summary>
        /// T相電流
        /// </summary>
        public decimal TA { get; set; }
        /// <summary>
        /// 平均電流
        /// </summary>
        public decimal AAvg { get; set; }
        /// <summary>
        /// 瞬間功率
        /// </summary>
        public decimal KW { get; set; }
        /// <summary>
        /// 功率因數
        /// </summary>
        public decimal PF { get; set; }
        /// <summary>
        /// 累積功率
        /// </summary>
        public decimal KWH { get; set; }
        /// <summary>
        /// 頻率
        /// </summary>
        public decimal HZ { get; set; }
        /// <summary>
        /// 電負載率
        /// </summary>
        public decimal ElectricLoadRate { get; set; }
        /// <summary>
        /// 額定功率(電表)
        /// </summary>
        public decimal RatedPower { get; set; }
        /// <summary>
        /// 最小判斷啟動電流
        /// </summary>
        public decimal MinCurrent { get; set; }
        /// <summary>
        /// 啟動狀態
        /// </summary>
        public bool ActionFlag { get; set; }
        /// <summary>
        /// 錯誤碼類型
        /// </summary>
        public int ErrorType { get; set; }
        /// <summary>
        /// 連線狀態
        /// </summary>
        public bool ConnectionFlag { get; set; }
    }
}
