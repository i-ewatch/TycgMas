using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TycgMas.Models;
using TycgMas.Models.TycgMasEntities;

namespace TycgMas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DAUController : ControllerBase
    {
        private readonly ILogger<DataCatcherController> _logger;
        private TycgMasDBContext dBEntities;

        public DAUController(ILogger<DataCatcherController> logger)
        {
            _logger = logger;
            dBEntities = new TycgMasDBContext();
        }
        [HttpGet("/api/get-dau")]
        public ActionResult DAUData()
        {
            List<DAU> dAUs = new List<DAU>();
            var SelectSet = dBEntities.SetDeviceSettings.Select(s => s).ToList();
            foreach (var Setitem in SelectSet)
            {
                DAU dAU = new DAU
                {
                    name = Setitem.TowerName,
                };
                dAU.States.Add(new state()
                {
                    Name = "PLC 1 看門狗",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "PLC 2 看門狗",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "PLC 1 狀態",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "PLC 2 狀態",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "UPS 狀態",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "MGuard 狀態",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "機櫃溫度",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "機櫃門狀態",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "電池模式狀態",
                    Status = "NONE"
                });
                dAU.States.Add(new state()
                {
                    Name = "電池加載狀態",
                    Status = "NONE"
                });
                dAUs.Add(dAU);
            }
            return Ok(dAUs);
        }
    }
}
