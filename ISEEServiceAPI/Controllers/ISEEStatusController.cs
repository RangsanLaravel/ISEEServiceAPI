using ISEEService.BusinessLogic;
using ISEEService.DataContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ISEEServiceAPI.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ISEEStatusController : ControllerBase
    {
        private readonly ServiceAction service = null;
        private readonly IConfiguration Configuration = null;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ISEEStatusController(IWebHostEnvironment hostingEnvironment, IConfiguration Configuration)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.Configuration = Configuration;

            MailSettings mailSettings = new MailSettings
            {
                DisplayName = Configuration["MailSettings:DisplayName"],
                Host = Configuration["MailSettings:Host"],
                Mail = Configuration["MailSettings:Mail"],
                Password = Configuration["MailSettings:Password"],
                Port = Convert.ToInt32(Configuration["MailSettings:Port"])
            };
            this.service = new ServiceAction(this.Configuration.GetConnectionString("ConnectionSQLServer"), mailSettings, Configuration["ConfigSetting:DBENV"]);
            //this.mailService = mailService;
        }
        
    }
}
