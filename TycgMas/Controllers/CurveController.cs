using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurveController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;
        public CurveController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }
        /// <summary>
        /// 取得曲線圖項目
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/get-curve-default-item")]
        public IActionResult Default_Item()
        {
            return Ok(new string[]
            {
                "Tower Operating",
                "Environmental Status",
                "Power Status & Flow Status",
                "CT_x",
                "CT_x_Scatter"
            });
        }
        /// <summary>
        /// 取得散佈圖項目
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/get-curve-scatter-item")]
        public IActionResult Scatter_Item()
        {
            return Ok(new string[]
            {
                "Inlet WB Temp",
                "Outlet WB Temp",
                "Range",
                "Appr",
                "Cold Water Temp",
                "Hot Water Temp",
                "Flow Rate %",
                "Fan Power %",
                "Fan Fq.",
                "Pump Power",
                "Pump Fq."
            });
        }
        /// <summary>
        /// 曲線圖圖表
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("/api/post-curve-default-Line")]
        public IActionResult Default_Line(string Item, Filter filter)
        {
            if (filter.StartTime == "" || filter.EndTime == "")
                return BadRequest($"Start time and end time can't empty.");
            DateTime StartTime = Convert.ToDateTime(filter.StartTime);
            DateTime EndTime = Convert.ToDateTime(filter.EndTime);
            var SelectSet = dBEntities.SetDeviceSettings.Select(s => s).ToList();
            if (SelectSet == null) return BadRequest($"Can not find Set devices.");
            Chart chart = new Chart();
            switch (Item)
            {
                case "Tower Operating":
                    {
                        foreach (var Setitem in SelectSet)
                        {
                            var SetData = dBEntities.SetDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Uid == Setitem.Uid).ToList();
                            var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == Setitem.Uid).ToList();
                            chart.xAxis = SetData.Select(s => s.CreateDateTime.ToString("yyyy/MM/dd HH:mm:ss")).ToList();
                            chart.series.Add(
                                new
                                {
                                    name = $"{Setitem.TowerName}-WB",
                                    data = SetData.Select(s => s.InWetBulbTemp).ToList(),
                                    type = "line"
                                }
                            );
                            chart.series.Add(
                               new
                               {
                                   name = $"{Setitem.TowerName}-Range",
                                   data = SetData.Select(s => s.RangeTemp).ToList(),
                                   type = "line"
                               }
                           );
                            chart.series.Add(
                               new
                               {
                                   name = $"{Setitem.TowerName}-Appr",
                                   data = SetData.Select(s => s.Appr).ToList(),
                                   type = "line"
                               }
                           );
                            chart.series.Add(
                               new
                               {
                                   name = $"{Setitem.TowerName}-Cold water",
                                   data = FlowData.Select(s => s.OutputTemp).ToList(),
                                   type = "line"
                               }
                           );
                            chart.series.Add(
                               new
                               {
                                   name = $"{Setitem.TowerName}-Hot water",
                                   data = FlowData.Select(s => s.InputTemp).ToList(),
                                   type = "line"
                               }
                           );
                        }
                    }
                    break;
                case "Environmental Status":
                    {
                        foreach (var Setitem in SelectSet)
                        {
                            var SetData = dBEntities.SetDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Uid == Setitem.Uid).ToList();
                            chart.xAxis = SetData.Select(s => s.CreateDateTime.ToString("yyyy/MM/dd HH:mm:ss")).ToList();
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-DB",
                                data = SetData.Select(s => new { temp = (s.InputTemp + s.OutputTemp) / 2 }).Select(s => s.temp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-WB",
                                data = SetData.Select(s => new { temp = (s.InWetBulbTemp + s.OutWetBulbTemp) / 2 }).Select(s => s.temp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-RH",
                                data = SetData.Select(s => new { temp = (s.InRelativeHumidity + s.OutRelativeHumidity) / 2 }).Select(s => s.temp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-AH",
                                data = SetData.Select(s => new { temp = (s.InAbsoluteHumidity + s.OutAbsoluteHumidity) / 2 }).Select(s => s.temp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-DP",
                                data = SetData.Select(s => new { temp = (s.InDewPointTemp + s.OutDewPointTemp) / 2 }).Select(s => s.temp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-H",
                                data = SetData.Select(s => new { temp = (s.InEnthalpy + s.OutEnthalpy) / 2 }).Select(s => s.temp).ToList(),
                                type = "line"
                            });
                        }
                    }
                    break;
                case "Power Status & Flow Status":
                    {
                        foreach (var Setitem in SelectSet)
                        {
                            var SelectElectric = dBEntities.ElectricDeviceSettings.Where(w => w.Uid == Setitem.Uid).ToList();
                            var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == Setitem.Uid).ToList();
                            chart.xAxis = FlowData.Select(s => s.CreateDateTime.ToString("yyyy/MM/dd HH:mm:ss")).ToList();
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Flow(CMH)",
                                data = FlowData.Select(s => s.Flow).ToList(),
                                type = "line"
                            });
                            foreach (var Electricitem in SelectElectric)
                            {
                                if (Electricitem.ElectricType == 0)
                                {
                                    var Fan_ElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.ElectricType == 0 && w.Uid == Electricitem.Electricid).ToList();
                                    chart.series.Add(new
                                    {
                                        name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Fan Power(KW)",
                                        data = Fan_ElectricData.Select(s => s.Kw).ToList(),
                                        type = "line"
                                    });
                                }
                                else if (Electricitem.ElectricType == 1)
                                {
                                    var Pump_ElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.ElectricType == 1 && w.Uid == Electricitem.Electricid).ToList();
                                    if (Pump_ElectricData.Count > 0)
                                    {
                                        chart.series.Add(new
                                        {
                                            name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Pump Power(KW)",
                                            data = Pump_ElectricData.Select(s => s.Kw).ToList(),
                                            type = "line"
                                        });
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "CT_x":
                    {
                        foreach (var Setitem in SelectSet)
                        {
                            var SetData = dBEntities.SetDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Uid == Setitem.Uid).ToList();
                            var SelectElectric = dBEntities.ElectricDeviceSettings.Where(w => w.Uid == Setitem.Uid).ToList();
                            var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == Setitem.Uid).ToList();
                            chart.xAxis = FlowData.Select(s => s.CreateDateTime.ToString("yyyy/MM/dd HH:mm:ss")).ToList();
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Inlet WB Temp",
                                data = SetData.Select(s => s.InWetBulbTemp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Outlet WB Temp",
                                data = SetData.Select(s => s.OutWetBulbTemp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Range",
                                data = SetData.Select(s => s.RangeTemp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Appr",
                                data = SetData.Select(s => s.Appr).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Cold Water Temp",
                                data = FlowData.Select(s => s.OutputTemp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Hot Water Temp",
                                data = FlowData.Select(s => s.InputTemp).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Cooling Loading %",
                                data = FlowData.Select(s => s.HeatLoadRate).ToList(),
                                type = "line"
                            });
                            chart.series.Add(new
                            {
                                name = $"{Setitem.TowerName}-Flow Rate %",
                                data = FlowData.Select(s => s.FlowPercent).ToList(),
                                type = "line"
                            });
                            foreach (var Electricitem in SelectElectric)
                            {
                                if (Electricitem.ElectricType == 0)
                                {
                                    var Fan_ElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.ElectricType == 0 && w.Uid == Electricitem.Electricid).ToList();
                                    chart.series.Add(new
                                    {
                                        name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Fan Power %",
                                        data = Fan_ElectricData.Select(s => s.ElectricLoadRate).ToList(),
                                        type = "line"
                                    });
                                    if (!Electricitem.FqType)
                                    {
                                        chart.series.Add(new
                                        {
                                            name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Fan Fq.",
                                            data = Fan_ElectricData.Select(s => new { Fq = s.MinCurrent < s.Aavg ? "60" : "0" }).Select(s => s.Fq).ToList(),
                                            type = "line"
                                        });
                                    }
                                    else
                                    {
                                        chart.series.Add(new
                                        {
                                            name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Fan Fq.",
                                            data = Fan_ElectricData.Select(s => s.Hz).ToList(),
                                            type = "line"
                                        });
                                    }
                                }
                                else if (Electricitem.ElectricType == 1)
                                {
                                    var Pump_ElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.ElectricType == 1 && w.Uid == Electricitem.Electricid).ToList();
                                    if (Pump_ElectricData.Count > 0)
                                    {
                                        chart.series.Add(new
                                        {
                                            name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Pump Power",
                                            data = Pump_ElectricData.Select(s => s.Kw).ToList(),
                                            type = "line"
                                        });
                                        if (!Electricitem.FqType)
                                        {
                                            chart.series.Add(new
                                            {
                                                name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Pump Fq.",
                                                data = Pump_ElectricData.Select(s => new { Fq = s.MinCurrent < s.Aavg ? "60" : "0" }).Select(s => s.Fq).ToList(),
                                                type = "line"
                                            });
                                        }
                                        else
                                        {
                                            chart.series.Add(new
                                            {
                                                name = $"{Setitem.TowerName}-{Electricitem.ElectricName}-Pump Fq.",
                                                data = Pump_ElectricData.Select(s => s.Hz).ToList(),
                                                type = "line"
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            return Ok(chart);
        }
        /// <summary>
        /// 散佈圖圖表
        /// </summary>
        /// <param name="targetname"></param>
        /// <param name="Max"></param>
        /// <param name="Min"></param>
        /// <param name="xaxis"></param>
        /// <param name="yaxis"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("/api/post-curve-scatter-chart")]
        public IActionResult Scatter_chart(string targetname, decimal Max, decimal Min, string xaxis, string yaxis, Filter filter)
        {
            if (filter.StartTime == "" || filter.EndTime == "")
                return BadRequest($"Start time and end time can't empty.");
            if (targetname == "" || xaxis == "" || yaxis == "")
                return BadRequest($"Filter Item can't empty.");
            if (Max ==0)
                return BadRequest($"Max Value can't zero.");
            DateTime StartTime = Convert.ToDateTime(filter.StartTime);
            DateTime EndTime = Convert.ToDateTime(filter.EndTime);
            CT_Scatter cT_Scatters = new CT_Scatter();
            var SelectSet = dBEntities.SetDeviceSettings.Select(s => s).ToList();
            if (SelectSet == null) return BadRequest($"Can not find Set devices.");
            foreach (var Setitem in SelectSet)
            {
                var SetData = dBEntities.SetDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Uid == Setitem.Uid).ToList();
                var SelectElectric = dBEntities.ElectricDeviceSettings.Where(w => w.Uid == Setitem.Uid).ToList();
                var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == Setitem.Uid).ToList();
                cT_Scatters.CreateDataTime = SetData.Select(s => s.CreateDateTime).ToList();
                cT_Scatters.Inlet_WB_Temp = SetData.Select(s => new { data = (s.InWetBulbTemp == null ? 0 : (decimal)s.InWetBulbTemp) }).Select(s => s.data).ToList();
                cT_Scatters.Outlet_WB_Temp = SetData.Select(s => new { data = (s.OutWetBulbTemp == null ? 0 : (decimal)s.OutWetBulbTemp) }).Select(s => s.data).ToList();
                cT_Scatters.Range = SetData.Select(s => new { data = (s.RangeTemp == null ? 0 : (decimal)s.RangeTemp) }).Select(s => s.data).ToList();
                cT_Scatters.Apper = SetData.Select(s => new { data = (s.Appr == null ? 0 : (decimal)s.Appr) }).Select(s => s.data).ToList();
                cT_Scatters.Cold_Water_Temp = FlowData.Select(s => new { data = (s.OutputTemp == null ? 0 : (decimal)s.OutputTemp) }).Select(s => s.data).ToList();
                cT_Scatters.Hot_Water_Temp = FlowData.Select(s => new { data = (s.InputTemp == null ? 0 : (decimal)s.InputTemp) }).Select(s => s.data).ToList();
                cT_Scatters.Flow_Rate = FlowData.Select(s => new { data = (s.FlowPercent == null ? 0 : (decimal)s.FlowPercent) }).Select(s => s.data).ToList();
                foreach (var Electricitem in SelectElectric)
                {
                    if (Electricitem.ElectricType == 0)
                    {
                        var Fan_ElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.ElectricType == 0 && w.Uid == Electricitem.Electricid).ToList();
                        cT_Scatters.Flow_Power = Fan_ElectricData.Select(s => new { data = (s.ElectricLoadRate == null ? 0 : (decimal)s.ElectricLoadRate) }).Select(s => s.data).ToList();
                        if (!Electricitem.FqType)
                        {
                            cT_Scatters.Flow_Fq = Fan_ElectricData.Select(s => new { data = (s.ActionFlag == true ? (decimal)60 : 0) }).Select(s => s.data).ToList();
                        }
                        else
                        {
                            cT_Scatters.Flow_Fq = Fan_ElectricData.Select(s => new { data = (s.Hz == null ? 0 : (decimal)s.Hz) }).Select(s => s.data).ToList();
                        }
                    }
                    else if (Electricitem.ElectricType == 1)
                    {
                        var Pump_ElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.ElectricType == 1 && w.Uid == Electricitem.Electricid).ToList();
                        if (!Electricitem.FqType)
                        {
                            cT_Scatters.Flow_Fq = Pump_ElectricData.Select(s => new { data = (s.ActionFlag == true ? (decimal)60 : 0) }).Select(s => s.data).ToList();
                        }
                        else
                        {
                            cT_Scatters.Flow_Fq = Pump_ElectricData.Select(s => new { data = (s.Hz == null ? 0 : (decimal)s.Hz) }).Select(s => s.data).ToList();
                        }
                    }
                }
            }
            List<decimal[]> Scatter_Data = new List<decimal[]>();
            switch (targetname)
            {
                case "Inlet WB Temp":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Inlet_WB_Temp[i] <= Max && cT_Scatters.Inlet_WB_Temp[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Outlet WB Temp":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Outlet_WB_Temp[i] <= Max && cT_Scatters.Outlet_WB_Temp[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Range":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Range[i] <= Max && cT_Scatters.Range[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Appr":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Apper[i] <= Max && cT_Scatters.Apper[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Cold Water Temp":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Cold_Water_Temp[i] <= Max && cT_Scatters.Cold_Water_Temp[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Hot Water Temp":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Hot_Water_Temp[i] <= Max && cT_Scatters.Hot_Water_Temp[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Flow Rate %":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Flow_Rate[i] <= Max && cT_Scatters.Flow_Rate[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Fan Power %":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Flow_Power[i] <= Max && cT_Scatters.Flow_Power[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Fan Fq.":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Flow_Fq[i] <= Max && cT_Scatters.Flow_Fq[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Pump Power %":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Pump_Power[i] <= Max && cT_Scatters.Pump_Power[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
                case "Pump Fq.":
                    {
                        for (int i = 0; i < cT_Scatters.CreateDataTime.Count; i++)
                        {
                            if (cT_Scatters.Pump_Fq[i] <= Max && cT_Scatters.Pump_Fq[i] >= Min)
                            {
                                Scatter_Data.Add(scatter_Data(i, cT_Scatters, xaxis, yaxis));
                            }
                        }
                    }
                    break;
            }
            return Ok(new
            {
                title = new string[] { xaxis, yaxis },
                data = Scatter_Data.OrderBy(s => s[0]).ToList()
            });
        }
        /// <summary>
        /// 散佈圖資料整合
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="cT_Scatters"></param>
        /// <param name="xaxis"></param>
        /// <param name="yaxis"></param>
        /// <returns></returns>
        private decimal[] scatter_Data(int Index, CT_Scatter cT_Scatters, string xaxis, string yaxis)
        {
            decimal[] data = new decimal[2];
            switch (xaxis)
            {
                case "Inlet WB Temp":
                    {
                        data[0] = cT_Scatters.Inlet_WB_Temp[Index];
                    }
                    break;
                case "Outlet WB Temp":
                    {
                        data[0] = cT_Scatters.Outlet_WB_Temp[Index];
                    }
                    break;
                case "Range":
                    {
                        data[0] = cT_Scatters.Range[Index];
                    }
                    break;
                case "Appr":
                    {
                        data[0] = cT_Scatters.Apper[Index];
                    }
                    break;
                case "Cold Water Temp":
                    {
                        data[0] = cT_Scatters.Cold_Water_Temp[Index];
                    }
                    break;
                case "Hot Water Temp":
                    {
                        data[0] = cT_Scatters.Hot_Water_Temp[Index];
                    }
                    break;
                case "Flow Rate %":
                    {
                        data[0] = cT_Scatters.Flow_Rate[Index];
                    }
                    break;
                case "Fan Power %":
                    {
                        data[0] = cT_Scatters.Flow_Power[Index];
                    }
                    break;
                case "Fan Fq.":
                    {
                        data[0] = cT_Scatters.Flow_Fq[Index];
                    }
                    break;
                case "Pump Power %":
                    {
                        if (cT_Scatters.Pump_Power.Count > 0)
                        {
                            data[0] = cT_Scatters.Pump_Power[Index];
                        }
                        else
                        {
                            data[0] = 0;
                        }
                    }
                    break;
                case "Pump Fq.":
                    {
                        if (cT_Scatters.Pump_Power.Count > 0)
                        {
                            data[0] = cT_Scatters.Pump_Fq[Index];
                        }
                        else
                        {
                            data[0] = 0;
                        }
                    }
                    break;
            }
            switch (yaxis)
            {
                case "Inlet WB Temp":
                    {
                        data[1] = cT_Scatters.Inlet_WB_Temp[Index];
                    }
                    break;
                case "Outlet WB Temp":
                    {
                        data[1] = cT_Scatters.Outlet_WB_Temp[Index];
                    }
                    break;
                case "Range":
                    {
                        data[1] = cT_Scatters.Range[Index];
                    }
                    break;
                case "Appr":
                    {
                        data[1] = cT_Scatters.Apper[Index];
                    }
                    break;
                case "Cold Water Temp":
                    {
                        data[1] = cT_Scatters.Cold_Water_Temp[Index];
                    }
                    break;
                case "Hot Water Temp":
                    {
                        data[1] = cT_Scatters.Hot_Water_Temp[Index];
                    }
                    break;
                case "Flow Rate %":
                    {
                        data[1] = cT_Scatters.Flow_Rate[Index];
                    }
                    break;
                case "Fan Power %":
                    {
                        data[1] = cT_Scatters.Flow_Power[Index];
                    }
                    break;
                case "Fan Fq.":
                    {
                        data[1] = cT_Scatters.Flow_Fq[Index];
                    }
                    break;
                case "Pump Power %":
                    {
                        if (cT_Scatters.Pump_Power.Count > 0)
                        {
                            data[1] = cT_Scatters.Pump_Power[Index];
                        }
                        else
                        {
                            data[1] = 0;
                        }
                    }
                    break;
                case "Pump Fq.":
                    {
                        if (cT_Scatters.Pump_Power.Count > 0)
                        {
                            data[1] = cT_Scatters.Pump_Fq[Index];
                        }
                        else
                        {
                            data[1] = 0;
                        }
                    }
                    break;
            }
            return data;
        }
    }
}
