using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataCatcherController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;
        public DataCatcherController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }
        /// <summary>
        /// 接收資料
        /// </summary>
        /// <param name="update">上傳數值</param>
        /// <returns></returns>
        [HttpPost("/api/post-realtime-data")]
        public async Task<IActionResult> CatchData(UpdateClass update)
        {
            DateTime logTime = Convert.ToDateTime($"{DateTime.Now:yyyy/MM/dd HH:mm:00}");
            var exsit_Main = dBEntities.SetDeviceSettings.Any(a => a.Uid == update.SetDevice.Guid);
            if (!exsit_Main)
                return BadRequest($"Not found access, pleass contact service engineer.");
            try
            {
                #region 新增 SetDeviceLog
                var setDevLogExsit = dBEntities.SetDeviceLogs.Any(a => a.Uid == update.SetDevice.Guid && a.CreateDateTime == logTime);
                if (!setDevLogExsit)
                {
                    #region 新增SetDevice
                    SetDeviceLog setDeviceLog = new()
                    {
                        CreateDateTime = logTime,
                        Uid = update.SetDevice.Guid,
                        InputTemp = update.SetDevice.InputTemp,
                        OutputTemp = update.SetDevice.OutputTemp,
                        InWetBulbTemp = update.SetDevice.InWetBulbTemp,
                        OutWetBulbTemp = update.SetDevice.OutWetBulbTemp,
                        InAbsoluteHumidity = update.SetDevice.InAbsoluteHumidity,
                        OutAbsoluteHumidity = update.SetDevice.OutAbsoluteHumidity,
                        InDewPointTemp = update.SetDevice.InDewPointTemp,
                        OutDewPointTemp = update.SetDevice.OutDewPointTemp,
                        InEnthalpy = update.SetDevice.InEnthalpy,
                        OutEnthalpy = update.SetDevice.OutEnthalpy,
                        InRelativeHumidity = update.SetDevice.InRelativeHumidity,
                        OutRelativeHumidity = update.SetDevice.OutRelativeHumidity,
                        ElectricLoadRate = update.SetDevice.ElectricLoadRate,
                        HeatLoadRate = update.SetDevice.HeatLoadRate,
                        Appr = update.SetDevice.Appr,
                        RangeTemp = update.SetDevice.RangeTemp,
                        RangeEnthalpy = update.SetDevice.RangeEnthalpy
                    };
                    dBEntities.SetDeviceLogs.Add(setDeviceLog);
                    #endregion
                    #region 新增 CellDeviceLog
                    foreach (var cellDevice in update.SetDevice.CellDevices)
                    {
                        var cellDeviceExist = dBEntities.CellDeviceSettings.Any(a => a.Cellid == cellDevice.Guid);
                        if (cellDeviceExist)
                        {
                            var cellDevLogExsit = dBEntities.CellDeviceLogs.Any(a => a.Uid == cellDevice.Guid && a.CreateDateTime == logTime);
                            if (!cellDevLogExsit)
                            {
                                CellDeviceLog cellDeviceLog = new()
                                {
                                    CreateDateTime = logTime,
                                    Setid = update.SetDevice.Guid,
                                    Uid = cellDevice.Guid,
                                    InputTemp = cellDevice.InputTemp,
                                    OutputTemp = cellDevice.OutputTemp,
                                    InWetBulbTemp = cellDevice.InWetBulbTemp,
                                    OutWetBulbTemp = cellDevice.OutWetBulbTemp,
                                    InDewPointTemp = cellDevice.InDewPointTemp,
                                    OutDewPointTemp = cellDevice.OutDewPointTemp,
                                    InAbsoluteHumidity = cellDevice.InAbsoluteHumidity,
                                    OutAbsoluteHumidity = cellDevice.OutAbsoluteHumidity,
                                    InEnthalpy = cellDevice.InEnthalpy,
                                    OutEnthalpy = cellDevice.OutEnthalpy,
                                    ActionFlag = cellDevice.ActionFlag
                                };
                                dBEntities.CellDeviceLogs.Add(cellDeviceLog);
                            }
                            #region 新增 SensorDeviceLog
                            foreach (var sensorDevice in cellDevice.SenserDevices)
                            {
                                var sensorDeviceExist = dBEntities.SensorDeviceSettings.Any(a => a.Sensorid == sensorDevice.Guid);
                                if (sensorDeviceExist)
                                {
                                    var sensorDevLogExist = dBEntities.SensorDeviceLogs.Any(a => a.Uid == sensorDevice.Guid && a.CreateDateTime == logTime);
                                    if (!sensorDevLogExist)
                                    {
                                        SensorDeviceLog sensorDeviceLog = new()
                                        {
                                            CreateDateTime = logTime,
                                            Setid = update.SetDevice.Guid,
                                            Uid = sensorDevice.Guid,
                                            DeviceType = sensorDevice.DeviceType,
                                            Temp = sensorDevice.Temp,
                                            Humidity = sensorDevice.Humidity,
                                            WetBulbTemp = sensorDevice.WetBulbTemp,
                                            DewPointTemp = sensorDevice.DewPointTemp,
                                            AbsoluteHumidity = sensorDevice.AbsoluteHumidity,
                                            Enthalpy = sensorDevice.Enthalpy,
                                            MaxTemp = sensorDevice.MaxTemp,
                                            MinTemp = sensorDevice.MinTemp,
                                            InOutFlag = sensorDevice.In_Out_Flag,
                                            ErrorType = sensorDevice.ErrorType,
                                            NoodleNumber = sensorDevice.Noodle_Number,
                                            ConnectionFlag = sensorDevice.ConnectionFlag
                                        };
                                        dBEntities.SensorDeviceLogs.Add(sensorDeviceLog);
                                    }
                                }
                            }
                            #endregion
                            #region 新增 ElectricDeviceLog
                            foreach (var electricDevice in cellDevice.ElectricDvices)
                            {
                                var electricDeviceExist = dBEntities.ElectricDeviceSettings.Any(a => a.Electricid == electricDevice.Guid);
                                if (electricDeviceExist)
                                {
                                    var electricDevLogExist = dBEntities.ElectricDeviceLogs.Any(a => a.Uid == electricDevice.Guid && a.CreateDateTime == logTime);
                                    if (!electricDevLogExist)
                                    {
                                        ElectricDeviceLog electricDeviceLog = new()
                                        {
                                            CreateDateTime = logTime,
                                            Setid = update.SetDevice.Guid,
                                            ElectricType = electricDevice.Electric_Type,
                                            FqType = electricDevice.Fq_Type,
                                            Uid = electricDevice.Guid,
                                            Rv = electricDevice.RV,
                                            Sv = electricDevice.SV,
                                            Tv = electricDevice.TV,
                                            Rsv = electricDevice.RSV,
                                            Stv = electricDevice.STV,
                                            Trv = electricDevice.TRV,
                                            Ra = electricDevice.RA,
                                            Sa = electricDevice.SA,
                                            Ta = electricDevice.TA,
                                            Aavg = electricDevice.AAvg,
                                            Kw = electricDevice.KW,
                                            Pf = electricDevice.PF,
                                            Kwh = electricDevice.KWH,
                                            Hz = electricDevice.HZ,
                                            ElectricLoadRate = electricDevice.ElectricLoadRate,
                                            RatedPower = electricDevice.RatedPower,
                                            MinCurrent = electricDevice.MinCurrent,
                                            ActionFlag = electricDevice.ActionFlag,
                                            ErrorType = electricDevice.ErrorType,
                                            ConnectionFlag = electricDevice.ConnectionFlag
                                        };
                                        dBEntities.ElectricDeviceLogs.Add(electricDeviceLog);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                    #region 新增 FlowDeviceLog
                    foreach (var flowDevice in update.SetDevice.FlowDvices)
                    {
                        var flowDeviceExist = dBEntities.FlowDeviceSettings.Any(a => a.Flowid == flowDevice.Guid);
                        if (flowDeviceExist)
                        {
                            var flowDevLogExsit = dBEntities.FlowDeviceLogs.Any(a => a.Uid == flowDevice.Guid && a.CreateDateTime == logTime);
                            if (!flowDevLogExsit)
                            {
                                FlowDeviceLog flowDeviceLog = new()
                                {
                                    CreateDateTime = logTime,
                                    Setid = update.SetDevice.Guid,
                                    Uid = flowDevice.Guid,
                                    Flow = flowDevice.Flow,
                                    FlowTotal = flowDevice.FlowTotal,
                                    InputTemp = flowDevice.InputTemp,
                                    OutputTemp = flowDevice.OutputTemp,
                                    Rang = flowDevice.Rang,
                                    HeatLoadRate = flowDevice.HeatLoadRate,
                                    HeatLoad = flowDevice.HeatLoad,
                                    MaxInputTemp = flowDevice.MaxInputTemp,
                                    MinInputTemp = flowDevice.MinInputTemp,
                                    MaxOutputTemp = flowDevice.MaxOutputTemp,
                                    MinOutputTemp = flowDevice.MinOutputTemp,
                                    InErrorType = flowDevice.InErrorType,
                                    OutErrorType = flowDevice.OutErrorType,
                                    ConnectionFlag = flowDevice.ConnectionFlag,
                                    FlowPercent = flowDevice.FlowPercent,
                                    Lflow = flowDevice.LFlow
                                };
                                dBEntities.FlowDeviceLogs.Add(flowDeviceLog);
                            }
                        }
                    }
                    #endregion
                    dBEntities.SaveChanges();           // 儲存資料
                }
                #endregion

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "catch data wrang.");
            }
            return Ok(update);
        }
    }
}
