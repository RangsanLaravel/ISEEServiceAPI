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
        public async ValueTask<tbm_employee> GET_EMAILEmployeeAsync(string user_id)
        {
            tbm_employee dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_EMAILEmployeeAsync(user_id);
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
               // await IMailService.SendEmailAsync(request);รอใช้งานจริง
            }
            
        }

        public async ValueTask sendemailStatusEmployee(string userid_assign,string userid, string JOB_ID)
        {
            var config = await GET_CONFIG();
            var employee = await GET_EMAILEmployeeAsync(userid_assign);

            if (employee?.email is not null)
            {
                tbt_email_history hemail = new tbt_email_history
                {
                    email_address = employee.email,
                    job_id = JOB_ID,
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
                    Body = @$"<span>แจ้งสถานะการ Assign job :{JOB_ID}</span>
                          <a href='http://203.151.136.81/timelinejob?jobid={JOB_ID}'>แสดงสถานะการทำงาน</a>",
                    Subject = "แจ้งสถานะการ Assign job",
                    //ToEmail = "Dethman_light@hotmail.com"
                    ToEmail = employee.email,
                    Cc = Cc

                };
                await IMailService.SendEmailAsync(request);
            }

        }

        public async ValueTask INSERT_TBM_SUBSTATUSAsync(update_status data,string userid)
        {
            tbt_job_header dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                //await repository.INSERT_TBM_SUBSTATUSAsync(Jobid, "I", "INR", "", userid);
                await repository.INSERT_TBM_SUBSTATUSAsync(data.jobid,"I",data.statusid,data.remark,userid);
                if(data.userid != null)
                {
                    await repository.UPDATE_OWNERID(data.userid,data.jobid, userid);
                    await  sendemailStatusEmployee(data.userid, data.jobid, userid);
                }
                await repository.CommitTransection();

            }
            catch (Exception ex)
            {
                await repository.RollbackTransection();
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
           
        }
    }
}
