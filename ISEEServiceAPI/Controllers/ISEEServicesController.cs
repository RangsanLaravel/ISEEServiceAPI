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

namespace ISEEServiceAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ISEEServicesController : ControllerBase
    {
        private readonly ServiceAction service = null;
        private readonly IConfiguration Configuration = null;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ISEEServicesController(IWebHostEnvironment hostingEnvironment, IConfiguration Configuration)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.Configuration = Configuration;
            this.service = new ServiceAction(this.Configuration.GetConnectionString("ConnectionSQLServer"));
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
                dataObjects = await this.service.GET_JOB_DETAIL_LISTAsync(userid);
                return Ok(dataObjects);
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
                dataObjects = await this.service.GET_TBM_SPAREPARTAsync(condition);
                return Ok(dataObjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion " GET "

        #region " TEST READ WRITE FILE "

        [HttpPost("WRITEFILE")]
        public async ValueTask<IActionResult> WRITEFILE( )
        {
            string pathfile = $@"{_hostingEnvironment.ContentRootPath}\Files";
            try
            {
                var files = "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";
                close_job data = new close_job
                {
                    job_images = new List<job_file>()
                };
               
                data.job_images = new List<job_file>()
                {
                    new job_file
                    {
                        image_type ="sig",
                        FileData =System.Convert.FromBase64String(files),
                        ContentType ="image/png",
                        FileName ="test.png"
                    }
                };
                if (data.job_images is not null && data.job_images.Count > 0)
                {
                    if (!System.IO.Directory.Exists(pathfile))
                    {
                        System.IO.Directory.CreateDirectory(pathfile);
                    }
                    foreach (var item in data.job_images)
                    {
                        var image_id = 1;
                        var img_path = await manageimage(item, pathfile, image_id);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("READFILE")]
        public async ValueTask<IActionResult> READFILE(string imageid)
        {
            string pathfile = $@"{_hostingEnvironment.ContentRootPath}\Files";
            try
            {
                if (!System.IO.Directory.Exists(pathfile))
                {
                    System.IO.Directory.CreateDirectory(pathfile);
                }
                string typefile = ".png";
                string fullpath = $@"{pathfile}\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}";
                string filename = $"{imageid}{typefile}";
                var file = System.IO.File.ReadAllBytes(@$"{fullpath}\{filename}");
                return File(file, "image/png");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        #endregion " TEST READ WRITE FILE "


        #region " MANAGE JOBE "
        [HttpPost("CREATEJOB")]
        public async ValueTask<IActionResult> INSERT_TBT_JOB_HEADER(create_job data)
        {
            try
            {
                await this.service.INSERT_TBT_JOB_HEADERAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("CLOSEJOB")]
        public async ValueTask<IActionResult> CLOSEJOB([FromForm] close_job data)
        {
            string pathfile = $@"{_hostingEnvironment.ContentRootPath}\Files";
            List<tbt_job_image> job_image = null;
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

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
        public async ValueTask<IActionResult> INSERT_TBM_CUSTOMER(tbm_customer data)
        {
            try
            {
                await this.service.INSERT_TBM_CUSTOMERAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("INSERT_TBM_EMPLOYEE")]
        public async ValueTask<IActionResult> INSERT_TBM_EMPLOYEE(employee data)
        {
            try
            {
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
                await this.service.INSERT_TBM_SPAREPARTAsync(data);
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
        public async ValueTask<IActionResult> TERMINATE_TBM_CUSTOMERAsync(model_terminate data)
        {
            try
            {
                if (data is not null)
                {
                    customer_terminate cus = new customer_terminate
                    {
                        user_id = data.user_id,
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
        public async ValueTask<IActionResult> TERMINATE_TBM_EMPLOYEEAsync(model_terminate data)
        {
            try
            {
                if(data is not null)
                {
                    employee_terminate em = new employee_terminate
                    {
                        terminate_by = data.user_id,
                        user_id =new List<string>()
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
        public async ValueTask<IActionResult> TERMINATE_TBM_LOCATION_STOREAsync(model_terminate data)
        {
            try
            {
                if(data is not null)
                {
                    location_store_terminate loc = new location_store_terminate
                    {
                        user_id =data.user_id,
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
                if(data is not null)
                {
                    service_terminate ser = new service_terminate
                    {
                        user_id = data.user_id,
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
                if(data is  not null)
                {
                    sparepart_terminate spar = new sparepart_terminate
                    {
                        user_id = data.user_id,
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
                    return Unauthorized();
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
            await System.IO.File.WriteAllBytesAsync(img_path, job_File.FileData);
            return img_path;
        }
    }
}
