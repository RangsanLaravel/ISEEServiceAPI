using ISEEService.BusinessLogic;
using ISEEService.DataContract;
using ITUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
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
        [HttpGet("get_substatus/{job_id}")]
        public async ValueTask<IActionResult> get_substatus(string job_id)
        {
            try
            {
                if (string.IsNullOrEmpty(job_id)) return BadRequest("Require job_id");
                var result = await this.service.TBT_JOB_SUBSTATUSAsync(job_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateStatus")]
        public async ValueTask<IActionResult> UpdateStatus(update_status data)
        {
            try
            {
                var userid = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBM_SUBSTATUSAsync(data, userid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
