using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class CellDeviceLog
    {
        public DateTime CreateDateTime { get; set; }
        public Guid Setid { get; set; }
        public Guid Uid { get; set; }
        public decimal? InputTemp { get; set; }
        public decimal? OutputTemp { get; set; }
        public decimal? InWetBulbTemp { get; set; }
        public decimal? OutWetBulbTemp { get; set; }
        public decimal? InDewPointTemp { get; set; }
        public decimal? OutDewPointTemp { get; set; }
        public decimal? InAbsoluteHumidity { get; set; }
        public decimal? OutAbsoluteHumidity { get; set; }
        public decimal? InEnthalpy { get; set; }
        public decimal? OutEnthalpy { get; set; }
        public bool? ActionFlag { get; set; }
    }
}
