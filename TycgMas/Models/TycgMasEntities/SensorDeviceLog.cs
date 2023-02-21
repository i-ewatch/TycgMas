using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class SensorDeviceLog
    {
        public DateTime CreateDateTime { get; set; }
        public Guid Setid { get; set; }
        public Guid Uid { get; set; }
        public int? DeviceType { get; set; }
        public decimal? Temp { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? WetBulbTemp { get; set; }
        public decimal? DewPointTemp { get; set; }
        public decimal? AbsoluteHumidity { get; set; }
        public decimal? Enthalpy { get; set; }
        public decimal? MaxTemp { get; set; }
        public decimal? MinTemp { get; set; }
        public bool? InOutFlag { get; set; }
        public int? ErrorType { get; set; }
        public int? NoodleNumber { get; set; }
        public bool? ConnectionFlag { get; set; }
    }
}
