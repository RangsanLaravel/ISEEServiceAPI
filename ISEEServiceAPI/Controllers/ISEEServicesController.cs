using ISEEService.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISEEService.DataContract;
using Microsoft.AspNetCore.Hosting;
using ITUtility;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ISEEServiceAPI.CustomPolicyProvider;
using Microsoft.Extensions.Options;

namespace ISEEServiceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ISEEServicesController : ControllerBase
    {
        private readonly ServiceAction service = null;
        private readonly IConfiguration Configuration = null;
        private readonly IWebHostEnvironment _hostingEnvironment;

       
        // private readonly MailSettings mailService;
        public ISEEServicesController(IWebHostEnvironment hostingEnvironment, IConfiguration Configuration)
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
            this.service = new ServiceAction(this.Configuration.GetConnectionString("ConnectionSQLServer"), mailSettings);
            //this.mailService = mailService;
        }
        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Running...");
        }
        [HttpGet("CheckConnectDB")]
        public async ValueTask<IActionResult> CheckConnectDB()
        {
            await this.service.CheckConnectDB();
            return Ok();
        }

        #region " STATICDATA "
        [HttpGet("GET_PROVINCE")]
        //[SecurityLevel(2)]
        public async ValueTask<IActionResult> GET_PROVINCE()
        {
            List<tbm_province> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_PROVINCE();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GET_DISTRICT/{province_code}")]
        public async ValueTask<IActionResult> GET_DISTRICT(string province_code)
        {
            List<tbm_district> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_DISTRICT(province_code);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GET_SUB_DISTRICT/{district_code}")]
        public async ValueTask<IActionResult> GET_SUB_DISTRICT(string district_code)
        {
            List<tbm_sub_district> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_SUB_DISTRICT(district_code);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GET_EMPLOYEE_POSITION")]
        public async ValueTask<IActionResult> GET_EMPLOYEE_POSITION()
        {
            List<tbm_employee_position> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_EMPLOYEE_POSITIONAsync();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GET_JOBTYPE")]
        public async ValueTask<IActionResult> GET_JOBTYPE()
        {
            List<tbm_jobtype> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_JOBTYPEAsync();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GET_TBM_IMAGE_TYPE")]
        public async ValueTask<IActionResult> GET_TBM_IMAGE_TYPEAsync()
        {
            List<tbm_image_type> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBM_IMAGE_TYPEAsync();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("CHECKLIST")]
        public async ValueTask<IActionResult> CHECKLIST()
        {
            List<tbm_checklist_group> dataObjects = null;
            try
            {
                dataObjects = await this.service.CHECK_LIST();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GET_UNIT")]
        public async ValueTask<IActionResult> GET_TBM_UNITAsync()
        {
            List<tbm_unit> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBM_UNITAsync();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GET_JOBDETAIL_LIST/{userid}")]

        public async ValueTask<IActionResult> GET_JOBDETAIL_LIST(string userid)
        {
            List<job_detail_list> dataObjects = null;
            try
            {
                if (string.IsNullOrWhiteSpace(userid)) return BadRequest("Require userid");
                userid = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                dataObjects = await this.service.GET_JOB_DETAIL_LISTAsync(userid);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GET_FILE/{ijob_id}/{seq}")]
        public async ValueTask<IActionResult> GET_FILE(string ijob_id, string seq)
        {
            try
            {
                if (string.IsNullOrEmpty(ijob_id)) return BadRequest("Require ijob_id");
                if (string.IsNullOrEmpty(seq)) return BadRequest("Require seq");
                var dataObject = await this.service.GET_PATHFILE(ijob_id, seq);
                if (dataObject is null) return NoContent();
                if (System.IO.File.Exists(dataObject.img_path))
                {
                    DataFile file = new DataFile(dataObject.img_path);
                    file.FileName = dataObject.img_name;
                    return Ok(file);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion " STATICDATA "

        #region " GET "
        [HttpPost("GET_TBM_CUSTOMER")]
        public async ValueTask<IActionResult> GET_TBM_CUSTOMERAsync(tbm_customer data)
        {
            List<tbm_customer> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBM_CUSTOMERAsync(data);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("GET_TBM_EMPLOYEE")]
        public async ValueTask<IActionResult> GET_TBM_EMPLOYEE(tbm_employee data)
        {
            List<tbm_employee> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBM_EMPLOYEEAsync(data);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GET_TBM_VEHICLE")]
        public async ValueTask<IActionResult> GET_TBM_VEHICLEAsync(tbm_vehicle data)
        {
            List<tbm_vehicle> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBM_VEHICLEAsync(data);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("GET_TBM_SERVICES")]
        public async ValueTask<IActionResult> GET_TBM_SERVICESAsync(tbm_services data)
        {
            List<tbm_services> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBM_SERVICESAsync(data);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GET_TBM_LOCATION_STORE")]
        public async ValueTask<IActionResult> GET_TBM_LOCATION_STOREAsync(tbm_location_store condition)
        {
            List<tbm_location_store> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBM_LOCATION_STOREAsync(condition);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GET_TBM_SPAREPART")]

        public async ValueTask<IActionResult> GET_TBM_SPAREPARTAsync(tbm_sparepart condition)
        {
            List<tbm_sparepart> dataObjects = null;
            try
            {
               var user_id = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                dataObjects = await this.service.GET_TBM_SPAREPARTAsync(condition,user_id);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GET_TBM_BRAND")]
        public async ValueTask<IActionResult> GET_BRANDAsync()
        {
            List<tbm_brand> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_BRANDAsync();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GET_TBT_ADJ_SPAREPART_DETAIL/{part_id}")]
        public async ValueTask<IActionResult> GET_TBT_ADJ_SPAREPART_DETAIL(string part_id)
        {
            List<tbt_adj_sparepart> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBT_ADJ_SPAREPART_DETAIL(part_id);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GET_TBT_ADJ_SPAREPART")]
        public async ValueTask<IActionResult> GET_TBT_ADJ_SPAREPART()
        {
            List<tbt_adj_sparepart> dataObjects = null;
            try
            {
                dataObjects = await this.service.GET_TBT_ADJ_SPAREPART();
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion " GET "


        #region " MANAGE JOBE "
        [HttpPost("CREATEJOB")]
        //[SecurityLevel(3)]
        public async ValueTask<IActionResult> INSERT_TBT_JOB_HEADER(create_job data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.type_job)) throw new Exception(" Required type_job");
                if (string.IsNullOrEmpty(data.customer_id)) throw new Exception(" Required customer_id");
                if (string.IsNullOrEmpty(data.email_customer)) throw new Exception(" Required email_customer");
                if (string.IsNullOrEmpty(data.owner_id)) throw new Exception(" Required owner_id");
                if (string.IsNullOrEmpty(data.user_id)) throw new Exception(" Required user_id");
                data.user_id = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBT_JOB_HEADERAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("CLOSEJOB")]
        public async ValueTask<IActionResult> CLOSEJOB([FromBody] close_job data)
        {
            string pathfile = $@"{_hostingEnvironment.ContentRootPath}\Files";
            List<tbt_job_image> job_image = new List<tbt_job_image>();
            try
            {

                //DataFile file = new DataFile(@"D:\TempFile\sig.jpg"); 

                //data.job_images = new List<job_file>()
                //{
                //    new job_file
                //    {
                //        image_type ="sig",
                //        FileData =file.FileData,
                //        ContentType =file.ContentType,
                //        FileName =file.FileName
                //    }
                //};
                data.userid = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                if (data.job_images is not null && data.job_images.Count > 0)
                {
                    if (!System.IO.Directory.Exists(pathfile))
                    {
                        System.IO.Directory.CreateDirectory(pathfile);
                    }
                    foreach (var item in data.job_images)
                    {
                        var image_id = await this.service.GET_IMAGE_ID();
                        var img_path = await manageimage(item, pathfile, image_id);
                        job_image.Add(new tbt_job_image
                        {
                            ijob_id = data.job_id,
                            img_path = img_path,
                            image_type = item.image_type,
                            img_name = item.FileName
                        });
                    }
                }

                await this.service.Close_jobAsync(data, job_image);
                if(data.flg_close == "Y")
                {
                    await SendEmailRpt(data.job_id);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        private async ValueTask SendEmailRpt(string Jobid)
        {
            string filename = "job.pdf";
            string image_type = "rpt";
            string pathfile = $@"{_hostingEnvironment.ContentRootPath}\Files";
            string userid = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
            var file = await service.GET_REPORT_CLOSE_JOB(Jobid);
            var image_id = await this.service.GET_IMAGE_ID();
            job_file jbfile = new job_file
            {
                FileData = Convert.ToBase64String(file.FileData),
                FileName = filename,
                ContentType = "application/pdf",
                image_type = image_type
            };
            var img_path = await manageimage(jbfile, pathfile, image_id);
            tbt_job_image tbt_job_image = new tbt_job_image
            {
                ijob_id = Jobid,
                image_type =image_type,
                img_name =filename,
                img_path = pathfile
            };
            await service.INSERT_TBT_JOB_IMAGE(tbt_job_image, userid, Jobid);
        }

        [HttpPost("GET_CUSTOMER_BY_JOB")]
        public async ValueTask<IActionResult> GET_CUSTOMER(string license_no)
        {
            List<Customer> dataObjects = null;
            try
            {
                if (string.IsNullOrWhiteSpace(license_no)) return BadRequest("Require license_no");
                dataObjects = await this.service.GET_CUSTOMER(license_no);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("GET_JOBREF")]
        public async ValueTask<IActionResult> GET_JOBREFAsync(job_ref job)
        {
            List<tbt_job_header> dataObjects = null;
            try
            {
                if (string.IsNullOrWhiteSpace(job.customer_id)) return BadRequest("Require customer_id");
                if (string.IsNullOrWhiteSpace(job.license_no)) return BadRequest("Require license_no");
                //tbt_job_header job
                dataObjects = await this.service.GET_JOBREFAsync(job);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GET_JOBDETAIL")]

        public async ValueTask<IActionResult> GET_JOB_DETAIL(string job_id)
        {
            close_job dataObjects = null;
            try
            {
                if (string.IsNullOrWhiteSpace(job_id)) return BadRequest("Require job_id");
                dataObjects = await this.service.GET_JOB_DETAIL(job_id);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion " MANAGE JOBE "

        #region " INESERT && UPDATE "
        [HttpPost("INSERT_TBM_CUSTOMER")]
       // [SecurityLevel(3)]
        public async ValueTask<IActionResult> INSERT_TBM_CUSTOMER(tbm_customer data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.id_card)) throw new Exception(" Required id_card");
                if (string.IsNullOrEmpty(data.cust_type)) throw new Exception(" Required cust_type");
                if (string.IsNullOrEmpty(data.fname)) throw new Exception(" Required fname");
                if (string.IsNullOrEmpty(data.lname)) throw new Exception(" Required lname");
                if (string.IsNullOrEmpty(data.province_code)) throw new Exception(" Required province_code");
                if (string.IsNullOrEmpty(data.sub_district_no)) throw new Exception(" Required sub_district_no");
                if (string.IsNullOrEmpty(data.district_code)) throw new Exception(" Required district_code");
                if (string.IsNullOrEmpty(data.zip_code)) throw new Exception(" Required zip_code");
                if (string.IsNullOrEmpty(data.Email)) throw new Exception(" Required Email");

                await this.service.INSERT_TBM_CUSTOMERAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("INSERT_TBM_EMPLOYEE")]
        //[SecurityLevel(3)]
        public async ValueTask<IActionResult> INSERT_TBM_EMPLOYEE(employee data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.user_name)) throw new Exception(" Required user_name");
                if (string.IsNullOrEmpty(data.password)) throw new Exception(" Required password");
                if (string.IsNullOrEmpty(data.fullname)) throw new Exception(" Required fullname");
                if (string.IsNullOrEmpty(data.lastname)) throw new Exception(" Required lastname");
                if (string.IsNullOrEmpty(data.create_by)) throw new Exception(" Required create_by");
                if (string.IsNullOrEmpty(data.idcard)) throw new Exception(" Required idcard");
                data.create_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBM_EMPLOYEEAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("INSERT_TBM_VEHICLE")]
        public async ValueTask<IActionResult> INSERT_TBM_VEHICLE(tbm_vehicle data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.license_no)) throw new Exception(" Required license_no");
                if (string.IsNullOrEmpty(data.service_no)) throw new Exception(" Required service_no");
                if (string.IsNullOrEmpty(data.contract_no)) throw new Exception(" Required contract_no");
                if (string.IsNullOrEmpty(data.customer_id)) throw new Exception(" Required customer_id");
                //data.create_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBM_VEHICLEAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("INSERT_TBM_SERVICES")]
        public async ValueTask<IActionResult> INSERT_TBM_SERVICES(tbm_services data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.jobcode)) throw new Exception(" Required jobcode");
                if (string.IsNullOrEmpty(data.services_name)) throw new Exception(" Required services_name");
                data.create_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBM_SERVICESAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("INSERT_TBM_LOCATION_STORE")]
        public async ValueTask<IActionResult> INSERT_TBM_LOCATION_STORE(tbm_location_store data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.location_name)) throw new Exception(" Required location_name");
                if (string.IsNullOrEmpty(data.owner_id)) throw new Exception(" Required owner_id");
                data.create_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBM_LOCATION_STOREAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("INSERT_TBM_SPAREPART")]
        public async ValueTask<IActionResult> INSERT_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.part_no)) throw new Exception(" Required part_no");
                if (string.IsNullOrEmpty(data.part_name)) throw new Exception(" Required part_name");
                if (string.IsNullOrEmpty(data.part_desc)) throw new Exception(" Required part_desc");
                if (string.IsNullOrEmpty(data.part_type)) throw new Exception(" Required part_type");
                if (string.IsNullOrEmpty(data.location_id)) throw new Exception(" Required location_id");
                data.create_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBM_SPAREPARTAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("INSERT_TBT_ADJ_SPAREPART")]
        public async ValueTask<IActionResult> INSERT_TBT_ADJ_SPAREPARTAsync(tbt_adj_sparepart data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.part_no)) throw new Exception(" Required part_no");
                if (string.IsNullOrEmpty(data.part_id)) throw new Exception(" Required part_id");
                if (string.IsNullOrEmpty(data.adj_part_value)) throw new Exception(" Required adj_part_value");
                data.create_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                await this.service.INSERT_TBT_ADJ_SPAREPARTAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion " INESERT && UPDATE "

        #region " TERMINATE "

        [HttpPost("TERMINATE_TBM_CUSTOMER")]
       // [SecurityLevel(3)]
        public async ValueTask<IActionResult> TERMINATE_TBM_CUSTOMERAsync(model_terminate data)
        {
            try
            {
                if (data is not null)
                {
                    customer_terminate cus = new customer_terminate
                    {
                        user_id = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault(),
                        customer_id = new List<string>()
                    };
                    foreach (var item in data.data)
                    {
                        cus.customer_id.Add(item.id);
                    }

                    await this.service.TERMINATE_TBM_CUSTOMERAsync(cus);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TERMINATE_TBM_EMPLOYEE")]
        //[SecurityLevel(3)]
        public async ValueTask<IActionResult> TERMINATE_TBM_EMPLOYEEAsync(model_terminate data)
        {
            try
            {
                if (data is not null)
                {
                    employee_terminate em = new employee_terminate
                    {
                        terminate_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault(),
                        user_id = new List<string>()
                    };
                    foreach (var item in data.data)
                    {
                        em.user_id.Add(item.id);
                    }
                    await this.service.TERMINATE_TBM_EMPLOYEEAsync(em);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TERMINATE_TBM_LOCATION_STORE")]
       // [SecurityLevel(3)]
        public async ValueTask<IActionResult> TERMINATE_TBM_LOCATION_STOREAsync(model_terminate data)
        {
            try
            {
                if (data is not null)
                {
                    location_store_terminate loc = new location_store_terminate
                    {
                        user_id = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault(),
                        location_id = new List<string>()
                    };
                    foreach (var item in data.data)
                    {
                        loc.location_id.Add(item.id);
                    }
                    await this.service.TERMINATE_TBM_LOCATION_STOREAsync(loc);

                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TERMINATE_TBM_SERVICES")]
        public async ValueTask<IActionResult> TERMINATE_TBM_SERVICESAsync(model_terminate data)
        {
            try
            {
                if (data is not null)
                {
                    service_terminate ser = new service_terminate
                    {
                        user_id = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault(),
                        services_no = new List<string>()
                    };
                    foreach (var item in data.data)
                    {
                        ser.services_no.Add(item.id);
                    }
                    await this.service.TERMINATE_TBM_SERVICESAsync(ser);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TERMINATE_TBM_SPAREPART")]
        public async ValueTask<IActionResult> TERMINATE_TBM_SPAREPARTAsync(model_terminate data)
        {
            try
            {
                if (data is not null)
                {
                    sparepart_terminate spar = new sparepart_terminate
                    {
                        user_id = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault(),
                        part_detail = new List<sparepart_id>()
                    };
                    foreach (var item in data.data)
                    {
                        spar.part_detail.Add(new sparepart_id { part_id = Convert.ToInt32(item.id), cancel_reason = item.msg });
                    }
                    await this.service.TERMINATE_TBM_SPAREPARTAsync(spar);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TERMINATE_TBT_ADJ_SPAREPART")]
        public async ValueTask<IActionResult> TERMINATE_TBT_ADJ_SPAREPARTAsync(tbt_adj_sparepart data)
        {
            try
            {
                if (data is not null)
                {
                    data.cancel_by = User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
                    await this.service.TERMINATE_TBT_ADJ_SPAREPARTAsync(data);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TERMINATE_TBT_JOB_IMAGE")]
        public async ValueTask<IActionResult> TERMINATE_TBT_JOB_IMAGEAsync(string ijob_id, string seq)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ijob_id)) return BadRequest("Require ijob_id");
                if (string.IsNullOrWhiteSpace(seq)) return BadRequest("Require seq");
                await this.service.TERMINATE_TBT_JOB_IMAGE(ijob_id, seq);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion " TERMINATE "

        [AllowAnonymous]
        [HttpPost("Login")]
        public async ValueTask<IActionResult> Login(UserLogin user)
        {
            employee_info employee_Info = null;
            try
            {
                var isNotValid = string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password);
                if (isNotValid)
                {
                    return NotFound("ไม่พบข้อมูลผู้ใช้งาน");
                }
                employee_Info = await this.service.UserLogin(user);
                if (employee_Info is null)
                {
                    return BadRequest("UserName or Password invalid ");
                }
                else
                {
                    var token = await BuildToken(employee_Info);
                    var menu = await this.service.GET_MENU(employee_Info.user_id);
                    employee_Info.token = token;
                    employee_Info.menu = new List<tbm_menu>();
                    employee_Info.menu = menu;
                    return Ok(employee_Info);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private async ValueTask<string> BuildToken(employee_info employee)
        {
            // key is case-sensitive
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                new Claim(DynamicPilicies.SecurityLevel ,employee.security_level),
                new Claim("id", employee.user_id),
                new Claim("username", employee.user_name),
                new Claim("position", employee.position),
                new Claim("position_description", employee.position_description),
            //ใช้ role เพื่อลดการโหลดดาต้าเบส
                new Claim(ClaimTypes.Role, employee.position)
            };
            var expires = DateTime.Now.AddDays(Convert.ToDouble(Configuration["Jwt:ExpireDay"]));
            //แก้วันที่ได้
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async ValueTask<string> manageimage(job_file job_File, string path, long? image_id)
        {
            string typefile = job_File.FileName.Split('.').LastOrDefault() != null ? $".{job_File.FileName.Split('.').LastOrDefault()}" : "";
            string fullpath = $@"{path}\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}";
            string filename = $"{image_id}{typefile}";
            if (!System.IO.Directory.Exists(fullpath))
            {
                System.IO.Directory.CreateDirectory(fullpath);
            }
            string img_path = $@"{fullpath}\{filename}";
            await System.IO.File.WriteAllBytesAsync(img_path, Convert.FromBase64String(job_File.FileData));
            return img_path;
        }

        #region " REPORT "
        [AllowAnonymous]
        [HttpGet("GET_REPORT_CLOSE_JOB/{jobid}")]
        public async ValueTask<IActionResult> GET_REPORT_CLOSE_JOB(string jobid)
        {
            await SendEmailRpt(jobid);
            return Ok();
        }
        [HttpPost("GET_Summary_job_list")]
        //[SecurityLevel(3)]
        public async ValueTask<IActionResult> GET_Summary_job_list(summary_job condition)
        {
            summary_job_list_condition dataObjects = null;
            try
            {
                if(condition is not null)
                {
                    dataObjects = new summary_job_list_condition
                    {
                        close_dt_from = condition.close_dt_from,
                        close_dt_to = condition.close_dt_to,
                        customer_name = condition.customer_name,
                        fix_date_from = condition.fix_date_from,
                        fix_date_to = condition.fix_date_to,
                        job_date_from = condition.job_date_from,
                        job_date_to = condition.job_date_to,
                        license_no = condition.license_no,
                        report_type = condition.report_type,
                        Teachnicial = condition.Teachnicial,
                        type_job = condition.type_job,
                        user_id = condition.user_id,
                        user_print = condition.user_print
                    };
                }
                dataObjects = await this.service.GET_Summary_job_list(dataObjects);
                if (dataObjects.report_type.ToUpper() == "SEARCH")
                {
                    return Ok(dataObjects);
                }
                else if (dataObjects.report_type.ToUpper() == "PDF")
                {
                    var pdf = await this.service.GET_REPORT_Summary_job_list(dataObjects);
                    return Ok(pdf);

                }
                else
                {
                    var xlsx = await this.service.GET_REPORT_Summary_job_list(dataObjects);
                    return Ok(xlsx);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("GET_Summary_stock_list")]
        //[SecurityLevel(3)]
        public async ValueTask<IActionResult> GET_Summary_stock_list(summary_stock condition)
        {
            summary_stock_list_condition dataObjects = null;           
           // List<summary_stock_list> dataObjects = null;
            try
            {
                //User.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault()
                if (condition is not null)
                {
                    dataObjects = new summary_stock_list_condition
                    {
                        locationid =condition.locationid,
                        PartId =condition.PartId,
                        Partno =condition.Partno,
                        Part_create_from =condition.Part_create_from,
                        Part_create_to =condition.Part_create_to,
                        report_type=condition.report_type,
                        UserName = condition.UserName
                       
                    };
                }

                dataObjects = await this.service.GET_Summary_stock_list(dataObjects);
                if (dataObjects.report_type.ToUpper() == "SEARCH")
                {
                    return Ok(dataObjects);
                }
                else if (dataObjects.report_type.ToUpper() == "PDF")
                {
                    var pdf = await this.service.GET_REPORT_Summary_stock_list(dataObjects);
                    return Ok(pdf);

                }
                else
                {
                    var xlsx = await this.service.GET_REPORT_Summary_stock_list(dataObjects);
                    return Ok(xlsx);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion " REPORT "





    }
}
