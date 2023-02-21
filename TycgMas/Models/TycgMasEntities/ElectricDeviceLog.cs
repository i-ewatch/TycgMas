using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class ElectricDeviceLog
    {
        public DateTime CreateDateTime { get; set; }
        public Guid Setid { get; set; }
        public Guid Uid { get; set; }
        public decimal? Rv { get; set; }
        public decimal? Sv { get; set; }
        public decimal? Tv { get; set; }
        public decimal? Rsv { get; set; }
        public decimal? Stv { get; set; }
        public decimal? Trv { get; set; }
        public decimal? Ra { get; set; }
        public decimal? Sa { get; set; }
        public decimal? Ta { get; set; }
        public decimal? Aavg { get; set; }
        public decimal? Kw { get; set; }
        public decimal? Pf { get; set; }
        public decimal? Kwh { get; set; }
        public decimal? Hz { get; set; }
        public decimal? ElectricLoadRate { get; set; }
        public decimal? RatedPower { get; set; }
        public decimal? MinCurrent { get; set; }
        public bool? ActionFlag { get; set; }
        public int? ErrorType { get; set; }
        public bool FqType { get; set; }
        public int ElectricType { get; set; }
        public bool? ConnectionFlag { get; set; }
    }
}
