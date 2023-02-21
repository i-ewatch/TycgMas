using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;

        public SignalController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }
        [HttpGet("/api/get-signal")]
        public IActionResult SignalData()
        {
            Signal signal = new Signal();
            var SelectSet = dBEntities.SetDeviceSettings.Select(s => s).ToList();
            if (SelectSet == null) return BadRequest($"Can not find Set devices.");
            foreach (var Setitem in SelectSet)
            {
                signal.name = Setitem.TowerName;
                var SelectFlow = dBEntities.FlowDeviceSettings.Where(w => w.Uid == Setitem.Uid).ToList();
                var SelectSensor = dBEntities.SensorDeviceSettings.Where(w => w.Uid == Setitem.Uid).ToList();
                foreach (var Flowitem in SelectFlow)
                {
                    var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.Setid == Setitem.Uid && w.Uid == Flowitem.Flowid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                    string Connection = FlowData.ConnectionFlag == true? "Connection" : "Disconnection";
                    signal.CreateDateTime = FlowData.CreateDateTime;
                    signal.Equipments.Add(new Equipment
                    {
                        name = $"{Flowitem.FlowName}",
                        Flow = $"{(decimal)FlowData.Flow}",
                        Flow_Unite = "CMH",
                        Sensor1 = $"{(decimal)FlowData.InputTemp}",
                        Sensor1_Unite = "°C",
                        Sensor2 = $"{(decimal)FlowData.OutputTemp}",
                        Sensor2_Unite = "°C",
                        Connection = $"{Connection}"
                    });
                }
                foreach (var Sensoritem in SelectSensor)
                {
                    var SensorData = dBEntities.SensorDeviceLogs.Where(w => w.Setid == Setitem.Uid && w.Uid == Sensoritem.Sensorid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                    string Connection = SensorData.ConnectionFlag == true ? "Connection" : "Disconnection";
                    signal.Equipments.Add(new Equipment
                    {
                        name = $"{Sensoritem.SensorName}",
                        Flow = $"",
                        Flow_Unite = "",
                        Sensor1 = $"{(decimal)SensorData.Temp}",
                        Sensor1_Unite = "°C",
                        Sensor2 = $"{(decimal)SensorData.Humidity}",
                        Sensor2_Unite = "%",
                        Connection = $"{Connection}"
                    });
                }
            }
            return Ok(signal);
        }
    }
}
