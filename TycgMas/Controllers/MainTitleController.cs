using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainTitleController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;

        public MainTitleController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }

        [HttpGet("/api/get-main-title")]
        public IActionResult TitleData()
        {
            Title Title = new Title();
            List<TitleNum> TitleNums = new List<TitleNum>();
            var SelectSet = dBEntities.SetDeviceSettings.Select(s => s).ToList();
            if (SelectSet == null) return BadRequest($"Can not find Set devices.");
            foreach (var Setitem in SelectSet)
            {
                TitleNum TitleNum = new TitleNum();
                var SetData = dBEntities.SetDeviceLogs.Where(w => w.Uid == Setitem.Uid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.Setid == Setitem.Uid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                Title.CreateDateTime = SetData.CreateDateTime;
                TitleData setTitle = new TitleData();
                setTitle.Set_No = Setitem.TowerName;
                setTitle.Cell_No = Setitem.TowerName;
                setTitle.Device_Type = 0;
                setTitle.Pump_Status = (decimal)FlowData.Flow > 0 ? "ON" : "OFF";
                setTitle.Pump_Power_A = "0";
                setTitle.Pump_Fq = "0";
                setTitle.Water_Flow = FlowData.Flow.ToString();
                setTitle.Hot_Water = FlowData.OutputTemp.ToString();
                setTitle.Cool_Water = FlowData.InputTemp.ToString();
                setTitle.Range = SetData.RangeTemp.ToString();
                setTitle.Inlet_Air_WB = SetData.InWetBulbTemp.ToString();
                setTitle.Approach = SetData.Appr.ToString();
                setTitle.DV1 = SetData.ElectricLoadRate.ToString();
                TitleNum.Titles.Add(setTitle);
                var SelectCell = dBEntities.CellDeviceSettings.Where(w => w.Uid == Setitem.Uid).ToList();
                foreach (var Cellitem in SelectCell)
                {
                    var CellData = dBEntities.CellDeviceLogs.Where(w => w.Uid == Cellitem.Cellid & w.Setid == Cellitem.Uid).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                    var SelectFan = dBEntities.ElectricDeviceSettings.Where(w => w.Cellid == Cellitem.Cellid && w.Uid == Cellitem.Uid).ToList();
                    foreach (var Fanitem in SelectFan)
                    {
                        var FanElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.Setid == Cellitem.Uid && w.Uid == Fanitem.Electricid && w.ElectricType == 0).OrderByDescending(o => o.CreateDateTime).Take(10).FirstOrDefault();
                        TitleData cellTitle = new TitleData();
                        cellTitle.Set_No = Setitem.TowerName;
                        cellTitle.Cell_No = Cellitem.CellName;
                        cellTitle.Device_Type = 1;
                        cellTitle.Status = (bool)CellData.ActionFlag ? "ON" : "OFF";
                        cellTitle.Fan_Status = (bool)CellData.ActionFlag ? "ON" : "OFF";
                        cellTitle.Fan_Power_A = FanElectricData.Aavg.ToString();
                        if ((bool)FanElectricData.FqType)
                        {
                            cellTitle.Fan_Fq = FanElectricData.Hz.ToString();
                            cellTitle.DV1 = FanElectricData.ElectricLoadRate.ToString();
                        }
                        else
                        {
                            if ((bool)FanElectricData.ActionFlag)
                            {
                                cellTitle.Fan_Fq = "60";
                            }
                            else
                            {
                                cellTitle.Fan_Fq = "0";
                            }
                            cellTitle.DV1 = FanElectricData.ElectricLoadRate.ToString();
                        }
                        cellTitle.Inlet_Air_WB = CellData.InWetBulbTemp.ToString();
                        TitleNum.Titles.Add(cellTitle);
                    }
                }
                TitleNums.Add(TitleNum);
            }
            Title.TitleNums = TitleNums;
            return Ok(Title);
        }
    }
}
