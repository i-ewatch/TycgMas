using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;

        public ExportController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }
        [HttpGet]
        [Route("/api/get-set-Item")]
        public IActionResult Set_Item()
        {
            var SelectSet = dBEntities.SetDeviceSettings.Select(s => s).ToList();
            if (SelectSet.Count == 0) return BadRequest($"Can not find Set devices.");
            List<string> setname = SelectSet.Select(s => s.TowerName.ToString()).ToList();
            return Ok(setname);
        }
        [HttpGet]
        [Route("/api/get-download-Item")]
        public IActionResult Download_Item()
        {
            return Ok(new string[]
            {
                "Set",
                "Cell",
                "Flow",
                "Electric",
                "Sensor"
            });
        }
        [HttpPost]
        [Route("api/post-download-report")]
        public IActionResult Download_Report(string setItem, string downloadItem, Filter filter)
        {
            if (filter.StartTime == "" || filter.EndTime == "")
                return BadRequest($"Start time and end time can't empty.");
            DateTime StartTime = Convert.ToDateTime(filter.StartTime + " 00:00:00");
            DateTime EndTime = Convert.ToDateTime(filter.EndTime + " 23:59:59");
            var SelectSet = dBEntities.SetDeviceSettings.SingleOrDefault(s => s.TowerName == setItem);
            if (SelectSet == null) return BadRequest($"Can not find Set devices.");
            var SelectCell = dBEntities.CellDeviceSettings.Where(w => w.Uid == SelectSet.Uid).ToList();
            var SelectFlow = dBEntities.FlowDeviceSettings.Where(w => w.Uid == SelectSet.Uid).ToList();
            var SelectElectric = dBEntities.ElectricDeviceSettings.Where(w => w.Uid == SelectSet.Uid).ToList();
            var SelectSensor = dBEntities.SensorDeviceSettings.Where(w => w.Uid == SelectSet.Uid).ToList();
            var SelectNoodle = dBEntities.NoodleNumberSettings.Select(s => s).ToList();
            IWorkbook wb = new XSSFWorkbook();
            ISheet ws;
            string[] TitleName;
            switch (downloadItem)
            {
                case "Set":
                    {
                        var SetData = dBEntities.SetDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Uid == SelectSet.Uid).ToList();
                        ws = wb.CreateSheet($"{SelectSet.TowerName}_Set");
                        string Title = "時間,進風溫度,進風溼球溫度,進風露點溫度,進風相對溼度,進風絕對溼度,進風熱焓,出風溫度,出風溼球溫度,出風露點溫度,出風相對溼度,出風絕對溼度,出風熱焓,熱負載率,電負載率,趨近溫度,範圍溫度(流量計溫差),焓差";
                        TitleName = Title.Split(',');
                        #region 設計標頭
                        ws.CreateRow(0);    //第一行為欄位名稱
                        XSSFCellStyle tStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        tStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                        tStyle.FillPattern = FillPattern.SolidForeground;
                        tStyle.BorderBottom = BorderStyle.Thin;
                        tStyle.BorderLeft = BorderStyle.Thin;
                        tStyle.BorderRight = BorderStyle.Thin;
                        tStyle.BorderTop = BorderStyle.Thin;
                        int titleCount = 0;
                        foreach (string titlename in TitleName)
                        {
                            ws.GetRow(0).CreateCell(titleCount).SetCellValue(titlename);
                            ws.GetRow(0).GetCell(titleCount).CellStyle = tStyle;
                            titleCount++;
                        }
                        #endregion
                        #region 設定明細
                        XSSFCellStyle xStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        xStyle.BorderBottom = BorderStyle.Thin;
                        xStyle.BorderLeft = BorderStyle.Thin;
                        xStyle.BorderRight = BorderStyle.Thin;
                        xStyle.BorderTop = BorderStyle.Thin;
                        int rowCount = 1;
                        foreach (var s in SetData)
                        {
                            ws.CreateRow(rowCount);
                            int cellCount = 0;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InputTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InWetBulbTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InDewPointTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InRelativeHumidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InAbsoluteHumidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InEnthalpy.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutputTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutWetBulbTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutDewPointTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutRelativeHumidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutAbsoluteHumidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutEnthalpy.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.HeatLoadRate.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.ElectricLoadRate.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Appr.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.RangeTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.RangeEnthalpy.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle;
                            rowCount++;
                        }
                        #endregion
                        NpoiMemoryStream stream = new NpoiMemoryStream();
                        stream.AllowClose = false;
                        wb.Write(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.AllowClose = true;
                        return new FileStreamResult(stream, $"application/xlsx") { FileDownloadName = $"{SelectSet.TowerName}_Set歷史紀錄_{DateTimeOffset.Now.ToUnixTimeSeconds()}.xlsx" };
                    }
                case "Cell":
                    {
                        var CellData = dBEntities.CellDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == SelectSet.Uid).ToList();
                        ws = wb.CreateSheet($"{SelectSet.TowerName}_Cell");
                        string Title = "時間,Cell名稱,進風溫度,進風溼球溫度,進風露點溫度,進風絕對溼度,進風熱焓,出風溫度,出風溼球溫度,出風露點溫度,出風絕對溼度,出風熱焓,啟動狀態";
                        TitleName = Title.Split(',');
                        #region 設計標頭
                        ws.CreateRow(0);    //第一行為欄位名稱
                        XSSFCellStyle tStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        tStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                        tStyle.FillPattern = FillPattern.SolidForeground;
                        tStyle.BorderBottom = BorderStyle.Thin;
                        tStyle.BorderLeft = BorderStyle.Thin;
                        tStyle.BorderRight = BorderStyle.Thin;
                        tStyle.BorderTop = BorderStyle.Thin;
                        int titleCount = 0;
                        foreach (string titlename in TitleName)
                        {
                            ws.GetRow(0).CreateCell(titleCount).SetCellValue(titlename);
                            ws.GetRow(0).GetCell(titleCount).CellStyle = tStyle;
                            titleCount++;
                        }
                        #endregion
                        #region 設定明細
                        XSSFCellStyle xStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        xStyle.BorderBottom = BorderStyle.Thin;
                        xStyle.BorderLeft = BorderStyle.Thin;
                        xStyle.BorderRight = BorderStyle.Thin;
                        xStyle.BorderTop = BorderStyle.Thin;
                        int rowCount = 1;
                        foreach (var s in CellData)
                        {
                            string name = SelectCell.Where(w => w.Cellid == s.Uid).Select(s => s.CellName.ToString()).Single();
                            ws.CreateRow(rowCount);
                            int cellCount = 0;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(name);
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InputTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InWetBulbTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InDewPointTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InAbsoluteHumidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InEnthalpy.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutputTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutWetBulbTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutDewPointTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutAbsoluteHumidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutEnthalpy.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.ActionFlag.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle;
                            rowCount++;
                        }
                        #endregion
                        NpoiMemoryStream stream = new NpoiMemoryStream();
                        stream.AllowClose = false;
                        wb.Write(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.AllowClose = true;
                        return new FileStreamResult(stream, $"application/xlsx") { FileDownloadName = $"{SelectSet.TowerName}_Cell歷史紀錄_{DateTimeOffset.Now.ToUnixTimeSeconds()}.xlsx" };
                    }
                case "Flow":
                    {
                        var FlowData = dBEntities.FlowDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == SelectSet.Uid).ToList();
                        ws = wb.CreateSheet($"{SelectSet.TowerName}_Flow");
                        string Title = "時間,Flow名稱,瞬間流量(CMH),瞬間流量(LPM),累積流量,入水溫度,出水溫度,流量百分比%,水溫差,熱負載率,熱負載定值,連線狀態";
                        TitleName = Title.Split(',');
                        #region 設計標頭
                        ws.CreateRow(0);    //第一行為欄位名稱
                        XSSFCellStyle tStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        tStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                        tStyle.FillPattern = FillPattern.SolidForeground;
                        tStyle.BorderBottom = BorderStyle.Thin;
                        tStyle.BorderLeft = BorderStyle.Thin;
                        tStyle.BorderRight = BorderStyle.Thin;
                        tStyle.BorderTop = BorderStyle.Thin;
                        int titleCount = 0;
                        foreach (string titlename in TitleName)
                        {
                            ws.GetRow(0).CreateCell(titleCount).SetCellValue(titlename);
                            ws.GetRow(0).GetCell(titleCount).CellStyle = tStyle;
                            titleCount++;
                        }
                        #endregion
                        #region 設定明細
                        XSSFCellStyle xStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        xStyle.BorderBottom = BorderStyle.Thin;
                        xStyle.BorderLeft = BorderStyle.Thin;
                        xStyle.BorderRight = BorderStyle.Thin;
                        xStyle.BorderTop = BorderStyle.Thin;
                        int rowCount = 1;
                        foreach (var s in FlowData)
                        {
                            string name = SelectFlow.Where(w => w.Flowid == s.Uid).Select(s => s.FlowName.ToString()).Single();
                            ws.CreateRow(rowCount);
                            int cellCount = 0;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(name);
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Flow.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Lflow.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.FlowTotal.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InputTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.OutputTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.FlowPercent.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Rang.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.HeatLoadRate.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.HeatLoad.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.ConnectionFlag.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle;
                            rowCount++;
                        }
                        #endregion
                        NpoiMemoryStream stream = new NpoiMemoryStream();
                        stream.AllowClose = false;
                        wb.Write(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.AllowClose = true;
                        return new FileStreamResult(stream, $"application/xlsx") { FileDownloadName = $"{SelectSet.TowerName}_Flow歷史紀錄_{DateTimeOffset.Now.ToUnixTimeSeconds()}.xlsx" };
                    }
                case "Electric":
                    {
                        var ElectricData = dBEntities.ElectricDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == SelectSet.Uid).ToList();
                        ws = wb.CreateSheet($"{SelectSet.TowerName}_Electric");
                        string Title = "時間,Electric名稱,R相電壓,S相電壓,T相電壓,RS線電壓,ST線電壓,TR線電壓,R相電流,S相電流,T相電流,平均電流,瞬間功率,功率因數,累積瓦時,頻率,電負載率,額定功率,啟動狀態,連線狀態";
                        TitleName = Title.Split(',');
                        #region 設計標頭
                        ws.CreateRow(0);    //第一行為欄位名稱
                        XSSFCellStyle tStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        tStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                        tStyle.FillPattern = FillPattern.SolidForeground;
                        tStyle.BorderBottom = BorderStyle.Thin;
                        tStyle.BorderLeft = BorderStyle.Thin;
                        tStyle.BorderRight = BorderStyle.Thin;
                        tStyle.BorderTop = BorderStyle.Thin;
                        int titleCount = 0;
                        foreach (string titlename in TitleName)
                        {
                            ws.GetRow(0).CreateCell(titleCount).SetCellValue(titlename);
                            ws.GetRow(0).GetCell(titleCount).CellStyle = tStyle;
                            titleCount++;
                        }
                        #endregion
                        #region 設定明細
                        XSSFCellStyle xStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        xStyle.BorderBottom = BorderStyle.Thin;
                        xStyle.BorderLeft = BorderStyle.Thin;
                        xStyle.BorderRight = BorderStyle.Thin;
                        xStyle.BorderTop = BorderStyle.Thin;
                        int rowCount = 1;
                        foreach (var s in ElectricData)
                        {
                            string name = SelectElectric.Where(w => w.Electricid == s.Uid).Select(s => s.ElectricName.ToString()).Single();
                            string hz = "0";
                            string ElectricType = SelectElectric.Where(w => w.Electricid == s.Uid).Select(s => s.ElectricType).ToString();
                            switch (ElectricType)
                            {
                                case "0":
                                    {
                                        hz = s.ActionFlag == true ? "60" : "0";
                                    }
                                    break;
                                case "1":
                                    {
                                        hz = s.Hz.ToString();
                                    }
                                    break;
                            }
                            ws.CreateRow(rowCount);
                            int cellCount = 0;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(name);
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Rv.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Sv.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Tv.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Rsv.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Stv.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Trv.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Ra.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Sa.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Ta.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Aavg.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Kw.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Pf.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Kwh.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(hz.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.ElectricLoadRate.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.RatedPower.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.ActionFlag.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.ConnectionFlag.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle;
                            rowCount++;
                        }
                        #endregion
                        NpoiMemoryStream stream = new NpoiMemoryStream();
                        stream.AllowClose = false;
                        wb.Write(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.AllowClose = true;
                        return new FileStreamResult(stream, $"application/xlsx") { FileDownloadName = $"{SelectSet.TowerName}_Electric歷史紀錄_{DateTimeOffset.Now.ToUnixTimeSeconds()}.xlsx" };
                    }
                    break;
                case "Sensor":
                    {
                        var SensorData = dBEntities.SensorDeviceLogs.Where(w => w.CreateDateTime >= StartTime && w.CreateDateTime <= EndTime && w.Setid == SelectSet.Uid).ToList();
                        ws = wb.CreateSheet($"{SelectSet.TowerName}_Sensor");
                        //string Title = "時間,Sensor名稱,乾球溫度,相對溼度,溼球溫度,露點溫度,絕對溼度,熱焓,進/出風旗標,水塔面,連線狀態";
                        string Title = "時間,Sensor名稱,乾球溫度,相對溼度,溼球溫度,露點溫度,絕對溼度,熱焓,進/出風旗標,連線狀態";
                        TitleName = Title.Split(',');
                        #region 設計標頭
                        ws.CreateRow(0);    //第一行為欄位名稱
                        XSSFCellStyle tStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        tStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index;
                        tStyle.FillPattern = FillPattern.SolidForeground;
                        tStyle.BorderBottom = BorderStyle.Thin;
                        tStyle.BorderLeft = BorderStyle.Thin;
                        tStyle.BorderRight = BorderStyle.Thin;
                        tStyle.BorderTop = BorderStyle.Thin;
                        int titleCount = 0;
                        foreach (string titlename in TitleName)
                        {
                            ws.GetRow(0).CreateCell(titleCount).SetCellValue(titlename);
                            ws.GetRow(0).GetCell(titleCount).CellStyle = tStyle;
                            titleCount++;
                        }
                        #endregion
                        #region 設定明細
                        XSSFCellStyle xStyle = (XSSFCellStyle)wb.CreateCellStyle();
                        xStyle.BorderBottom = BorderStyle.Thin;
                        xStyle.BorderLeft = BorderStyle.Thin;
                        xStyle.BorderRight = BorderStyle.Thin;
                        xStyle.BorderTop = BorderStyle.Thin;
                        int rowCount = 1;
                        foreach (var s in SensorData)
                        {
                            string name = SelectSensor.Where(w => w.Sensorid == s.Uid).Select(s => s.SensorName.ToString()).Single();
                            string noodle = "";
                            if (SelectNoodle.Count != 0)
                            {
                                 noodle = SelectNoodle.Where(w => w.NoodleNumber == s.NoodleNumber).Select(s => s.NoodleName.ToString()).Single();
                            }
                            else
                            {
                                noodle = s.NoodleNumber.ToString();
                            }
                            ws.CreateRow(rowCount);
                            int cellCount = 0;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(name);
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Temp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Humidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.WetBulbTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.DewPointTemp.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.AbsoluteHumidity.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.Enthalpy.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.InOutFlag == true ? "出風" : "進風");
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            //ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(noodle);
                            //ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle; cellCount++;
                            ws.GetRow(rowCount).CreateCell(cellCount).SetCellValue(s.ConnectionFlag.ToString());
                            ws.GetRow(rowCount).GetCell(cellCount).CellStyle = xStyle;
                            rowCount++;
                        }
                        #endregion
                        NpoiMemoryStream stream = new NpoiMemoryStream();
                        stream.AllowClose = false;
                        wb.Write(stream);
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.AllowClose = true;
                        return new FileStreamResult(stream, $"application/xlsx") { FileDownloadName = $"{SelectSet.TowerName}_Sensor歷史紀錄_{DateTimeOffset.Now.ToUnixTimeSeconds()}.xlsx" };
                    }
            }
            return BadRequest("None");
        }
        #region NpoiMethod
        public class NpoiMemoryStream : MemoryStream
        {
            public bool AllowClose { get; set; }
            public NpoiMemoryStream()
            {
                AllowClose = true;
            }

            public override void Close()
            {
                if (AllowClose)
                {
                    base.Close();
                }
            }
        }
        #endregion
    }
}
