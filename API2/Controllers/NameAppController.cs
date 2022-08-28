
using API2.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NameAppController : ControllerBase
    {
        [HttpGet(Name = "GetNameApp")]
        public string GetNameApp()
        {
            AppName appName = new AppName();
            return appName.GetName();
        }
    }
}
