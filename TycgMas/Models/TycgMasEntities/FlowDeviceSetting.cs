using System;
using System.Collections.Generic;

namespace TycgMas.Models.TycgMasEntities
{
    public partial class FlowDeviceSetting
    {
        public Guid Uid { get; set; }
        public Guid Flowid { get; set; }
        public string? FlowName { get; set; }
    }
}
