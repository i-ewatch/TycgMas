using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class CellDeviceSetting
    {
        public Guid Uid { get; set; }
        public Guid Cellid { get; set; }
        public int CellType { get; set; }
        public string? CellName { get; set; }
    }
}
