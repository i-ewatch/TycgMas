using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class ElectricDeviceSetting
    {
        public Guid Uid { get; set; }
        public Guid Cellid { get; set; }
        public Guid Electricid { get; set; }
        public bool FqType { get; set; }
        public int ElectricType { get; set; }
        public string? ElectricName { get; set; }
    }
}
