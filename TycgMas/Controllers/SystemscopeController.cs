using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemscopeController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;
        public SystemscopeController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }
        [HttpGet("/api/get-system-scope")]
        public IActionResult SystemScope()
        {
            System_scope system_Scope = new System_scope();
            List<SetData> SetDatas = new List<SetData>();
            var SelectSet = dBEntities.SetDeviceSettings.Select(s => s).ToList();
            if (SelectSet == null) return BadRequest($"Can not find Set devices.");
            foreach (var Setitem in SelectSet)
            {
                var SetData = dBEntities.SetDeviceLogs.Where(w => w.Uid == Setitem.Uid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.Setid == Setitem.Uid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                var FanElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.Setid == Setitem.Uid && w.ElectricType == 0).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                system_Scope.CreateDateTime = SetData.CreateDateTime;
                SetData Set = new SetData();
                Set.Name = Setitem.TowerName;
                Set.Fan_Power_A = (decimal)FanElectricData.Aavg;
                Set.RangeTemp = (decimal)SetData.RangeTemp;
                Set.Inlet_Air_WB = (decimal)SetData.InWetBulbTemp;
                Set.Approach = (decimal)SetData.Appr;
                Set.Hot_Water = (decimal)FlowData.OutputTemp;
                Set.Cool_Water = (decimal)FlowData.InputTemp;
                Set.Pump_Power_A = 0;
                Set.Water_Flow = (decimal)FlowData.Flow;
                var SelectCell = dBEntities.CellDeviceSettings.Where(w => w.Uid == Setitem.Uid).ToList();
                foreach (var Cellitem in SelectCell)
                {
                    var CellData = dBEntities.CellDeviceLogs.Where(w => w.Uid == Cellitem.Cellid & w.Setid == Cellitem.Uid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                    CellData Cell = new CellData();
                    Cell.Name = Cellitem.CellName;
                    Cell.Cell_Type = (int)Cellitem.CellType;
                    Cell.Inlet_Air_WB = (decimal)CellData.InWetBulbTemp;
                    Set.CellDatas.Add(Cell);
                }
                SetDatas.Add(Set);
            }
            system_Scope.SetDatas = SetDatas;
            return Ok(system_Scope);
        }
    }
}
