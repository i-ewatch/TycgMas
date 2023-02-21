using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class SensorDeviceSetting
    {
        public Guid Uid { get; set; }
        public Guid Cellid { get; set; }
        public Guid Sensorid { get; set; }
        public string? SensorName { get; set; }
        public int? NoodleNumber { get; set; }
    }
}
