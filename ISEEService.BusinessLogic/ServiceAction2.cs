using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Data.Filtering.Helpers;
using ISEEService.DataAccess;
using ISEEService.DataContract;
using ITUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.BusinessLogic
{
    public partial class ServiceAction
    {
        public async ValueTask<tbt_job_header> GET_EMAILAsync(string JOB_ID)
        {
            tbt_job_header dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_EMAILAsync(JOB_ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
            return dataObjects;
        }
        public async ValueTask sendemailStatus(string userid, string JOB_ID)
        {
            var config = await GET_CONFIG();
            var job = await GET_EMAILAsync(JOB_ID);
          
            if (job is not null)
            {
                tbt_email_history hemail = new tbt_email_history
                {
                    email_address = job.email_customer,
                    job_id = job.job_id,
                    customer_id = job.customer_id,
                    license_no = job.license_no,
                    send_by = userid,
                    email_code = "NT",
                    
                };
                await INSERT_TBT_EMAIL_HISTORYAsync(hemail);

                List<string> Cc = new List<string>();
                if (config != null)
                {
                    Cc = config.Where(a => a.config_key == "CC-Mail").Select(a => a.config_value).FirstOrDefault().Split(',')?.ToList();
                }
                MailRequest request = new MailRequest
                {
                    Body = @$"<span>แจ้งถานะการซ่อมบำรุง :{JOB_ID}</span>
                          <a href='http://203.151.136.81/timelinejob?jobid={JOB_ID}'>แสดงสถานะการทำงาน</a>",
                    Subject = "แจ้งถานะการซ่อมบำรุง",
                    //ToEmail = "Dethman_light@hotmail.com"
                    ToEmail = job.email_customer,
                    Cc = Cc

                };
                await IMailService.SendEmailAsync(request);
            }
            
        }
    }
}
