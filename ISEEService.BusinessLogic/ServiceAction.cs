﻿using CryptoHelper;
using ISEEService.DataAccess;
using ISEEService.DataContract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.BusinessLogic
{
    public class ServiceAction
    {
        private readonly string _connectionstring = string.Empty;
        //string contentRootPath = _hostingEnvironment.ContentRootPath + @"\ImageMaster";
        public ServiceAction(string connectionstring)
        {
            this._connectionstring = connectionstring;
        }
        public async ValueTask CheckConnectDB()
        {
            Repository repository = new Repository(_connectionstring);
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
        public async ValueTask<List<tbm_province>> GET_PROVINCE()
        {
            List<tbm_province> dataObjects = null;
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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

        public async ValueTask<List<tbt_job_header>> GET_JOBREFAsync(job_ref data)
        {
            List<tbt_job_header> dataObjects = null;
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            CultureInfo culture = new CultureInfo("th-TH");
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            try
            {
                if (data is not null)
                {
                    if (!string.IsNullOrWhiteSpace(data.expire_date))
                    {
                        sExpire_date = DateTime.ParseExact(data.expire_date, "mm/dd/yyyy", culture);
                        sExpire_date = sExpire_date.Value.Add(timestart);
                        eExpire_date = sExpire_date.Value.Add(timelast);
                    }
                    if (!string.IsNullOrWhiteSpace(data.effective_date))
                    {
                        sEffective_date = DateTime.ParseExact(data.effective_date, "mm/dd/yyyy", culture);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
        public async ValueTask<List<tbm_sparepart>> GET_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            List<tbm_sparepart> dataObjects = null;
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBM_SPAREPARTAsync(data);
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
            Repository repository = new Repository(_connectionstring);
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
        public async ValueTask<long> GET_SEQ_IMAGEAsync(string ijob_id)
        {
            long dataObjects = 0;
            Repository repository = new Repository(_connectionstring);
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

        #region " GET JOB DETAIL "
        public async ValueTask<close_job> GET_JOB_DETAIL(string job_id)
        {
            close_job dataObjects = null;
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBT_JOB_HEADERAsync(job_id);
                if (dataObjects is not null)
                {
                    dataObjects.job_detail = new tbt_job_detail();
                    dataObjects.job_detail = await repository.GET_TBT_JOB_DETAILAsync(job_id);
                    dataObjects.job_checklists = new List<tbt_job_checklist>();
                    dataObjects.job_checklists = await repository.GET_TBT_JOB_CHECKLISTAsync(job_id);
                    dataObjects.job_parts = new List<tbt_job_part>();
                    dataObjects.job_parts = await repository.GET_TBT_JOB_PARTAsync(job_id);
                    dataObjects.image_detail = new List<tbt_job_image>();
                    dataObjects.image_detail = await repository.GET_TBT_JOB_IMAGEAsync(job_id);
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

        public async ValueTask<List<job_detail_list>> GET_JOB_DETAIL_LISTAsync(string userid)
        {
            List<job_detail_list> dataObjects = null;
            bool isAdmin = true;
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_JOB_DETAIL_LISTAsync(userid, isAdmin);
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
            string dataObjects =string.Empty;
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.GET_TBT_JOB_PART_SEQ(job_id);
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
        public async ValueTask<List<tbt_job_part>> GET_TBT_JOB_PART(string pjob_id)
        {
            List<tbt_job_part> dataObjects = null;
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            try
            {

                dataObjects = await repository.GET_MENU(user_id);

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

        public async ValueTask INSERT_TBM_EMPLOYEEAsync(employee data)
        {

            Repository repository = new Repository(_connectionstring);
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

            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
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

            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null)
                {
                    if (!string.IsNullOrWhiteSpace(data.effective_date))
                    {
                        effective_date = DateTime.ParseExact(data.effective_date, "mm/dd/yyyy", culture);
                    }
                    if (!string.IsNullOrWhiteSpace(data.expire_date))
                    {
                        expire_date = DateTime.ParseExact(data.expire_date, "mm/dd/yyyy", culture);
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

            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                var olddata = await GET_TBM_SERVICESAsync(new tbm_services { services_no = data.services_no });
                if (olddata is null)
                {
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

            Repository repository = new Repository(_connectionstring);
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

        public async ValueTask INSERT_TBM_LOCATION_STOREAsync(tbm_location_store data)
        {
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                var olddata = await GET_TBM_LOCATION_STOREAsync(new tbm_location_store { location_id = data.location_id });
                if (olddata is null)
                {
                    await repository.INSERT_TBM_LOCATION_STOREAsync(data);
                }
                else
                {
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
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                if (data is not null && string.IsNullOrWhiteSpace(data.part_id))
                {
                    await repository.INSERT_TBM_SPAREPARTAsync(data);
                }
                else
                {
                    if(data.status == "0")
                    {
                        data.cancel_by = data.create_by;
                    }
                    await repository.UPDATE_TBM_SPAREPARTAsync(data);
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

        public async ValueTask TERMINATE_TBM_CUSTOMERAsync(customer_terminate data)
        {
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
            Repository repository = new Repository(_connectionstring);
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
        public async ValueTask Close_jobAsync(close_job data, List<tbt_job_image> image)
        {
            Repository repository = new Repository(_connectionstring);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {

                await repository.Close_jobAsync(data);
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
                    if (olddetail is null)
                    {
                        await repository.INSERT_TBT_JOB_DETAIL(data.job_detail, data.job_id);
                    }
                    else
                    {
                        await repository.UPDATE_TBT_JOB_DETAIL(data.job_detail, data.job_id);
                    }
                }

                if (image is not null && image.Count > 0)
                {
                    foreach (var item in image)
                    {
                        var seq = await GET_SEQ_IMAGEAsync(item.ijob_id);
                        item.seq = (seq + 1).ToString();
                        await repository.INSERT_TBT_JOB_IMAGE(item, data.userid);
                    }
                }

                if (data.job_parts is not null && data.job_parts.Count > 0)
                {
                    await repository.TERMINATE_TBT_JOB_PART(data.job_id);
                    foreach (var item in data.job_parts)
                    {
                        var seq = await GET_TBT_JOB_PART_SEQ(data.job_id);
                        item.seq = (seq + 1).ToString();
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

        /*
        
        
        3.ลงตารางเพิ่ม3ตารางตอนปิดjob
        -tbt_job_checklist ok
        -tbt_job_detail ok
        -tbt_job_image รับไฟล์ ok
        -tbt_job_part 
        4.ให้ลงข้อมูลLogin checklogin return menu
        5.select ตารางและเช็คคอนดิชั่นทุกฟิล
            -emp + where condition
            -cus + where condition
            -vil + where condition
        6.ดึงข้อมูลมาสเตอร์ตาราง image_type ok
        ถ้าเปิดหลายรอบมีปัญหาแน่ไม่ได้ดักไว้เนชรื่องการปิดงาน
         */
    }
}