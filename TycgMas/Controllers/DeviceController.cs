using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;

        public DeviceController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }
        /// <summary>
        /// 設備資訊設定
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPost("post-setting")]
        public IActionResult CreateDataSetting(UpdateClass update)
        {
            int FlowCount = 0;
            int CellCount = 0;
            int SensorCount = 0;
            int ElectricCount = 0;
            try
            {
                var setDeviceExsit = dBEntities.SetDeviceSettings.Any(a => a.Uid == update.SetDevice.Guid);
                if (setDeviceExsit)
                    return BadRequest($"Create not success SetDevice Id [{update.SetDevice.Guid}] is exist.");
                SetDeviceSetting setDeviceSetting = new()
                {
                    Uid = update.SetDevice.Guid,
                    TowerName = ""
                };
                dBEntities.SetDeviceSettings.Add(setDeviceSetting);
                foreach (var flowDev in update.SetDevice.FlowDvices)
                {
                    var flowDeviceExsit = dBEntities.FlowDeviceSettings.Any(a => a.Flowid == flowDev.Guid && a.Uid == update.SetDevice.Guid);
                    if (!flowDeviceExsit)
                    {
                        FlowDeviceSetting flowDeviceSetting = new()
                        {
                            Uid = update.SetDevice.Guid,
                            Flowid = flowDev.Guid,
                            FlowName = ""
                        };
                        dBEntities.FlowDeviceSettings.Add(flowDeviceSetting);
                        FlowCount++;
                    }
                }
                foreach (var cellDev in update.SetDevice.CellDevices)
                {
                    var cellDeviceExsit = dBEntities.CellDeviceSettings.Any(a => a.Cellid == cellDev.Guid && a.Uid == update.SetDevice.Guid);
                    if (!cellDeviceExsit)
                    {
                        CellDeviceSetting cellDeviceSetting = new()
                        {
                            Uid = update.SetDevice.Guid,
                            Cellid = cellDev.Guid,
                            CellName = ""
                        };
                        dBEntities.CellDeviceSettings.Add(cellDeviceSetting);
                        CellCount++;
                        foreach (var sensorDev in cellDev.SenserDevices)
                        {
                            var sensorDeviceExsit = dBEntities.SensorDeviceSettings.Any(a => a.Sensorid == sensorDev.Guid && a.Uid == update.SetDevice.Guid);
                            if (!sensorDeviceExsit)
                            {
                                SensorDeviceSetting sensorDeviceSetting = new()
                                {
                                    Uid = update.SetDevice.Guid,
                                    Cellid = cellDev.Guid,
                                    Sensorid = sensorDev.Guid,
                                    SensorName = "",
                                    NoodleNumber = sensorDev.Noodle_Number
                                };
                                dBEntities.SensorDeviceSettings.Add(sensorDeviceSetting);
                                SensorCount++;
                            }
                        }
                        foreach (var electricDev in cellDev.ElectricDvices)
                        {
                            var electricDeviceExsit = dBEntities.ElectricDeviceSettings.Any(a => a.Electricid == electricDev.Guid && a.Uid == update.SetDevice.Guid);
                            if (!electricDeviceExsit)
                            {
                                ElectricDeviceSetting electricDeviceSetting = new()
                                {
                                    Uid = update.SetDevice.Guid,
                                    Cellid = cellDev.Guid,
                                    Electricid = electricDev.Guid,
                                    ElectricName = ""
                                };
                                dBEntities.ElectricDeviceSettings.Add(electricDeviceSetting);
                                ElectricCount++;
                            }
                        }
                    }
                }
                dBEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Initial setting fault.");
            }
            return Ok($"Create success amount for this time, \n" +
                      $"SetDevice ID:[{update.SetDevice.Guid}] \n" +
                      $"FlowDevice:{FlowCount} \n" +
                      $"CellDevice:{CellCount} \n" +
                      $"SensorDevice:{SensorCount} \n" +
                      $"ElectricDevice:{ElectricCount}");
        }

        [HttpGet("get-setdevice-setting")]
        public IActionResult GetSetDeviceSetting()
        {
            return Ok();
        }
    }
}
