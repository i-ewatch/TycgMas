using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class FlowDeviceLog
    {
        public DateTime CreateDateTime { get; set; }
        public Guid Setid { get; set; }
        public Guid Uid { get; set; }
        public decimal? Flow { get; set; }
        public decimal? FlowTotal { get; set; }
        public decimal? InputTemp { get; set; }
        public decimal? OutputTemp { get; set; }
        public decimal? Rang { get; set; }
        public decimal? HeatLoadRate { get; set; }
        public decimal? HeatLoad { get; set; }
        public decimal? MaxInputTemp { get; set; }
        public decimal? MinInputTemp { get; set; }
        public decimal? MaxOutputTemp { get; set; }
        public decimal? MinOutputTemp { get; set; }
        public int? InErrorType { get; set; }
        public int? OutErrorType { get; set; }
        public bool? ConnectionFlag { get; set; }
        public decimal? Lflow { get; set; }
        public decimal? FlowPercent { get; set; }
    }
}
