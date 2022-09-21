using CryptoHelper;
//using ISEEService.BusinessLogic.report;
using ISEEService.DataAccess;
using ISEEService.DataContract;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISEEService.BusinessLogic.Extensions;
using ITUtility;
using MimeKit;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ISEEService.BusinessLogic.report;
using DataAccessUtility;


namespace ISEEService.BusinessLogic
{
    public partial class ServiceAction
    {
        private readonly string _connectionstring = string.Empty;
        private readonly CultureInfo culture = new CultureInfo("th-TH");
        private readonly IMailService IMailService;
        private readonly string DBENV = string.Empty;
        //string contentRootPath = _hostingEnvironment.ContentRootPath + @"\ImageMaster";
        public ServiceAction(string connectionstring, MailSettings mailSettings, string DBENV)
        {
            this._connectionstring = connectionstring;
            this.IMailService = new MailService(mailSettings);
            this.DBENV = DBENV;
        }
        public async ValueTask CheckConnectDB()
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            try
            {
                await repository.OpenConnectionAsync();


                await repository.CloseConnectionAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async ValueTask<long?> CHECK_STOCK(string part_id, string location_id)
        {
            long? dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.CHECK_STOCK(part_id, location_id);
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
        public async ValueTask<List<tbm_province>> GET_PROVINCE()
        {
            List<tbm_province> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_PROVINCEAsync();
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
        public async ValueTask<List<tbm_district>> GET_DISTRICT(string province_code)
        {
            List<tbm_district> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();

            try
            {
                dataObjects = await repository.GET_DISTRICTAsync(province_code);
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

        public async ValueTask<List<tbm_sub_district>> GET_SUB_DISTRICT(string district_code)
        {
            List<tbm_sub_district> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_SUB_DISTRICTAsync(district_code);
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

        public async ValueTask<List<tbm_employee_position>> GET_EMPLOYEE_POSITIONAsync()
        {
            List<tbm_employee_position> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_EMPLOYEE_POSITIONAsync();
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

        public async ValueTask<List<tbm_jobtype>> GET_JOBTYPEAsync()
        {
            List<tbm_jobtype> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_JOBTYPEAsync();
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
        public async ValueTask<List<tbm_unit>> GET_TBM_UNITAsync()
        {
            List<tbm_unit> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_UNITAsync();
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

        public async ValueTask<List<Customer>> GET_CUSTOMER(string license_no)
        {
            List<Customer> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_CUSTOMERAsync(license_no);
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
        public async ValueTask<List<tbm_brand>> GET_BRANDAsync()
        {
            List<tbm_brand> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_BRANDAsync();
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
        public async ValueTask<List<tbt_job_header>> GET_JOBREFAsync(job_ref data)
        {
            List<tbt_job_header> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_JOBREFAsync(data);
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

        public async ValueTask<List<tbm_employee>> GET_TBM_EMPLOYEEAsync(tbm_employee data)
        {
            List<tbm_employee> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_EMPLOYEEAsync(data);
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

        public async ValueTask<List<tbm_customer>> GET_TBM_CUSTOMERAsync(tbm_customer data)
        {
            List<tbm_customer> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_CUSTOMERAsync(data);

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

        public async ValueTask<List<tbm_vehicle>> GET_TBM_VEHICLEAsync(tbm_vehicle data)
        {
            List<tbm_vehicle> dataObjects = null;
            DateTime? sExpire_date = null;
            DateTime? eExpire_date = null;
            DateTime? sEffective_date = null;
            DateTime? eEffective_date = null;
            TimeSpan timestart = new TimeSpan(0, 0, 0);
            TimeSpan timelast = new TimeSpan(23, 59, 59);

            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                if (data is not null)
                {
                    if (!string.IsNullOrWhiteSpace(data.expire_date))
                    {
                        sExpire_date = DateTime.ParseExact(data.expire_date, "dd/MM/yyyy", culture);
                        sExpire_date = sExpire_date.Value.Add(timestart);
                        eExpire_date = sExpire_date.Value.Add(timelast);
                    }
                    if (!string.IsNullOrWhiteSpace(data.effective_date))
                    {
                        sEffective_date = DateTime.ParseExact(data.effective_date, "dd/MM/yyyy", culture);
                        sEffective_date = sEffective_date.Value.Add(timestart);
                        eEffective_date = sEffective_date.Value.Add(timelast);
                    }

                }
                dataObjects = await repository.GET_TBM_VEHICLEAsync(data, sExpire_date, eExpire_date, sEffective_date, eEffective_date);
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

        public async ValueTask<List<tbm_location_store>> GET_TBM_LOCATION_STOREAsync(tbm_location_store condition)
        {
            List<tbm_location_store> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_LOCATION_STOREAsync(condition);
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

        public async ValueTask<List<tbm_services>> GET_TBM_SERVICESAsync(tbm_services data)
        {
            List<tbm_services> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_SERVICESAsync(data);
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

        public async ValueTask<string> GET_TBM_SERVICES_BY_JOBTYPE(string JOBTYPE)
        {
            string dataObjects = string.Empty;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_SERVICES_BY_JOBTYPE(JOBTYPE);
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
        public async ValueTask<List<tbm_sparepart>> GET_TBM_SPAREPARTAsync(tbm_sparepart data, string userid)
        {
            List<tbm_sparepart> dataObjects = null;
            tbm_employee emp = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                emp = await repository.GET_EM_PERMISSIONAsync(userid);
                if (data.page == "STOCK")
                {
                    if (emp is not null)
                    {
                        if (emp.position == "MN" || emp.position == "OS")
                        {
                            if (emp.showstock == "1")
                            {
                                dataObjects = await repository.GET_TBM_SPAREPARTAsync(data);
                            }
                            else
                            {
                                //var store = await repository.GET_LocalAsync(userid);
                                data.location_id = emp.location_id;
                                dataObjects = await repository.GET_TBM_SPAREPARTAsync(data);
                            }
                        }
                        else
                        {
                            dataObjects = await repository.GET_TBM_SPAREPARTAsync(data);
                        }
                    }
                }
                else if (data.page == "JOB")
                {
                    if (emp.position == "MN" || emp.position == "OS")
                    {
                        //var store = await repository.GET_LocalAsync(userid);
                        data.location_id = emp.location_id;
                        dataObjects = await repository.GET_TBM_SPAREPARTAsync(data);
                    }
                    else
                    {
                        dataObjects = await repository.GET_TBM_SPAREPARTAsync(data);
                    }
                }
                else
                {
                    dataObjects = await repository.GET_TBM_SPAREPARTAsync(data);

                }

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

        public async ValueTask<List<tbm_sparepart>> sp_tbm_sparepart_detail(string part_id)
        {
            List<tbm_sparepart> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.sp_tbm_sparepart_detail(part_id);
                if(dataObjects is not null)
                {
                    if(string.IsNullOrWhiteSpace(dataObjects[0].path_image))
                    {
                        dataObjects[0].path_image = imgnotfound;
                    }
                    else
                    {
                        if (System.IO.File.Exists(dataObjects[0].path_image))
                        {
                           var pic = System.IO.File.ReadAllBytes(dataObjects[0].path_image);
                            dataObjects[0].path_image = $"data:image/png;base64,{Convert.ToBase64String(pic)}";
                        }
                        else
                        {
                            dataObjects[0].path_image = imgnotfound;
                        }
                    }

                    
                }
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
        public async ValueTask<long?> GET_IMAGE_ID()
        {
            long? dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_IMAGE_ID();
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

        public async ValueTask<tbt_job_image> CHECK_RESEND_EMAIL(string Jobid)
        {
            tbt_job_image dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.CHECK_RESEND_EMAIL(Jobid);
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
        public async ValueTask<long> GET_SEQ_IMAGEAsync(string ijob_id)
        {
            long dataObjects = 0;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_SEQ_IMAGEAsync(ijob_id);
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
        public async ValueTask<List<tbt_adj_sparepart>> GET_TBT_ADJ_SPAREPART_DETAIL(string part_id)
        {
            List<tbt_adj_sparepart> dataObjects = new List<tbt_adj_sparepart>();
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBT_ADJ_SPAREPART_DETAIL(part_id);
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
        public async ValueTask<List<tbt_adj_sparepart>> GET_TBT_ADJ_SPAREPART(string adj_type)
        {
            List<tbt_adj_sparepart> dataObjects = new List<tbt_adj_sparepart>();
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBT_ADJ_SPAREPART( adj_type);
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

        #region " GET JOB DETAIL "
        public async ValueTask<close_job> GET_JOB_DETAIL(string job_id)
        {
            close_job dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                var ds = await repository.sp_get_tbm_job_data_detail(job_id);
                if(ds.Tables.Count > 0)
                {
                    dataObjects = ds.Tables[0].AsEnumerable<close_job>().FirstOrDefault();
                    if (dataObjects is not null)
                    {
                        dataObjects.job_detail = new tbt_job_detail();
                        dataObjects.job_detail = ds.Tables[1].AsEnumerable<tbt_job_detail>().FirstOrDefault();
                        dataObjects.job_checklists = new List<tbt_job_checklist>();
                        dataObjects.job_checklists = ds.Tables[2].AsEnumerable<tbt_job_checklist>().ToList();
                        dataObjects.job_parts = new List<tbt_job_part>();
                        dataObjects.job_parts = ds.Tables[3].AsEnumerable<tbt_job_part>().ToList();
                        dataObjects.image_detail = new List<tbt_job_image>();
                        dataObjects.image_detail = ds.Tables[4].AsEnumerable<tbt_job_image>().ToList();
                    }
                }
                //dataObjects = await repository.GET_TBT_JOB_HEADERAsync(job_id);
                //if (dataObjects is not null)
                //{
                //    dataObjects.job_detail = new tbt_job_detail();
                //    dataObjects.job_detail = await repository.GET_TBT_JOB_DETAILAsync(job_id);
                //    dataObjects.job_checklists = new List<tbt_job_checklist>();
                //    dataObjects.job_checklists = await repository.GET_TBT_JOB_CHECKLISTAsync(job_id);
                //    dataObjects.job_parts = new List<tbt_job_part>();
                //    dataObjects.job_parts = await repository.GET_TBT_JOB_PARTAsync(job_id);
                //    dataObjects.image_detail = new List<tbt_job_image>();
                //    dataObjects.image_detail = await repository.GET_TBT_JOB_IMAGEAsync(job_id);
                //}
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

        public async ValueTask<List<job_detail_list>> GET_JOB_DETAIL_LISTAsync(string userid)
        {
            List<job_detail_list> dataObjects = null;
           // bool isAdmin = true;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                //isAdmin = await repository.CheckPermissionAdmin(userid);
                dataObjects = await repository.GET_JOB_DETAIL_LISTAsync(userid);
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
        public async ValueTask<tbt_job_header> GET_TBT_JOB(string job_id)
        {
            tbt_job_header dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                // isAdmin = await repository.CheckPermissionAdmin(userid);
                dataObjects = await repository.GET_TBT_JOB(job_id);
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
        public async ValueTask<bool> CheckPermissionAdmin(string userid)
        {
            //tbt_job_header dataObjects = null;
            bool isAdmin = false;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                isAdmin = await repository.CheckPermissionAdmin(userid);
                // dataObjects = await repository.GET_TBT_JOB(job_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
            return isAdmin;
        }
        public async ValueTask<tbt_job_image> GET_PATHFILE(string ijob_id, string seq)
        {
            tbt_job_image dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_PATHFILE(ijob_id, seq);
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

        #endregion " GET JOB DETAIL "


        public async ValueTask<string> GET_SP_GET_RUNNING_NOAsync(string running_type)
        {
            string dataObjects = string.Empty;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_SP_GET_RUNNING_NOAsync(running_type);
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

        public async ValueTask<long> GET_TBT_JOB_PART_SEQ(string job_id)
        {
            long dataObjects = 0;
            Repository repository = new Repository(_connectionstring, DBENV);
            
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                dataObjects = await repository.GET_TBT_JOB_PART_SEQ(job_id);
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
            return dataObjects;
        }
        public async ValueTask<List<tbt_job_part>> GET_TBT_JOB_PART(string pjob_id)
        {
            List<tbt_job_part> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBT_JOB_PART(pjob_id);
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

        public async ValueTask<List<tbt_job_detail>> GET_TBT_JOB_DETAIL(tbt_job_detail tbt_job_detail)
        {
            List<tbt_job_detail> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBT_JOB_DETAIL(tbt_job_detail);
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

        public async ValueTask<List<tbm_image_type>> GET_TBM_IMAGE_TYPEAsync()
        {
            List<tbm_image_type> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_IMAGE_TYPEAsync();
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


        public async ValueTask<List<tbm_checklist_group>> CHECK_LIST()
        {
            List<tbm_checklist_group> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_CHECKLIST_GROUP();
                if (dataObjects is not null)
                {
                    foreach (var item in dataObjects)
                    {
                        item.check_list = new List<tbm_checklist>();
                        item.check_list = await repository.GET_TBM_CHECKLIST(item.ch_group_id);

                    }
                }
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

        public async ValueTask<employee_info> UserLogin(UserLogin user)
        {

            employee_info employee_Info = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {

                employee_Info = await repository.UserLogin(user);
                if (employee_Info is null)
                    return employee_Info;
                if (!Crypto.VerifyHashedPassword(employee_Info.password, user.Password))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
            return employee_Info;

        }

        public async ValueTask<List<tbm_menu>> GET_MENU(string user_id)
        {

            List<tbm_menu> dataObjects = null;
            List<tbm_config> configs = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {

                dataObjects = await repository.GET_MENU(user_id);
                configs = await repository.GET_TBM_CONFIG();
                if (dataObjects is not null && configs is not null)
                {
                    dataObjects.Select(a => { a.config = new List<tbm_config>(); a.config = configs; return a; }).ToList();
                }

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
        public async ValueTask<List<tbm_config>> GET_CONFIG()
        {
            List<tbm_config> configs = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                configs = await repository.GET_TBM_CONFIG();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
            return configs;

        }

        public async ValueTask INSERT_TBM_EMPLOYEEAsync(employee data)
        {

            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {

                if (string.IsNullOrWhiteSpace(data.user_id))
                {
                    data.password = Crypto.HashPassword(data.password);
                    await repository.INSERT_TBM_EMPLOYEEAsync(data);
                }
                else
                {
                    await repository.UPDATE_TBM_EMPLOYEEAsync(data);
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

        public async ValueTask INSERT_TBM_CUSTOMERAsync(tbm_customer data)
        {

            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                data.id_card = data.id_card.Replace("-", "");
                if (string.IsNullOrWhiteSpace(data.customer_id))
                {
                    await repository.INSERT_TBM_CUSTOMERAsync(data);
                }
                else
                {
                    await repository.UPDATE_TBM_CUSTOMERAsync(data);
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

        public async ValueTask INSERT_TBM_VEHICLEAsync(tbm_vehicle data)
        {
            DateTime? effective_date = null;
            DateTime? expire_date = null;
            CultureInfo culture = new CultureInfo("th-TH");

            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                data.service_price = data.service_price.Replace(",", "");
                if (data is not null)
                {
                    if (!string.IsNullOrWhiteSpace(data.effective_date))
                    {
                        effective_date = DateTime.ParseExact(data.effective_date, "dd/MM/yyyy", culture);
                    }
                    if (!string.IsNullOrWhiteSpace(data.expire_date))
                    {
                        expire_date = DateTime.ParseExact(data.expire_date, "dd/MM/yyyy", culture);
                    }
                }
                var oldlicense = await GET_TBM_VEHICLEAsync(new tbm_vehicle { license_no = data.license_no, customer_id = data.customer_id });
                if (oldlicense is null)
                {
                    var seq = await GET_TBM_VEHICLEAsync(new tbm_vehicle { customer_id = data.customer_id });
                    if (seq is null)
                    {
                        data.seq = "1";
                    }
                    else
                    {
                        data.seq = (seq.Count + 1).ToString();
                    }
                    await repository.INSERT_TBM_VEHICLEAsync(data, effective_date, expire_date);
                    await repository.sp_gen_remind_table(data.license_no);
                }
                else
                {
                    data.seq = oldlicense.Max(a => a.seq);
                    await repository.UPDATE_TBM_VEHICLEAsync(data, effective_date, expire_date);
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
        public async ValueTask INSERT_TBM_SERVICESAsync(tbm_services data)
        {

            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                List<tbm_services> olddata = new List<tbm_services>();
                if (data.services_no is null)
                {
                    olddata = null;
                }
                else
                {
                    olddata = await GET_TBM_SERVICESAsync(new tbm_services { services_no = data.services_no });
                }
                if (olddata is null)
                {
                    var service_no = await GET_TBM_SERVICES_BY_JOBTYPE(data.jobcode);
                    if (!string.IsNullOrWhiteSpace(service_no))
                    {
                        var number = service_no.Substring(2);
                        var runingnumber = Convert.ToInt32(number) + 1;
                        data.services_no = $"{data.jobcode}{runingnumber.ToString().PadLeft(2, '0')}";
                    }
                    else
                    {
                        data.services_no = $"{data.jobcode}01";
                    }
                    await repository.INSERT_TBM_SERVICESAsync(data);
                }
                else
                {
                    await repository.UPDATE_TBM_SERVICESAsync(data);

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
        public async ValueTask INSERT_TBT_JOB_HEADERAsync(create_job data)
        {

            Repository repository = new Repository(_connectionstring, DBENV);
            List<tbm_jobtype> tbm_Jobtypes = null;
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (string.IsNullOrWhiteSpace(data.job_id))
                {
                    tbm_Jobtypes = await GET_JOBTYPEAsync();
                    string running_type = tbm_Jobtypes.Where(a => a.jobcode == data.type_job).FirstOrDefault().running_type;
                    string running_no = await GET_SP_GET_RUNNING_NOAsync(running_type);
                    data.job_id = running_no;
                    await repository.INSERT_TBT_JOB_HEADERAsync(data);
                }
                else
                {
                    await repository.UPDATE_TBT_JOB_HEADERAsync(data);
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
        private async ValueTask CheckDuplicateStore(tbm_location_store data, string edit_flg)
        {
            var olddata = await GET_TBM_LOCATION_STOREAsync(new tbm_location_store { owner_id = data.owner_id });
            if (edit_flg == "Y")
            {
                if (olddata is null)
                    return;
                olddata = olddata.Where(a => a.location_id != data.location_id).ToList();
                if (olddata is not null && olddata.Count > 0)
                {
                    throw new Exception($"{olddata.FirstOrDefault().owner_name } ประจำที่ {olddata.FirstOrDefault().location_name}");
                }
            }
            else
            {
                if (olddata is not null)
                {
                    throw new Exception($"{olddata.FirstOrDefault().owner_name } ประจำที่ {olddata.FirstOrDefault().location_name}");
                }
            }

        }
        public async ValueTask INSERT_TBM_LOCATION_STOREAsync(tbm_location_store data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                var olddata = await GET_TBM_LOCATION_STOREAsync(new tbm_location_store { location_id = data.location_id });
                if (olddata is null)
                {
                    await CheckDuplicateStore(data, "N");
                    await repository.INSERT_TBM_LOCATION_STOREAsync(data);
                }
                else
                {
                    if (olddata.FirstOrDefault().owner_id is not null && data.owner_id is not null && olddata.FirstOrDefault().owner_id != data.owner_id)
                    {
                        throw new Exception("รถคันนี้มีช่างประจำอยู่แล้ว");
                    }
                    if (data.owner_id is not null)
                    {
                        await CheckDuplicateStore(data, "Y");
                    }

                    await repository.UPDATE_TBM_LOCATION_STOREAsync(data);
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

        public async ValueTask INSERT_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null && string.IsNullOrWhiteSpace(data.part_id) && data.location_id == "L01")
                {
                    await repository.INSERT_TBM_SPAREPARTAsync(data);//สร้างใหม่
                }
                else/* if (data.location_id != "L01" && !string.IsNullOrWhiteSpace(data.part_id))*/
                {

                    await repository.MOVECAR_TBM_SPAREPARTAsync(data);
                    //var spart = await GET_TBM_SPAREPARTAsync(new tbm_sparepart { part_id = data.part_id }, data.create_by);
                    //if (spart is not null)
                    //{
                    //    if (spart.FirstOrDefault().location_id != data.location_id)
                    //    {
                    //        await repository.INSERT_TBM_SPAREPARTAsync(data);//ย้ายไปคันอื่น
                    //        spart.FirstOrDefault().part_value = (Convert.ToUInt32(spart.FirstOrDefault().part_value) - Convert.ToInt32(data.part_value)).ToString();
                    //        await repository.UPDATE_TBM_SPAREPARTAsync(spart.FirstOrDefault());
                    //    }
                    //    else
                    //    {
                    //        await repository.UPDATE_TBM_SPAREPARTAsync(data);//แก้ไขคันเดิม
                    //    }
                    //}
                }
                //else
                //{
                //    if (data.status == "0")
                //    {
                //        data.cancel_by = data.create_by;
                //    }
                //    await repository.UPDATE_TBM_SPAREPARTAsync(data);
                //}
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

        public async ValueTask INSERT_TBT_ADJ_SPAREPARTAsync(tbt_adj_sparepart data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                //if (data is not null && string.IsNullOrEmpty(data.adj_id))
                //{
                await repository.INSERT_TBT_ADJ_SPAREPARTAsync(data);
                //}
                //else
                //{
                //    await repository.UPDATE_TBT_ADJ_SPAREPARTAsync(data);
                //}

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
        public async ValueTask INSERT_TBT_JOB_IMAGE(tbt_job_image data, string userid, string job_id)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                var seq = await GET_SEQ_IMAGEAsync(job_id);
                seq = seq + 1;
                data.seq = seq.ToString();
                await repository.INSERT_TBT_JOB_IMAGE(data, userid);

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
        public async ValueTask TERMINATE_TBM_CUSTOMERAsync(customer_terminate data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null)
                {
                    if (data.customer_id is not null)
                    {
                        foreach (var item in data.customer_id)
                        {
                            await repository.TERMINATE_TBM_CUSTOMERAsync(item);
                        }

                    }
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
        public async ValueTask TERMINATE_TBM_EMPLOYEEAsync(employee_terminate data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null)
                {
                    if (data.user_id is not null)
                    {
                        foreach (var item in data.user_id)
                        {
                            await repository.TERMINATE_TBM_EMPLOYEEAsync(item, data.terminate_by);

                        }
                    }
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
        public async ValueTask TERMINATE_TBM_SERVICESAsync(service_terminate data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null)
                {
                    if (data.services_no is not null)
                    {
                        foreach (var item in data.services_no)
                        {
                            await repository.TERMINATE_TBM_SERVICESAsync(item, data.user_id);

                        }
                    }
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
        public async ValueTask TERMINATE_TBM_LOCATION_STOREAsync(location_store_terminate data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null)
                {
                    if (data.location_id is not null)
                    {
                        foreach (var item in data.location_id)
                        {
                            await repository.TERMINATE_TBM_LOCATION_STOREAsync(item, data.user_id);
                        }
                    }
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

        public async ValueTask TERMINATE_TBM_SPAREPARTAsync(sparepart_terminate data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null)
                {
                    if (data.part_detail is not null)
                        foreach (var item in data.part_detail)
                        {
                            await repository.TERMINATE_TBM_SPAREPARTAsync(item.part_id, data.user_id, item.cancel_reason);
                        }
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
        public async ValueTask TERMINATE_TBT_ADJ_SPAREPARTAsync(tbt_adj_sparepart data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null)
                {
                    await repository.TERMINATE_TBT_ADJ_SPAREPARTAsync(Convert.ToInt32(data.adj_id), data.cancel_by, data.cancel_reason);
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

        public async ValueTask TERMINATE_TBT_JOB_IMAGE(string job_id, string seq)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                await repository.TERMINATE_TBT_JOB_IMAGE(job_id, seq);
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
        public async ValueTask Close_jobAsync(close_job data, List<tbt_job_image> image)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                var job = await GET_TBT_JOB(data.job_id);

                await repository.Close_jobAsync(data, job);
                if (data.job_checklists is not null && data.job_checklists.Count > 0)
                {
                    await repository.TERMINATE_TBT_JOB_CHECKLISTAsync(data.job_id);
                    foreach (var item in data.job_checklists)
                    {
                        await repository.INSERT_tbt_job_checklist(item, data.job_id);
                    }
                }

                if (data.job_detail is not null)
                {
                    var olddetail = await GET_TBT_JOB_DETAIL(new tbt_job_detail { bjob_id = data.job_id });
                    DateTime? CD_tag_date = null;
                    if (data.job_detail.CD_tag_date is not null)
                    {
                        if (!string.IsNullOrWhiteSpace(data.job_detail.CD_tag_date))
                        {
                            CD_tag_date = DateTime.ParseExact(data.job_detail.CD_tag_date, "dd/MM/yyyy", culture);
                        }
                    }
                    if (olddetail is null)
                    {
                        await repository.INSERT_TBT_JOB_DETAIL(data.job_detail, data.job_id, CD_tag_date);
                    }
                    else
                    {
                        await repository.UPDATE_TBT_JOB_DETAIL(data.job_detail, data.job_id, CD_tag_date);
                    }
                }

                if (image is not null && image.Count > 0)
                {
                    var seq = await GET_SEQ_IMAGEAsync(data.job_id);

                    foreach (var item in image)
                    {
                        seq = seq + 1;
                        item.seq = seq.ToString();
                        await repository.INSERT_TBT_JOB_IMAGE(item, data.userid);
                    }
                }
                await repository.TERMINATE_TBT_JOB_PART(data.job_id);
                if (data.job_parts is not null && data.job_parts.Count > 0)
                {
                    //var seq = await GET_TBT_JOB_PART_SEQ(data.job_id);
                    var seq = await  repository.GET_TBT_JOB_PART_SEQ(data.job_id); 
                    foreach (var item in data.job_parts)
                    {
                        seq = seq + 1;
                        item.seq = seq.ToString();
                        await repository.INSERT_TBT_JOB_PART(item, data.userid, data.job_id);
                    }
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

        #region " REPORT "
        public async ValueTask<tbt_job_image> GET_IMAGE_SIG(string condition)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            tbt_job_image dataObjects = new tbt_job_image();
            try
            {
                dataObjects = await repository.GET_IMAGE_SIG(condition);
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

        public async ValueTask<summary_job_list_condition> GET_Summary_job_list(summary_job_list_condition condition)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            List<DataContract.summary_job_list> dataObjects = new List<DataContract.summary_job_list>();
            try
            {
                DateTime? jobfrom = null;
                DateTime? jobto = null;
                DateTime? fixfrom = null;
                DateTime? fixto = null;
                DateTime? closefrom = null;
                DateTime? closeto = null;
                if (condition is not null)
                {
                    convertDate(ref jobfrom, ref jobto, condition.job_date_from, condition.job_date_to);
                    convertDate(ref fixfrom, ref fixto, condition.fix_date_from, condition.fix_date_to);
                    convertDate(ref closefrom, ref closeto, condition.close_dt_from, condition.close_dt_to);
                }
                dataObjects = await repository.GET_Summary_job_list(condition, jobfrom, jobto, fixfrom, fixto, closefrom, closeto);
                if (dataObjects is not null)
                {
                    condition.summary_job_list = new List<DataContract.summary_job_list>();
                    condition.summary_job_list = dataObjects;
                }
                var emp = await repository.GET_TBM_EMPLOYEEAsync(new tbm_employee { user_id = condition.user_print });
                condition.user_print = $"{emp.FirstOrDefault().fullname}   {emp.FirstOrDefault().lastname}";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
            return condition;
        }
        public async ValueTask<DataFile> GET_REPORT_Summary_job_list(summary_job_list_condition condition)
        {
            DataFile file = new DataFile();
            try
            {

                report.summary_job_list rpt = new report.summary_job_list(condition);
                if (condition.report_type.ToUpper() == "PDF")
                {
                    MemoryStream stream = new MemoryStream();
                    await rpt.ExportToPdfAsync(stream);
                    file.FileData = stream.ToArray();
                    file.FileName = "summaryJob.pdf";
                    file.ContentType = "application/pdf";

                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    await rpt.ExportToXlsAsync(stream);
                    file.FileData = stream.ToArray();
                    file.FileName = "summaryJob.xls";
                    file.ContentType = "application/vnd.ms-excel";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return file;
        }
        private void convertDate(ref DateTime? from, ref DateTime? to, string stfrom, string stto)
        {
            TimeSpan timestart = new TimeSpan(0, 0, 0);
            TimeSpan timelast = new TimeSpan(23, 59, 59);
            if (!string.IsNullOrEmpty(stfrom) && !string.IsNullOrEmpty(stto))
            {
                from = DateTime.ParseExact(stfrom, "dd/MM/yyyy", culture);
                to = DateTime.ParseExact(stto, "dd/MM/yyyy", culture);
                from.Value.Add(timestart);
                to.Value.Add(timestart);
            }
            else if (!string.IsNullOrEmpty(stfrom) && string.IsNullOrEmpty(stto))
            {
                from = DateTime.ParseExact(stfrom, "dd/MM/yyyy", culture);
                to = DateTime.ParseExact(stfrom, "dd/MM/yyyy", culture);
                from.Value.Add(timestart);
                to.Value.Add(timestart);
            }
            else if (string.IsNullOrEmpty(stfrom) && !string.IsNullOrEmpty(stto))
            {
                from = DateTime.ParseExact(stto, "dd/MM/yyyy", culture);
                to = DateTime.ParseExact(stto, "dd/MM/yyyy", culture);
                from.Value.Add(timestart);
                to.Value.Add(timestart);
            }
            else if (string.IsNullOrEmpty(stfrom) && string.IsNullOrEmpty(stto))
            {
                from = null;
                to = null;
            }
        }
        public async ValueTask<summary_stock_list_condition> GET_Summary_stock_list(summary_stock_list_condition condition)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            List<DataContract.summary_stock_list> dataObjects = new List<DataContract.summary_stock_list>();
            try
            {
                DateTime? partcrtfrom = null;
                DateTime? partcrtto = null;
                if (condition is not null)
                {
                    convertDate(ref partcrtfrom, ref partcrtto, condition.Part_create_from, condition.Part_create_to);
                }
                dataObjects = await repository.GET_Summary_stock_list(condition, partcrtfrom, partcrtto);
                if (dataObjects is not null)
                {
                    condition.summary_stock_list = new List<DataContract.summary_stock_list>();
                    condition.summary_stock_list = dataObjects;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
            return condition;
        }
        public async ValueTask<DataFile> GET_REPORT_Summary_stock_list(summary_stock_list_condition condition)
        {
            DataFile file = new DataFile();
            try
            {
                report.summary_stock_list rpt = new report.summary_stock_list(condition);
                if (condition.report_type.ToUpper() == "PDF")
                {
                    MemoryStream stream = new MemoryStream();
                    await rpt.ExportToPdfAsync(stream);
                    file.FileData = stream.ToArray();
                    file.FileName = "summaryStock.pdf";
                    file.ContentType = "application/pdf";

                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    await rpt.ExportToXlsxAsync(stream);
                    file.FileData = stream.ToArray();
                    file.FileName = "summaryStock.xls";
                    file.ContentType = "application/vnd.ms-excel";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return file;
        }

        public async ValueTask<DataFile> GET_REPORT_CLOSE_JOB(string job_id)
        {
            // Repository repository = new Repository(_connectionstring,DBENV);
            //  await repository.OpenConnectionAsync();
            List<check_list_rpt> chkpt = new List<check_list_rpt>();
            check_list_header_rpt chkheader = null;
            DataFile pdf = null;
            try
            {
                var checklist = await CHECK_LIST();
                var listdata = await GET_JOB_DETAIL(job_id);
                if (listdata is not null)
                {
                    chkheader = new check_list_header_rpt
                    {
                        Customer = listdata.customer_name,
                        Job_id = listdata.job_id
                    };
                }
                if (listdata is not null && listdata.job_checklists is not null)
                {
                    foreach (var item in listdata.job_checklists)
                    {
                        foreach (var _item in checklist)
                        {
                            _item.check_list.Where(a => a.ch_id == item.ck_id).Select(a => { a.status_check = "Y"; a.remark = item.description; return a; }).ToList();
                        }
                    }
                }
                foreach (var item in checklist)
                {
                    chkpt.Add(new check_list_rpt
                    {
                        DESCRIPTION = $"{item.check_group_name}",
                        Header = "Y"
                    });
                    foreach (var _item in item.check_list.Where(a => a.show_in_rpt == "1"))
                    {
                        chkpt.Add(new check_list_rpt
                        {
                            DESCRIPTION = $"{_item.check_name.PadLeft(2)}",
                            INSP = _item.status_check,
                            ACT = _item.status_check,
                            REMARK = _item.remark
                        });
                    }

                }

                var img = await GET_IMAGE_SIG(listdata.job_id);
                if (img is not null)
                {
                    if (System.IO.File.Exists(img.img_path))
                    {
                        var imageData = System.IO.File.ReadAllBytes(img.img_path);
                        listdata.rptsig = Convert.ToBase64String(imageData);
                    }
                }
                var rpt = await mainreport(listdata);
                chkheader.checklist = new List<check_list_rpt>();
                chkheader.checklist = chkpt;
                var chklist = await checklistreport(chkheader);
                var allpdf = await CombineMultiplePDFs(new List<byte[]> { rpt, chklist });
                pdf = new DataFile { FileData = allpdf, FileName = "job.pdf", ContentType = "application/pdf" };
                await sendemail(pdf, listdata.email_customer, listdata.job_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //await repository.CloseConnectionAsync();
            }
            return pdf;
        }
        public async ValueTask sendemail(DataFile pdf, string email,string JOB_ID)
        {
            var config =await GET_CONFIG();
            List<string> Cc = new List<string>();
            if(config !=null)
            {
                Cc = config.Where(a => a.config_key == "CC-Mail").Select(a=>a.config_value).FirstOrDefault().Split(',')?.ToList();
            }
            List<DataFile> Attachments = new List<DataFile>();
            Attachments.Add(pdf);
            MailRequest request = new MailRequest
            {
                Attachments = Attachments,
                Body = "<span>รายละเอียดการซ่อมบำรุง</span>",
                Subject = $"รายละเอียดการซ่อมบำรุง:{JOB_ID}",
                //ToEmail = "Dethman_light@hotmail.com"
                ToEmail = email,
                Cc =Cc

            };
            await IMailService.SendEmailAsync(request);

        }

        private async ValueTask<byte[]> CombineMultiplePDFs(List<byte[]> dataFiles)
        {
            // step 1: creation of a document-object

            Document document = new Document();
            byte[] pdfAll = null;
            //create newFileStream object which will be disposed at the end
            using (Stream newFileStream = new MemoryStream())
            {
                // step 2: we create a writer that listens to the document
                PdfCopy writer = new PdfCopy(document, newFileStream);
                if (writer == null)
                {
                    return dataFiles[0];
                }

                // step 3: we open the document
                document.Open();

                foreach (var fileName in dataFiles)
                {
                    // we create a reader for a certain document
                    PdfReader reader = new PdfReader(fileName);
                    reader.ConsolidateNamedDestinations();

                    // step 4: we add content
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        writer.AddPage(page);
                    }

                    PrAcroForm form = reader.AcroForm;
                    if (form != null)
                    {
                        writer.CopyAcroForm(reader);
                    }

                    reader.Close();
                }

                // step 5: we close the document and writer
                writer.Close();
                document.Close();
                pdfAll = newFileStream.ToByteArray();
            }
            return pdfAll;//disposes the newFileStream object
        }
        private async ValueTask<byte[]> mainreport(close_job listdata)
        {
            report_close_job rpt = new report_close_job(listdata);
            MemoryStream stream = new MemoryStream();
            rpt.ExportToPdf(stream);
            var mainreport = stream.ToArray();
            return mainreport;
        }
        private async ValueTask<byte[]> checklistreport(check_list_header_rpt header)
        {
            report_checklist rpt = new report_checklist(header);
            MemoryStream stream = new MemoryStream();
            rpt.ExportToPdf(stream);
            var mainreport = stream.ToArray();
            return mainreport;

        }
        public async ValueTask<List<ReportPPM>> sp_getReportPPM(string customerid,
            string date_from,
            string date_to)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            List<ReportPPM> dataObjects = new List<ReportPPM>();
            try
            {
                DateTime? date_fromdt = null;
                DateTime? date_todt = null;
                convertDate(ref date_fromdt, ref date_todt, date_from, date_to);
                dataObjects = await repository.sp_getReportPPM(customerid, date_fromdt, date_todt);
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

        public async ValueTask<summary_stock_list_condition> sp_get_movement_sparepart(summary_stock_list_condition condition)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                DateTime? partcrtfrom = null;
                DateTime? partcrtto = null;
                if (condition is not null)
                {
                    convertDate(ref partcrtfrom, ref partcrtto, condition.Part_create_from, condition.Part_create_to);
                }

                var dataObjects = await repository.sp_get_movement_sparepart(condition);
                if (dataObjects is not null)
                {
                    condition.summary_stock_list = new List<DataContract.summary_stock_list>();
                    condition.summary_stock_list = dataObjects;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                await repository.CloseConnectionAsync();
            }
            return condition;
        }
        #endregion " REPORT "

    }
}
