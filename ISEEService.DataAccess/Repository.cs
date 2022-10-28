using ISEEService.DataContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITUtility;
using DataAccessUtility;

namespace ISEEService.DataAccess
{
    public class Repository
    {
        #region " STATIC "

        private readonly SqlConnection sqlConnection = null;
        private SqlTransaction transaction;

        private readonly string DBENV = string.Empty;
        public Repository(string connectionstring, string DBENV) : this(new SqlConnection(connectionstring), DBENV)
        {

        }
        public Repository(SqlConnection ConnectionString, string DBENV)
        {
            this.sqlConnection = ConnectionString;
            this.DBENV = DBENV;
        }
        public async ValueTask OpenConnectionAsync() =>
       await this.sqlConnection.OpenAsync();
        public async ValueTask CloseConnectionAsync() =>
       await this.sqlConnection.CloseAsync();

        public async ValueTask beginTransection() =>
            this.transaction = this.sqlConnection.BeginTransaction();

        public async ValueTask CommitTransection() =>
            this.transaction.Commit();


        public async ValueTask RollbackTransection() =>
            this.transaction.Rollback();
        #endregion " STATIC "

        #region " GET DATA "
        public async ValueTask<long?> CHECK_STOCK(string part_id, string location_id)
        {
            long? dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"select  [{DBENV}].dbo.fn_showOnhand(sp.part_id) as 'onhand' 
 from [{DBENV}].[dbo].[tbm_sparepart] sp
 WHERE sp.part_id = @part_id
 AND sp.location_id=@location_id "
            };
            sql.Parameters.AddWithValue("@part_id", part_id);
            sql.Parameters.AddWithValue("@location_id", location_id);

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                else
                {
                    dataObjects = 0;
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<tbm_province>> GET_PROVINCEAsync()
        {
            List<tbm_province> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                                FROM [{DBENV}].[dbo].[tbm_province]
                            WHERE status =1 "
            };
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_province>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbm_district>> GET_DISTRICTAsync(string province_code)
        {
            List<tbm_district> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                            FROM [{DBENV}].[dbo].[tbm_district]
                            WHERE province_code = @inprovince_code 
                            AND status =1 "
            };
            sql.Parameters.AddWithValue("@inprovince_code", province_code);

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_district>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbm_sub_district>> GET_SUB_DISTRICTAsync(string district_code)
        {
            List<tbm_sub_district> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                                FROM [{DBENV}].[dbo].[tbm_sub_district]
                                where district_code =@district_code
                                AND status =1 "
            };
            sql.Parameters.AddWithValue("@district_code", district_code);
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_sub_district>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbm_employee_position>> GET_EMPLOYEE_POSITIONAsync()
        {
            List<tbm_employee_position> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                                FROM [{DBENV}].[dbo].[tbm_employee_position]
                                WHERE STATUS =1 "
            };
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_employee_position>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbm_jobtype>> GET_JOBTYPEAsync()
        {
            List<tbm_jobtype> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                            FROM [{DBENV}].[dbo].[tbm_jobtype]
                            where status=1 "
            };
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_jobtype>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<tbm_brand>> GET_BRANDAsync()
        {
            List<tbm_brand> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT * FROM [{DBENV}].[dbo].[tbm_brand] WHERE ACTIVE_FLAG =1"
            };
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_brand>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbm_contract_type>> GET_CONTRACTTYPEAsync()
        {
            List<tbm_contract_type> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT * FROM [{DBENV}].[dbo].[tbm_contract_type]"
            };
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_contract_type>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbm_employee>> GET_EMPLOYEEAsync()
        {
            List<tbm_employee> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT user_id,user_name,fullname,lastname,position FROM [{DBENV}].[dbo].[tbm_employee] WHERE STATUS =1"
            };
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_employee>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<Customer>> GET_CUSTOMERAsync(string license_no)
        {
            List<Customer> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"select v.license_no
        ,cu.customer_id
      ,id_card
      ,cust_type
	  , 
	  CASE WHEN cust_type ='01' THEN 'บุคคลธรรมดา' 
	  WHEN cust_type ='02' THEN 'นิติบุคคล'
	  END AS
	   cust_type_description 
      ,fname
      ,lname
      ,cu.address
      ,cu.sub_district_no
	  ,sd.sub_district_name_tha
      ,cu.district_code
	  ,ds.district_name_tha
      ,cu.province_code
	  ,pr.province_name_tha
      ,cu.zip_code
      ,phone_no
      ,Email from [{DBENV}].[dbo].[tbm_vehicle] v
                            INNER JOIN [{DBENV}].[dbo].[tbm_customer] cu ON cu.customer_id =v.customer_id
  INNER JOIN [{DBENV}].[dbo].[tbm_province] pr on pr.province_code =cu.province_code
  INNER JOIN [{DBENV}].[dbo].[tbm_district] ds on ds.district_code =cu.district_code
  INNER JOIN [{DBENV}].[dbo].[tbm_sub_district] sd on sd.sub_district_code =cu.sub_district_no
                            WHERE v.license_no like @license_no "
            };
            command.Parameters.AddWithValue("@license_no", $"%{license_no}%");
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<Customer>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<tbt_job_header>> GET_JOBREFAsync(job_ref data)
        {
            List<tbt_job_header> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                             FROM [{DBENV}].[dbo].[tbt_job_header]
                             WHERE status =1 
                             AND license_no =@license_no
                             AND customer_id =@customer_id "
            };
            command.Parameters.AddWithValue("@license_no", data.license_no);
            command.Parameters.AddWithValue("@customer_id", data.customer_id);
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_header>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<string> GET_SP_GET_RUNNING_NOAsync(string running_type)
        {
            string running_no = string.Empty;
            SqlCommand cmd = new SqlCommand($"[{DBENV}].[dbo].[sp_get_running_no]", this.sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@running_type", running_type);
            cmd.Parameters.Add("@running_no", SqlDbType.VarChar, 30);
            cmd.Parameters["@running_no"].Direction = ParameterDirection.Output;
            await cmd.ExecuteNonQueryAsync();
            running_no = (string)cmd.Parameters["@running_no"].Value;
            return running_no;
        }

        public async ValueTask<List<tbm_checklist_group>> GET_TBM_CHECKLIST_GROUP()
        {
            List<tbm_checklist_group> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                             FROM [{DBENV}].[dbo].[tbm_checklist_group]
                             where status =1"
            };

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_checklist_group>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbm_checklist>> GET_TBM_CHECKLIST(string ch_group_id)
        {
            List<tbm_checklist> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT
        [ch_id]
      ,[check_name_E]
      ,[show_in_rpt]
      ,[check_name_T] AS check_name
      ,[status]
      ,[show_in_rpt]
      ,[check_group_id]
      ,[create_date]
      ,[create_by]
      ,[update_date]
      ,[update_by]
  FROM [{DBENV}].[dbo].[tbm_checklist]
  WHERE check_group_id =@check_group_id
  AND status =1  "
            };
            command.Parameters.AddWithValue("@check_group_id", ch_group_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_checklist>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbt_job_detail>> GET_TBT_JOB_DETAIL(tbt_job_detail tbt_job_detail)
        {
            List<tbt_job_detail> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
  FROM [{DBENV}].[dbo].[tbt_job_detail]
  where bjob_id =@job_id"
            };
            command.Parameters.AddWithValue("@job_id", tbt_job_detail.bjob_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_detail>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<tbt_job_header> GET_TBT_JOB(string job_id)
        {
            tbt_job_header dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                                FROM [{DBENV}].[dbo].[tbt_job_header]
                                where job_id =@job_id
                                AND STATUS =1 "
            };
            command.Parameters.AddWithValue("@job_id", job_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_header>().FirstOrDefault();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbt_adj_sparepart>> GET_TBT_ADJ_SPAREPART_DETAIL(string part_id)
        {
            List<tbt_adj_sparepart> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                /*              CommandText = $@"SELECT  sp.[part_id],
                     adj.adj_id
                    ,sp.[part_no]
                    ,[part_name]
                    ,[part_desc]
                 ,sp.part_value
                 ,[adj_part_value]
                FROM [{DBENV}].[dbo].[tbm_sparepart] sp
                INNER join [{DBENV}].[dbo].[tbt_adj_sparepart] adj
                ON adj.part_id =sp.part_id
                WHERE sp.[part_id] =@part_id "
                          };*/
                CommandText = $"[{DBENV}].[dbo].[sp_get_tbt_adj_sparepart_detail]"
            };
            command.Parameters.AddWithValue("@part_id", part_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_adj_sparepart>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbt_adj_sparepart>> GET_TBT_ADJ_SPAREPART(string adj_type)
        {
            List<tbt_adj_sparepart> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].SP_GET_TBT_ADJ_SPAREPART"
            };
            command.Parameters.AddWithValue("@P_adj_type", adj_type);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_adj_sparepart>().ToList();
                }
            }
            return dataObjects;
        }

        #region " GET JOB DETAIL "
        public async ValueTask<close_job> GET_TBT_JOB_HEADERAsync(string job_id)
        {
            close_job dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT  [job_id],type_job,hd.job_status
      ,[license_no]
      ,hd.customer_id
	  ,concat(tc.fname,' ',tc.lname) customer_name
      ,[summary]
      ,[action]
      ,[result]
      ,[transfer_to]
      ,[fix_date]
      ,[close_date]
      ,[email_customer]
      ,[invoice_no]
      ,[owner_id]
	  ,CONCAT(te.fullname,' ',te.lastname) owner_name
      ,hd.create_by
      ,hd.create_date
      ,hd.update_by
      ,hd.update_date
      ,[ref_hjob_id]
      ,hd.status
 FROM [{DBENV}].[dbo].[tbt_job_header] hd
 INNER JOIN [{DBENV}].[dbo].[tbm_customer] tc on hd.customer_id =tc.customer_id 
 INNER JOIN [{DBENV}].[dbo].[tbm_employee] te on hd.owner_id =te.user_id
 where job_id =@job_id 
 AND hd.STATUS =1"
            };
            command.Parameters.AddWithValue("@job_id", job_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<close_job>().FirstOrDefault();
                }
            }
            return dataObjects;
        }
        public async ValueTask<tbt_job_detail> GET_TBT_JOB_DETAILAsync(string job_id)
        {
            tbt_job_detail dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT [bjob_id]
      ,[B1_model]
      ,[B1_serial]
      ,[B1_amp_hrs]
      ,[B1_date_code]
      ,[B1_spec_gravity]
      ,[B1_volt_static]
      ,[B1_volt_load]
      ,[B2_model]
      ,[B2_serial]
      ,[B2_amp_hrs]
      ,[B2_date_code]
      ,[B2_spec_gravity]
      ,[B2_volt_static]
      ,[B2_volt_load]
      ,[CD_manufact]
      ,[CD_model]
      ,[CD_serial]
      ,Case
	  WHEN CD_tag_date is null THEN
	  ''
	  ELSE
	  concat( convert(varchar(2), FORMAT(CD_tag_date,'dd')),'/',convert(varchar(2), FORMAT(CD_tag_date,'MM') ),'/',convert(varchar, year(CD_tag_date) +543)) 
		END AS CD_tag_date
      ,[H_meter]
      ,[V_service_mane]
      ,[V_labour]
      ,[V_travel]
      ,[V_total]
      ,[failure_code]
      ,[fair_wear]
                                FROM [{DBENV}].[dbo].[tbt_job_detail]
                                where bjob_id =@job_id"
            };
            command.Parameters.AddWithValue("@job_id", job_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_detail>().FirstOrDefault();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbt_job_checklist>> GET_TBT_JOB_CHECKLISTAsync(string job_id)
        {
            List<tbt_job_checklist> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT [ckjob_id]
                                    ,[ck_id]
                                    ,[description]
	                                ,'Y' AS check_list
                                FROM [{DBENV}].[dbo].[tbt_job_checklist]
                                where ckjob_id =@job_id"
            };
            command.Parameters.AddWithValue("@job_id", job_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_checklist>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbt_job_part>> GET_TBT_JOB_PARTAsync(string job_id)
        {
            List<tbt_job_part> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"
SELECT  [pjob_id]
      ,[seq]
      ,spar.[part_no]
      ,jpar.part_id
	  ,[part_name]
      ,[part_desc]
      ,[part_type]
	  ,case
	  WHEN part_type ='00' then
	  'Normal Part'
	  ELSE 'Dummy Part (ช่างกำหนด)'
	  END AS part_type_desc
      ,[total]
      ,jpar.[create_date]
      ,jpar.[create_by]
      ,[status]
  FROM [{DBENV}].[dbo].[tbt_job_part] jpar
  INNER JOIN [{DBENV}].[dbo].[tbm_sparepart] spar on jpar.part_id =spar.part_id
  WHERE jpar.status =1
  AND jpar.pjob_id =@job_id"
            };
            command.Parameters.AddWithValue("@job_id", job_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_part>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<List<tbt_job_image>> GET_TBT_JOB_IMAGEAsync(string job_id)
        {
            List<tbt_job_image> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"
SELECT [ijob_id]
      ,[seq]
      ,[img_name]
	  ,imt.image_description
      ,[img_path]
      ,[create_date]
      ,[create_by]
      ,im.[status]
      ,[image_type]
  FROM [{DBENV}].[dbo].[tbt_job_image] im
  INNER JOIN  [{DBENV}].[dbo].tbm_image_type imt ON im.image_type =imt.image_code
  WHERE imt.status= 1
  AND im.status= 1
  and im.ijob_id =@job_id
"
            };
            command.Parameters.AddWithValue("@job_id", job_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_image>().ToList();
                }
            }
            return dataObjects;
        }
        /*  public async ValueTask<List<job_detail_list>> GET_JOB_DETAIL_LISTAsync(string userid, bool isAdmin)
          {
              List<job_detail_list> dataObjects = null;
              SqlCommand command = new SqlCommand
              {
                  CommandType = System.Data.CommandType.Text,
                  Connection = this.sqlConnection,
                  CommandText = $@"SELECT [job_id]
                                  ,type_job
                                  ,jh.[license_no]
                                  ,jh.[customer_id]
                                  ,CONCAT(cus.fname,' ',cus.lname)AS cus_fullname
                                  ,[summary]
                                  ,[action]
                                  ,[result]
                                  ,[transfer_to]
                                  ,[fix_date]
                                  ,[close_date]
                                  ,[email_customer]
                                  ,[invoice_no]
                                  ,[owner_id]
                                  ,CONCAT(emp.fullname,' ',emp.lastname) as emp_fullname
                                  ,jh.[create_by]
                                  ,jh.[create_date]
                                  ,jh.[update_by]
                                  ,jh.[update_date]
                                  ,[ref_hjob_id]
                  FROM [{DBENV}].[dbo].[tbt_job_header] jh
                  INNER JOIN [{DBENV}].[dbo].[tbm_customer] cus on jh.customer_id =cus.customer_id
                  INNER JOIN [{DBENV}].[dbo].[tbm_employee] emp on emp.user_id =jh.owner_id
                  where jh.status =1 "
              };
              if (!isAdmin)
              {
                  command.CommandText += $@" AND owner_id =@owner_id";
                  command.Parameters.AddWithValue("@owner_id", userid);
              }
              using (DataTable dt = await Utility.FillDataTableAsync(command))
              {
                  if (dt.Rows.Count > 0)
                  {
                      dataObjects = dt.AsEnumerable<job_detail_list>().ToList();
                  }
              }
              return dataObjects;
          }*/
        public async ValueTask<bool> CheckPermissionAdmin(string userid)
        {
            SqlCommand command = new SqlCommand($"[{DBENV}].[dbo].[CheckPermissionAdmin]", this.sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", userid);
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }


        #endregion " GET JOB DETAIL "

        public async ValueTask<List<tbt_job_part>> GET_TBT_JOB_PART(string pjob_id)
        {
            List<tbt_job_part> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                                FROM [{DBENV}].[dbo].[tbt_job_part]
                                where pjob_id =@job_id AND STATUS =1"
            };
            command.Parameters.AddWithValue("@job_id", pjob_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_part>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<long> GET_TBT_JOB_PART_SEQ(string pjob_id)
        {
            long seq = 0;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"SELECT ISNULL(MAX(SEQ),0)
                                FROM [{DBENV}].[dbo].[tbt_job_part]
                                where pjob_id =@job_id AND STATUS =1"
            };
            command.Parameters.AddWithValue("@job_id", pjob_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    seq = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            return seq;
        }

        public async ValueTask<List<tbm_employee>> GET_TBM_EMPLOYEEAsync(tbm_employee data)
        {
            List<tbm_employee> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT [user_id]
      ,[user_name]
      ,[password]
      ,[fullname]
      ,[lastname]
      ,[idcard]
      ,[position]
	  ,emp.position_description
      ,em.[status]
      ,em.[create_date]
      ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = em.create_by) as [create_by]
      ,em.[update_date]
      ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = em.update_by) as [update_by]
      ,showstock
	  ,loc.location_name
  FROM [{DBENV}].[dbo].[tbm_employee] em
  INNER JOIN [{DBENV}].[dbo].[tbm_employee_position] emp on em.position =emp.position_code
  LEFT JOIN [{DBENV}].[dbo].[tbm_location_store] loc on loc.owner_id =em.user_id AND loc.status =1
  WHERE emp.status =1
  AND em.status =1
  "
            };
            if (data is not null && !string.IsNullOrWhiteSpace(data.user_id))
            {
                command.CommandText += " AND em.user_id =@user_id";
                command.Parameters.AddWithValue("@user_id", data.user_id);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.user_name))
            {
                command.CommandText += " AND em.user_name =@user_name";
                command.Parameters.AddWithValue("@user_name", data.user_name);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.fullname))
            {
                command.CommandText += " AND fullname like @fullname";
                command.Parameters.AddWithValue("@fullname", $"{data.fullname}%");
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.lastname))
            {
                command.CommandText += " AND lastname like @lastname";
                command.Parameters.AddWithValue("@lastname", $"{data.lastname}%");
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.idcard))
            {
                command.CommandText += " AND idcard =@idcard";
                command.Parameters.AddWithValue("@idcard", data.idcard);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.position))
            {
                command.CommandText += " AND em.position =@position";
                command.Parameters.AddWithValue("@position", data.position);
            }
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_employee>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<tbm_customer>> GET_TBM_CUSTOMERAsync(tbm_customer data)
        {
            List<tbm_customer> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT customer_id
      ,id_card
      ,cust_type
	  , 
	  CASE WHEN cust_type ='01' THEN 'บุคคลธรรมดา' 
	  WHEN cust_type ='02' THEN 'นิติบุคคล'
	  END AS
	   cust_type_description 
      ,fname
      ,lname
      ,cu.address
      ,cu.sub_district_no
	  ,sd.sub_district_name_tha
      ,cu.district_code
	  ,ds.district_name_tha
      ,cu.province_code
	  ,pr.province_name_tha
      ,cu.zip_code
      ,phone_no
      ,Email
  FROM [{DBENV}].[dbo].[tbm_customer] cu
  INNER JOIN [{DBENV}].[dbo].[tbm_province] pr on pr.province_code =cu.province_code
  INNER JOIN [{DBENV}].[dbo].[tbm_district] ds on ds.district_code =cu.district_code
  INNER JOIN [{DBENV}].[dbo].[tbm_sub_district] sd on sd.sub_district_code =cu.sub_district_no
WHERE cu.status =1 "
            };
            if (data is not null && !string.IsNullOrWhiteSpace(data.customer_id))
            {
                command.CommandText += " AND cu.customer_id =@customer_id";
                command.Parameters.AddWithValue("@customer_id", data.customer_id);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.id_card))
            {
                command.CommandText += " AND id_card =@id_card";
                command.Parameters.AddWithValue("@id_card", data.id_card);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.cust_type))
            {
                command.CommandText += " AND cust_type =@cust_type";
                command.Parameters.AddWithValue("@cust_type", data.cust_type);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.fname))
            {
                command.CommandText += " AND fname like @fname";
                command.Parameters.AddWithValue("@fname", $"{data.fname}%");
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.lname))
            {
                command.CommandText += " AND lname like @lname";
                command.Parameters.AddWithValue("@lname", $"{data.lname}%");
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.sub_district_no))
            {
                command.CommandText += " AND cu.sub_district_no =@sub_district_no";
                command.Parameters.AddWithValue("@sub_district_no", data.sub_district_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.district_code))
            {
                command.CommandText += " AND cu.district_code =@district_code";
                command.Parameters.AddWithValue("@district_code", data.district_code);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.province_code))
            {
                command.CommandText += " AND cu.province_code =@province_code";
                command.Parameters.AddWithValue("@province_code", data.province_code);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.zip_code))
            {
                command.CommandText += " AND cu.zip_code =@zip_code";
                command.Parameters.AddWithValue("@zip_code", data.zip_code);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.phone_no))
            {
                command.CommandText += " AND cu.phone_no =@phone_no";
                command.Parameters.AddWithValue("@phone_no", data.phone_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.Email))
            {
                command.CommandText += " AND Email =@Email";
                command.Parameters.AddWithValue("@Email", data.Email);
            }

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_customer>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<tbm_vehicle>> GET_TBM_VEHICLEAsync(tbm_vehicle data, DateTime? sExpire_date, DateTime? eExpire_date, DateTime? sEffective_date, DateTime? eEffective_date)
        {
            List<tbm_vehicle> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT  license_no
      ,seq
      ,brand_no
      ,br.brand_name_tha
      ,model_no
      ,chassis_no
      ,Color
	  ,concat( convert(varchar(2), FORMAT(effective_date,'dd')),'/',convert(varchar(2), FORMAT(effective_date,'MM') ),'/',convert(varchar, year(effective_date) +543)) effective_date
      ,concat( convert(varchar(2), FORMAT(expire_date,'dd')),'/',convert(varchar(2), FORMAT(expire_date,'MM') ),'/',convert(varchar, year(expire_date) +543)) expire_date
      ,service_price
      ,v.service_no
	  ,s.services_name
      ,contract_no
      ,cus.customer_id
	  ,CONCAT(cus.fname,' ',cus.lname) customer_name
  FROM [{DBENV}].[dbo].[tbm_vehicle] v
  INNER JOIN [{DBENV}].[dbo].[tbm_services] s on s.services_no =v.service_no
  INNER JOIN [{DBENV}].[dbo].[tbm_customer] cus on cus.customer_id =v.customer_id
  LEFT JOIN [{DBENV}].[dbo].[tbm_brand] br on br.brand_code =v.brand_no 
  WHERE s.status =1 "
            };
            if (data is not null && !string.IsNullOrWhiteSpace(data.license_no))
            {
                command.CommandText += " AND license_no =@license_no";
                command.Parameters.AddWithValue("@license_no", data.license_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.seq))
            {
                command.CommandText += " AND seq =@seq";
                command.Parameters.AddWithValue("@seq", data.seq);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.brand_no))
            {
                command.CommandText += " AND brand_no =@brand_no";
                command.Parameters.AddWithValue("@brand_no", data.brand_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.model_no))
            {
                command.CommandText += " AND model_no =@model_no";
                command.Parameters.AddWithValue("@model_no", data.model_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.chassis_no))
            {
                command.CommandText += " AND chassis_no =@chassis_no";
                command.Parameters.AddWithValue("@chassis_no", data.chassis_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.chassis_no))
            {
                command.CommandText += " AND Color =@Color";
                command.Parameters.AddWithValue("@Color", data.Color);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.effective_date))
            {
                command.CommandText += " AND effective_date between @seffective_date and @eeffective_date";
                command.Parameters.AddWithValue("@seffective_date", sEffective_date);
                command.Parameters.AddWithValue("@eeffective_date", eEffective_date);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.expire_date))
            {
                command.CommandText += " AND expire_date between @sexpire_date and @eexpire_date";
                command.Parameters.AddWithValue("@sexpire_date", sExpire_date);
                command.Parameters.AddWithValue("@eexpire_date", eExpire_date);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.service_price))
            {
                command.CommandText += " AND service_price =@service_price";
                command.Parameters.AddWithValue("@service_price", data.service_price);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.service_no))
            {
                command.CommandText += " AND v.service_no =@service_no";
                command.Parameters.AddWithValue("@service_no", data.service_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.contract_no))
            {
                command.CommandText += " AND contract_no =@contract_no";
                command.Parameters.AddWithValue("@contract_no", data.contract_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.customer_id))
            {
                command.CommandText += " AND v.customer_id =@customer_id";
                command.Parameters.AddWithValue("@customer_id", data.customer_id);
            }
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_vehicle>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<tbm_services>> GET_TBM_SERVICESAsync(tbm_services data)
        {
            List<tbm_services> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                        FROM [{DBENV}].[dbo].[tbm_services]
                         WHERE status= 1 "
            };
            if (data is not null && !string.IsNullOrWhiteSpace(data.services_no))
            {
                command.CommandText += " AND services_no =@services_no";
                command.Parameters.AddWithValue("@services_no", data.services_no);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.services_name))
            {
                command.CommandText += " AND services_name =@services_name";
                command.Parameters.AddWithValue("@services_name", data.services_name);
            }
            if (data is not null && !string.IsNullOrWhiteSpace(data.period_year))
            {
                command.CommandText += " AND period_year =@period_year";
                command.Parameters.AddWithValue("@period_year", data.period_year);
            }

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_services>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask<string> GET_TBM_SERVICES_BY_JOBTYPE(string data)
        {
            string service_no = string.Empty;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT MAX(services_no)
                        FROM [{DBENV}].[dbo].[tbm_services]
                         WHERE status= 1 "
            };
            if (!string.IsNullOrWhiteSpace(data))
            {
                command.CommandText += " AND services_no like @services_no";
                command.Parameters.AddWithValue("@services_no", $"{data}%");
            }
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    service_no = dt.Rows[0][0].ToString();
                }
            }
            return service_no;
        }

        public async ValueTask<tbm_employee> GET_EM_PERMISSIONAsync(string userid)
        {
            tbm_employee dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"select * from [{DBENV}].[dbo].tbm_employee
                                em 
                                INNER JOIN [{DBENV}].[dbo].tbm_employee_position ps on ps.position_code =em.position
                                LEFT JOIN [{DBENV}].[dbo].tbm_location_store loc on loc.owner_id =em.user_id
                                where em.user_id =@userid
                                AND em.status =1"
            };

            if (!string.IsNullOrWhiteSpace(userid))
            {
                command.Parameters.AddWithValue("@userid", userid);
            }

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_employee>().FirstOrDefault();
                }
            }
            return dataObjects;
        }
        /*  public async ValueTask<List<tbm_sparepart>> GET_TBM_SPAREPARTAsync(tbm_sparepart data)
            {
                List<tbm_sparepart> dataObjects = null;
                SqlCommand command = new SqlCommand
                {
                    CommandType = System.Data.CommandType.Text,
                    Connection = this.sqlConnection,
                    CommandText = $@"
    SELECT 
        part_id
          ,part_no
          ,part_name
          ,part_desc
          ,part_type
          ,case
          WHEN part_type ='00' then
          'Normal Part'
          ELSE 'Dummy Part (ช่างกำหนด)'
          END AS part_type_desc
          ,cost_price
          ,sale_price
          ,part.unit_code
          ,un.unit_name
          ,[{DBENV}].dbo.fn_showOnHand(part_id,@jobid) AS  part_value
          ,minimum_value
          ,maximum_value
          ,part.location_id
          ,loca.location_name
          ,part.create_date
          ,part.create_by
          ,part.cancal_date
          ,part.cancel_by
          ,part.cancel_reason
          ,part.update_date
          ,part.update_by
      FROM [{DBENV}].[dbo].[tbm_sparepart] part 
      INNER JOIN [{DBENV}].[dbo].[tbm_unit] un ON un.unit_code =part.unit_code
      INNER JOIN [{DBENV}].[dbo].[tbm_location_store] loca on loca.location_id =part.location_id
      WHERE 1=1 "
                };

                command.Parameters.AddWithValue("@jobid", data.jobid ?? (object)DBNull.Value);

                if (data is not null && !string.IsNullOrWhiteSpace(data.part_id))
                {
                    command.CommandText += " AND part_id =@part_id";
                    command.Parameters.AddWithValue("@part_id", data.part_id);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.part_no))
                {
                    command.CommandText += " AND part_no =@part_no";
                    command.Parameters.AddWithValue("@part_no", data.part_no);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.part_desc))
                {
                    command.CommandText += " AND part_desc =@part_desc";
                    command.Parameters.AddWithValue("@part_desc", data.part_desc);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.part_type))
                {
                    command.CommandText += " AND part_type =@part_type";
                    command.Parameters.AddWithValue("@part_type", data.part_type);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.cost_price))
                {
                    command.CommandText += " AND cost_price =@cost_price";
                    command.Parameters.AddWithValue("@cost_price", data.cost_price);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.sale_price))
                {
                    command.CommandText += " AND sale_price =@sale_price";
                    command.Parameters.AddWithValue("@sale_price", data.sale_price);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.unit_code))
                {
                    command.CommandText += " AND part.unit_code =@unit_code";
                    command.Parameters.AddWithValue("@unit_code", data.unit_code);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.part_value))
                {
                    command.CommandText += " AND part_value =@part_value";
                    command.Parameters.AddWithValue("@part_value", data.part_value);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.minimum_value))
                {
                    command.CommandText += " AND minimum_value =@minimum_value";
                    command.Parameters.AddWithValue("@minimum_value", data.minimum_value);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.maximum_value))
                {
                    command.CommandText += " AND minimum_value =@maximum_value";
                    command.Parameters.AddWithValue("@maximum_value", data.maximum_value);
                }
                if (data is not null && !string.IsNullOrWhiteSpace(data.location_id))
                {
                    command.CommandText += " AND part.location_id =@location_id";
                    command.Parameters.AddWithValue("@location_id", data.location_id);
                }

                using (DataTable dt = await Utility.FillDataTableAsync(command))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dataObjects = dt.AsEnumerable<tbm_sparepart>().ToList();
                    }
                }
                return dataObjects;
            }*/

        public async ValueTask<long?> GET_IMAGE_ID()
        {
            long? image_id = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT NEXT VALUE FOR image_id"
            };

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    image_id = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            return image_id;
        }
        public async ValueTask<tbt_job_image> CHECK_RESEND_EMAIL(string Jobid)
        {
            tbt_job_image image = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
  FROM [{DBENV}].[dbo].[tbt_job_image]
  where status =1
  AND ijob_id =@jobid
  AND image_type ='rpt'"
            };
            command.Parameters.AddWithValue("@jobid", Jobid);
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    image = dt.AsEnumerable<tbt_job_image>().FirstOrDefault();
                }
            }
            return image;
        }
        public async ValueTask<tbt_job_image> GET_PATHFILE(string ijob_id, string seq)
        {
            tbt_job_image filedetai = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT [ijob_id]
      ,[seq]
      ,[img_name]
      ,[img_path]
      ,[create_date]
      ,[create_by]
      ,[status]
      ,[image_type]
  FROM [{DBENV}].[dbo].[tbt_job_image]
WHERE [status] =1 AND ijob_id = @ijob_id AND seq =@seq"
            };
            command.Parameters.AddWithValue("@ijob_id", ijob_id);
            command.Parameters.AddWithValue("@seq", seq);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    filedetai = dt.AsEnumerable<tbt_job_image>().FirstOrDefault();
                }
            }
            return filedetai;
        }

        public async ValueTask<List<tbm_image_type>> GET_TBM_IMAGE_TYPEAsync()
        {
            List<tbm_image_type> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT *
                                FROM [{DBENV}].[dbo].[tbm_image_type]
                                where status=1"
            };

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_image_type>().ToList();

                }
            }
            return dataObjects;
        }

        public async ValueTask<long> GET_SEQ_IMAGEAsync(string ijob_id)
        {
            long seq = 0;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT ISNULL(MAX(SEQ),0)
                               FROM [{DBENV}].[dbo].[tbt_job_image]
                                WHERE status =1
                        AND ijob_id =@ijob_id"
            };
            command.Parameters.AddWithValue("@ijob_id", ijob_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    seq = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            return seq;
        }

        public async ValueTask<employee_info> UserLogin(UserLogin user)
        {
            employee_info employee_Info = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT  user_id
                                        ,em.user_name 
                                        ,em.password
                                        ,em.fullname
                                        ,em.lastname 
                                        ,em.position
                                        ,po.position_description   
                                        ,po.security_level
                                FROM [{DBENV}].[dbo].[tbm_employee] em 
                                INNER JOIN [{DBENV}].[dbo].[tbm_employee_position] po on em.position =po.position_code
                                WHERE UPPER(em.user_name) =@username                               
                                AND em.status =1 
                                AND po.status =1"
            };
            command.Parameters.AddWithValue("@username", user.UserName.ToUpper());

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    employee_Info = dt.AsEnumerable<employee_info>().First();
                }
            }
            return employee_Info;
        }

        public async ValueTask<List<tbm_menu>> GET_MENU(string user_id)
        {
            List<tbm_menu> menu = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"select menu.* from [{DBENV}].[dbo].[tbm_employee] em
INNER JOIN [{DBENV}].[dbo].[tbm_employee_position] pos on pos.position_code =em.position
INNER JOIN [{DBENV}].[dbo].tbm_permission per on per.tep_id =pos.tep_id
INNER JOIN [{DBENV}].[dbo].tbm_menu menu on menu.menu_id =per.menu_id
where em.user_id =@user_id
AND em.status =1
AND pos.status =1
AND  per.status =1
AND menu.status =1
"
            };
            command.Parameters.AddWithValue("@user_id", user_id);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    menu = dt.AsEnumerable<tbm_menu>().ToList();
                }
            }
            return menu;
        }
        public async ValueTask<List<tbm_config>> GET_TBM_CONFIG()
        {
            List<tbm_config> config = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT  [config_id]
      ,[config_key]
      ,[config_value]
  FROM [{DBENV}].[dbo].[tbm_config]
"
            };

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    config = dt.AsEnumerable<tbm_config>().ToList();
                }
            }
            return config;
        }
        public async ValueTask<List<tbm_unit>> GET_TBM_UNITAsync()
        {
            List<tbm_unit> menu = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"select * from [{DBENV}].[dbo].[tbm_unit]
"
            };

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    menu = dt.AsEnumerable<tbm_unit>().ToList();
                }
            }
            return menu;
        }

        public async ValueTask<List<tbm_location_store>> GET_TBM_LOCATION_STOREAsync(tbm_location_store condition)
        {
            List<tbm_location_store> menu = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT sto.location_id
      ,sto.location_name
      ,sto.owner_id
	  ,CONCAT(emp.fullname,' ',emp.lastname) owner_name
      ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = sto.create_by) as [create_by]
      ,sto.create_date
      ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = sto.update_by ) as [update_by]
      ,sto.update_date
      FROM [{DBENV}].[dbo].[tbm_location_store] sto 
      LEFT JOIN [{DBENV}].[dbo].[tbm_employee] emp on emp.user_id =sto.owner_id AND emp.status =1
      WHERE sto.status =1
                "
            };
            if (condition is not null && !string.IsNullOrWhiteSpace(condition.location_id))
            {
                command.CommandText += " AND sto.location_id =@location_id";
                command.Parameters.AddWithValue("@location_id", condition.location_id);
            }
            if (condition is not null && !string.IsNullOrWhiteSpace(condition.location_name))
            {
                command.CommandText += " AND sto.location_name =@location_name";
                command.Parameters.AddWithValue("@location_name", condition.location_name);
            }
            if (condition is not null && !string.IsNullOrWhiteSpace(condition.owner_id))
            {
                command.CommandText += " AND sto.owner_id =@owner_id";
                command.Parameters.AddWithValue("@owner_id", condition.owner_id);
            }

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    menu = dt.AsEnumerable<tbm_location_store>().ToList();
                }
            }
            return menu;
        }




        #endregion " GET DATA "

        #region " INSERT DATA "
        public async ValueTask INSERT_TBM_EMPLOYEEAsync(employee data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbm_employee]
           (user_name
           ,password
           ,fullname
           ,lastname
           ,idcard
           ,position
           ,status
           ,create_date
           ,create_by
           ,update_date
           ,update_by
           ,showstock)
     VALUES
           (@user_name
           ,@password
           ,@fullname
           ,@lastname
           ,@idcard
           ,@position
           ,1
           ,GETDATE()
           ,@create_by
           ,null
           ,null
           ,@showstock)"
            };

            sql.Parameters.AddWithValue("@user_name", data.user_name);
            sql.Parameters.AddWithValue("@password", data.password);
            sql.Parameters.AddWithValue("@fullname", data.fullname);
            sql.Parameters.AddWithValue("@lastname", data.lastname);
            sql.Parameters.AddWithValue("@idcard", data.idcard);
            sql.Parameters.AddWithValue("@position", data.position);
            sql.Parameters.AddWithValue("@create_by", data.create_by);
            sql.Parameters.AddWithValue("@showstock", data.showstock);
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBM_CUSTOMERAsync(tbm_customer data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbm_customer]
           (
           cust_type
           ,fname
           ,lname
           ,id_card
           ,address
           ,sub_district_no
           ,district_code
           ,province_code
           ,zip_code
           ,phone_no
           ,Email
           ,STATUS)
     VALUES (    
          @cust_type
          ,@fname
          ,@lname
          ,@id_card
          ,@address
          ,@sub_district_no
          ,@district_code
          ,@province_code
          ,@zip_code
          ,@phone_no
          ,@Email
          ,1
           )"
            };

            sql.Parameters.AddWithValue("@cust_type", data.cust_type);
            sql.Parameters.AddWithValue("@fname", data.fname);
            sql.Parameters.AddWithValue("@lname", data.lname);
            sql.Parameters.AddWithValue("@id_card", data.id_card);
            sql.Parameters.AddWithValue("@address", data.address);
            sql.Parameters.AddWithValue("@sub_district_no", data.sub_district_no);
            sql.Parameters.AddWithValue("@district_code", data.district_code);
            sql.Parameters.AddWithValue("@province_code", data.province_code);
            sql.Parameters.AddWithValue("@zip_code", data.zip_code);
            sql.Parameters.AddWithValue("@phone_no", data.phone_no);
            sql.Parameters.AddWithValue("@Email", data.Email);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBM_VEHICLEAsync(tbm_vehicle data, DateTime? effective_date, DateTime? expire_date)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbm_vehicle]
           (license_no
           ,seq
           ,brand_no
           ,model_no
           ,chassis_no
           ,Color
           ,effective_date
           ,expire_date
           ,service_price
           ,service_no
           ,contract_no
           ,customer_id
           ,contract_type
           ,std_pmp
           ,employee_id)
     VALUES
           (@license_no
           ,@seq
           ,@brand_no
           ,@model_no
           ,@chassis_no
           ,@Color
           ,@effective_date
           ,@expire_date
           ,@service_price
           ,@service_no
           ,@contract_no
           ,@customer_id
           ,@contract_type
           ,@std_pmp
           ,@employee_id)"
            };

            sql.Parameters.AddWithValue("@license_no", data.license_no);
            sql.Parameters.AddWithValue("@seq", data.seq);
            sql.Parameters.AddWithValue("@brand_no", data.brand_no);
            sql.Parameters.AddWithValue("@model_no", data.model_no);
            sql.Parameters.AddWithValue("@chassis_no", data.chassis_no);
            sql.Parameters.AddWithValue("@Color", data.Color);
            sql.Parameters.AddWithValue("@effective_date", effective_date);
            sql.Parameters.AddWithValue("@expire_date", expire_date);
            sql.Parameters.AddWithValue("@service_price", data.service_price);
            sql.Parameters.AddWithValue("@service_no", data.service_no);
            sql.Parameters.AddWithValue("@contract_no", data.contract_no);
            sql.Parameters.AddWithValue("@customer_id", data.customer_id);
            sql.Parameters.AddWithValue("@contract_type", data.contract_type);
            sql.Parameters.AddWithValue("@std_pmp", data.std_pmp);
            sql.Parameters.AddWithValue("@employee_id", data.employee_id);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBM_SERVICESAsync(tbm_services data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbm_services]
           (
             services_no
            ,services_name
            ,period_year
            ,create_date
            ,create_by
            ,status
,jobcode)
     VALUES
           (@services_no
           ,@services_name
           ,@period_year
           ,GETDATE()
           ,@create_by
           ,1,@jobcode)"
            };

            sql.Parameters.AddWithValue("@services_no", data.services_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@services_name", data.services_name ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@period_year", data.period_year ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@create_by", data.create_by ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@jobcode", data.jobcode ?? (object)DBNull.Value);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBT_JOB_HEADERAsync(create_job data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @$"INSERT INTO [{DBENV}].[dbo].[tbt_job_header]
           (job_id
            ,type_job
            ,summary
           ,license_no    
           ,customer_id
           ,owner_id
           ,email_customer
           ,create_by
           ,create_date      
          {(string.IsNullOrWhiteSpace(data.ref_hjob_id) ? "" : ",ref_hjob_id")}
           ,status)
     VALUES
           (@job_id
            ,@type_job
,@summary
           ,@license_no   
           ,@customer_id
           ,@owner_id
           ,@email_customer          
           ,@create_by
           ,GETDATE()         
           {(string.IsNullOrWhiteSpace(data.ref_hjob_id) ? "" : ",@ref_hjob_id")}
           ,1)"
            };

            sql.Parameters.AddWithValue("@job_id", data.job_id);
            sql.Parameters.AddWithValue("@type_job", data.type_job);
            sql.Parameters.AddWithValue("@summary", data.summary ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@license_no", data.license_no);
            sql.Parameters.AddWithValue("@customer_id", data.customer_id);
            sql.Parameters.AddWithValue("@owner_id", data.owner_id);
            sql.Parameters.AddWithValue("@email_customer", data.email_customer);
            sql.Parameters.AddWithValue("@create_by", data.user_id);
            if (!string.IsNullOrWhiteSpace(data.ref_hjob_id))
            {
                sql.Parameters.AddWithValue("@ref_hjob_id", data.ref_hjob_id);
            }
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_tbt_job_checklist(tbt_job_checklist data, string job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbt_job_checklist]
           ( ckjob_id
            ,ck_id
            ,description)
     VALUES
           (@ckjob_id
           ,@ck_id         
           ,@description )"
            };

            sql.Parameters.AddWithValue("@ckjob_id", job_id);
            sql.Parameters.AddWithValue("@ck_id", data.ck_id);
            sql.Parameters.AddWithValue("@description", data.description);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBT_JOB_DETAIL(tbt_job_detail data, string job_id, DateTime? CD_tag_date)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,

                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbt_job_detail]
           ( bjob_id
      ,B1_model
      ,B1_serial
      ,B1_amp_hrs
      ,B1_date_code
      ,B1_spec_gravity
      ,B1_volt_static
      ,B1_volt_load
      ,B2_model
      ,B2_serial
      ,B2_amp_hrs
      ,B2_date_code
      ,B2_spec_gravity
      ,B2_volt_static
      ,B2_volt_load
      ,CD_manufact
      ,CD_model
      ,CD_serial
      ,CD_tag_date
      ,H_meter
      ,V_service_mane
      ,V_labour
      ,V_travel
      ,V_total
      ,failure_code
      ,fair_wear)
     VALUES
      (@bjob_id
      ,@B1_model
      ,@B1_serial
      ,@B1_amp_hrs
      ,@B1_date_code
      ,@B1_spec_gravity
      ,@B1_volt_static
      ,@B1_volt_load
      ,@B2_model
      ,@B2_serial
      ,@B2_amp_hrs
      ,@B2_date_code
      ,@B2_spec_gravity
      ,@B2_volt_static
      ,@B2_volt_load
      ,@CD_manufact
      ,@CD_model
      ,@CD_serial
      ,@CD_tag_date
      ,@H_meter
      ,@V_service_mane
      ,@V_labour
      ,@V_travel
      ,@V_total
      ,@failure_code
      ,@fair_wear )"
            };

            SqlParameter bjob_id = sql.Parameters.AddWithValue("@bjob_id", job_id);
            if (job_id is null)
            {
                bjob_id.Value = DBNull.Value;
            }
            SqlParameter B1_model = sql.Parameters.AddWithValue("@B1_model", data.B1_model);
            if (data.B1_model is null)
            {
                B1_model.Value = DBNull.Value;
            }
            SqlParameter B1_serial = sql.Parameters.AddWithValue("@B1_serial", data.B1_serial);
            if (data.B1_serial is null)
            {
                B1_serial.Value = DBNull.Value;
            }
            SqlParameter B1_amp_hrs = sql.Parameters.AddWithValue("@B1_amp_hrs", data.B1_amp_hrs);
            if (data.B1_amp_hrs is null)
            {
                B1_amp_hrs.Value = DBNull.Value;
            }
            SqlParameter B1_date_code = sql.Parameters.AddWithValue("@B1_date_code", data.B1_date_code);
            if (data.B1_date_code is null)
            {
                B1_date_code.Value = DBNull.Value;
            }
            SqlParameter B1_spec_gravity = sql.Parameters.AddWithValue("@B1_spec_gravity", data.B1_spec_gravity);
            if (data.B1_spec_gravity is null)
            {
                B1_spec_gravity.Value = DBNull.Value;
            }
            SqlParameter B1_volt_static = sql.Parameters.AddWithValue("@B1_volt_static", data.B1_volt_static);
            if (data.B1_volt_static is null)
            {
                B1_volt_static.Value = DBNull.Value;
            }
            SqlParameter B1_volt_load = sql.Parameters.AddWithValue("@B1_volt_load", data.B1_volt_load);
            if (data.B1_volt_load is null)
            {
                B1_volt_load.Value = DBNull.Value;
            }
            SqlParameter B2_model = sql.Parameters.AddWithValue("@B2_model", data.B2_model);
            if (data.B2_model is null)
            {
                B2_model.Value = DBNull.Value;
            }
            SqlParameter B2_serial = sql.Parameters.AddWithValue("@B2_serial", data.B2_serial);
            if (data.B2_serial is null)
            {
                B2_serial.Value = DBNull.Value;
            }
            SqlParameter B2_amp_hrs = sql.Parameters.AddWithValue("@B2_amp_hrs", data.B2_amp_hrs);
            if (data.B2_amp_hrs is null)
            {
                B2_amp_hrs.Value = DBNull.Value;
            }
            SqlParameter B2_date_code = sql.Parameters.AddWithValue("@B2_date_code", data.B2_date_code);
            if (data.B2_date_code is null)
            {
                B2_date_code.Value = DBNull.Value;
            }
            SqlParameter B2_spec_gravity = sql.Parameters.AddWithValue("@B2_spec_gravity", data.B2_spec_gravity);
            if (data.B2_spec_gravity is null)
            {
                B2_spec_gravity.Value = DBNull.Value;
            }
            SqlParameter B2_volt_static = sql.Parameters.AddWithValue("@B2_volt_static", data.B2_volt_static);
            if (data.B2_volt_static is null)
            {
                B2_volt_static.Value = DBNull.Value;
            }
            SqlParameter B2_volt_load = sql.Parameters.AddWithValue("@B2_volt_load", data.B2_volt_load);
            if (data.B2_volt_load is null)
            {
                B2_volt_load.Value = DBNull.Value;
            }
            SqlParameter CD_manufact = sql.Parameters.AddWithValue("@CD_manufact", data.CD_manufact);
            if (data.CD_manufact is null)
            {
                CD_manufact.Value = DBNull.Value;
            }
            SqlParameter CD_model = sql.Parameters.AddWithValue("@CD_model", data.CD_model);
            if (data.CD_model is null)
            {
                CD_model.Value = DBNull.Value;
            }
            SqlParameter CD_serial = sql.Parameters.AddWithValue("@CD_serial", data.CD_serial);
            if (data.CD_serial is null)
            {
                CD_serial.Value = DBNull.Value;
            }
            SqlParameter _CD_tag_date = sql.Parameters.AddWithValue("@CD_tag_date", CD_tag_date);
            if (CD_tag_date is null)
            {
                _CD_tag_date.Value = DBNull.Value;
            }
            SqlParameter H_meter = sql.Parameters.AddWithValue("@H_meter", data.H_meter);
            if (data.H_meter is null)
            {
                H_meter.Value = DBNull.Value;
            }
            SqlParameter V_service_mane = sql.Parameters.AddWithValue("@V_service_mane", data.V_service_mane);
            if (data.V_service_mane is null)
            {
                V_service_mane.Value = DBNull.Value;
            }
            SqlParameter V_labour = sql.Parameters.AddWithValue("@V_labour", data.V_labour);
            if (data.V_labour is null)
            {
                V_labour.Value = DBNull.Value;
            }
            SqlParameter V_travel = sql.Parameters.AddWithValue("@V_travel", data.V_travel);
            if (data.V_travel is null)
            {
                V_travel.Value = DBNull.Value;
            }
            SqlParameter V_total = sql.Parameters.AddWithValue("@V_total", data.V_total);
            if (data.V_total is null)
            {
                V_total.Value = DBNull.Value;
            }
            SqlParameter failure_code = sql.Parameters.AddWithValue("@failure_code", data.failure_code);
            if (data.failure_code is null)
            {
                failure_code.Value = DBNull.Value;
            }
            SqlParameter fair_wear = sql.Parameters.AddWithValue("@fair_wear", data.fair_wear);
            if (data.fair_wear is null)
            {
                fair_wear.Value = DBNull.Value;
            }
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBT_JOB_IMAGE(tbt_job_image data, string userid)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO  [{DBENV}].[dbo].[tbt_job_image]
     ( ijob_id
      ,seq
      ,img_name
      ,img_path
      ,create_date
      ,create_by
      ,status
      ,image_type)
     VALUES
      (@ijob_id
      ,@seq
      ,@img_name
      ,@img_path
      ,GETDATE()
      ,@create_by   
      ,1
      ,@image_type)"
            };

            sql.Parameters.AddWithValue("@ijob_id", data.ijob_id);
            sql.Parameters.AddWithValue("@seq", data.seq);
            sql.Parameters.AddWithValue("@img_name", data.img_name);
            sql.Parameters.AddWithValue("@img_path", data.img_path);
            sql.Parameters.AddWithValue("@create_by", userid);
            sql.Parameters.AddWithValue("@image_type", data.image_type);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBT_JOB_PART(tbt_job_part data, string userid, string job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbt_job_part]
     ( pjob_id
      ,seq
      ,part_no
      ,part_id
      ,total
      ,create_date
      ,create_by
      ,status)
     VALUES
      (@pjob_id
      ,@seq
      ,@part_no
      ,@part_id
      ,@total
      ,GETDATE()
      ,@create_by
      ,1)"
            };

            sql.Parameters.AddWithValue("@pjob_id", job_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@seq", data.seq ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_no", data.part_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_id", data.part_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@total", data.total ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@create_by", userid ?? (object)DBNull.Value);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBM_LOCATION_STOREAsync(tbm_location_store data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO  [{DBENV}].[dbo].[tbm_location_store]
     ( location_id
      ,location_name
      ,owner_id
      ,create_by
      ,create_date
      ,status)
     VALUES
      (@location_id
      ,@location_name
      ,@owner_id
      ,@create_by
      ,GETDATE()    
      ,1)"
            };

            sql.Parameters.AddWithValue("@location_id", data.location_id);
            sql.Parameters.AddWithValue("@location_name", data.location_name);
            sql.Parameters.AddWithValue("@owner_id", data.owner_id);
            sql.Parameters.AddWithValue("@create_by", data.create_by);
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO  [{DBENV}].[dbo].[tbm_sparepart]
     ( 
      [part_no]
      ,[part_name]
      ,[part_desc]
      ,[part_type]
      ,[cost_price]
      ,[sale_price]
      ,[unit_code]
      ,[part_value]
      ,[part_weight]
      ,[minimum_value]
      ,[maximum_value]
      ,[location_id]
      ,[create_date]
      ,[create_by]
      ,[ref_group]
      ,[ref_other]  
     )
     VALUES
      (
      @part_no
      ,@part_name
      ,@part_desc
      ,@part_type
      ,@cost_price
      ,@sale_price
      ,@unit_code
      ,@part_value
      ,@part_weight 
      ,@minimum_value
      ,@maximum_value
      ,@location_id
      ,GETDATE()
      ,@create_by
      ,@ref_group
      ,@ref_other
)"
            };

            sql.Parameters.AddWithValue("@part_no", data.part_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_name", data.part_name ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_desc", data.part_desc ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_type", data.part_type ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@cost_price", data.cost_price?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@sale_price", data.sale_price?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@unit_code", data.unit_code?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_value", data.part_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_weight", data.part_weight?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@minimum_value", data.minimum_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@maximum_value", data.maximum_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@location_id", data.location_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@create_by", data.create_by ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@ref_group", data.ref_group ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@ref_other", data.ref_other ?? (object)DBNull.Value);
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBT_ADJ_SPAREPARTAsync(tbt_adj_sparepart data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"
INSERT INTO  [{DBENV}].[dbo].[tbt_adj_sparepart]
           (
           [part_id]
           ,[part_no]
           ,[adj_part_value]
           ,[remark]
           ,[create_date]
           ,[create_by]
           ,[adj_type])
     VALUES
           (
           @part_id
           ,@part_no
           ,@adj_part_value
           ,@remark
           ,GETDATE()
           ,@create_by
           ,@adj_type)
"
            };
            sql.Parameters.AddWithValue("@part_id", data.part_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_no", data.part_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@adj_part_value", data.adj_part_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@remark", data.remark ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@create_by", data.create_by ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@adj_type", data.adj_type ?? (object)DBNull.Value);
            await sql.ExecuteNonQueryAsync();
        }

        #endregion " INSERT DATA "

        #region " UPDATE "
        public async ValueTask UPDATE_TBT_JOB_HEADERAsync(create_job data)
        {

            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbt_job_header] 
                                SET
                                    license_no=@license_no,
                                   email_customer= @email_customer,
                                    ref_hjob_id =@ref_hjob_id,
                                    UPDATE_BY =@UPDATE_BY,
                                    UPDATE_DATE =GETDATE()
                                WHERE job_id =@job_id
"
            };
            command.Parameters.AddWithValue("@license_no", data.license_no);
            command.Parameters.AddWithValue("@email_customer", data.email_customer);
            command.Parameters.AddWithValue("@ref_hjob_id", data.ref_hjob_id);
            command.Parameters.AddWithValue("@UPDATE_BY", data.user_id);
            command.Parameters.AddWithValue("@job_id", data.job_id);
            await command.ExecuteNonQueryAsync();

        }
        public async ValueTask Close_jobAsync(close_job data, tbt_job_header old)
        {
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbt_job_header]
                                SET
                                    summary=@summary,
                                    action= @action,
                                    result =@result,
                                    transfer_to =@transfer_to,
                                    {(((old.job_status is null || old.job_status == "D") && data.job_status == "C") || (data.job_status == "F") ? "fix_date = GETDATE()," : "")}        
                                    {(data.job_status == "C" ? "Close_DATE =GETDATE()," : "")}
                                    invoice_no=@invoice_no,
                                    update_date=GETDATE(),
                                    update_by=@update_by,
                                    job_status =@job_status
                                WHERE job_id =@job_id"
            };
            SqlParameter summary = command.Parameters.AddWithValue("@summary", data.summary);
            if (data.summary is null)
            {
                summary.Value = DBNull.Value;
            }
            SqlParameter job_id = command.Parameters.AddWithValue("@job_status", data.job_status);

            if (data.job_id is null)
            {
                job_id.Value = DBNull.Value;
            }
            SqlParameter action = command.Parameters.AddWithValue("@action", data.action);
            if (data.action is null)
            {
                action.Value = DBNull.Value;
            }
            SqlParameter result = command.Parameters.AddWithValue("@result", data.result);
            if (data.result is null)
            {
                result.Value = DBNull.Value;
            }
            SqlParameter transfer_to = command.Parameters.AddWithValue("@transfer_to", data.transfer_to);
            if (data.transfer_to is null)
            {
                transfer_to.Value = DBNull.Value;
            }
            SqlParameter invoice_no = command.Parameters.AddWithValue("@invoice_no", data.invoice_no);
            if (data.invoice_no is null)
            {
                invoice_no.Value = DBNull.Value;
            }
            command.Parameters.AddWithValue("@update_by", data.userid ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@job_id", data.job_id ?? (object)DBNull.Value);
            await command.ExecuteNonQueryAsync();

        }


        public async ValueTask UPDATE_TBM_CUSTOMERAsync(tbm_customer data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbm_customer]
           SET
           cust_type =@cust_type
           ,fname=@fname
           ,lname=@lname
           ,address=@address
           ,sub_district_no=@sub_district_no
           ,district_code=@district_code
           ,province_code=@province_code
           ,zip_code=@zip_code
           ,phone_no=@phone_no
           ,Email=@Email
     WHERE customer_id =@customer_id "
            };

            sql.Parameters.AddWithValue("@cust_type", data.cust_type ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fname", data.fname ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@lname", data.lname ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@address", data.address ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@sub_district_no", data.sub_district_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@district_code", data.district_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@province_code", data.province_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@zip_code", data.zip_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@phone_no", data.phone_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@Email", data.Email ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@customer_id", data.customer_id ?? (object)DBNull.Value);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_EMPLOYEEAsync(employee data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbm_employee]
           SET  
           fullname =@fullname
           ,lastname=@lastname
           ,idcard=@idcard
           ,position=@position
           ,update_date =GETDATE()
           ,update_by =@create_by
           , showstock=@showstock
                where user_id =@user_id"
            };


            sql.Parameters.AddWithValue("@fullname", data.fullname);
            sql.Parameters.AddWithValue("@lastname", data.lastname);
            sql.Parameters.AddWithValue("@idcard", data.idcard);
            sql.Parameters.AddWithValue("@position", data.position);
            sql.Parameters.AddWithValue("@create_by", data.create_by);
            sql.Parameters.AddWithValue("@user_id", data.user_id);
            sql.Parameters.AddWithValue("@showstock", data.showstock);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_VEHICLEAsync(tbm_vehicle data, DateTime? effective_date, DateTime? expire_date)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbm_vehicle]
           SET     
                     seq=@seq
                    ,brand_no=@brand_no
                    ,model_no=@model_no
                    ,chassis_no=@chassis_no
                    ,Color=@Color
                    ,effective_date=@effective_date
                    ,expire_date=@expire_date
                    ,service_price=@service_price
                    ,service_no=@service_no
                    ,contract_no=@contract_no
                    ,customer_id=@customer_id
                where license_no = @license_no"
            };


            sql.Parameters.AddWithValue("@seq", data.seq);
            sql.Parameters.AddWithValue("@brand_no", data.brand_no);
            sql.Parameters.AddWithValue("@model_no", data.model_no);
            sql.Parameters.AddWithValue("@chassis_no", data.chassis_no);
            sql.Parameters.AddWithValue("@Color", data.Color);
            sql.Parameters.AddWithValue("@effective_date", effective_date);
            sql.Parameters.AddWithValue("@expire_date", expire_date);
            sql.Parameters.AddWithValue("@service_price", data.service_price);
            sql.Parameters.AddWithValue("@service_no", data.service_no);
            sql.Parameters.AddWithValue("@contract_no", data.contract_no);
            sql.Parameters.AddWithValue("@customer_id", data.customer_id);
            sql.Parameters.AddWithValue("@license_no", data.license_no);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_SERVICESAsync(tbm_services data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbm_services]
           SET          
            services_name=@services_name
            ,period_year=@period_year
            ,update_date=GETDATE()
            ,update_by=@create_by
WHERE   services_no=@services_no"

            };
            sql.Parameters.AddWithValue("@services_name", data.services_name);
            sql.Parameters.AddWithValue("@period_year", data.period_year);
            sql.Parameters.AddWithValue("@create_by", data.create_by);
            sql.Parameters.AddWithValue("@services_no", data.services_no);
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBT_JOB_DETAIL(tbt_job_detail data, string job_id, DateTime? CD_tag_date)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbt_job_detail]
           SET
      B1_model =@B1_model
      ,B1_serial=@B1_serial
      ,B1_amp_hrs=@B1_amp_hrs
      ,B1_date_code=@B1_date_code
      ,B1_spec_gravity=@B1_spec_gravity
      ,B1_volt_static=@B1_volt_static
      ,B1_volt_load=@B1_volt_load
      ,B2_model=@B2_model
      ,B2_serial=@B2_serial
      ,B2_amp_hrs=@B2_amp_hrs
      ,B2_date_code=@B2_date_code
      ,B2_spec_gravity=@B2_spec_gravity
      ,B2_volt_static=@B2_volt_static
      ,B2_volt_load=@B2_volt_load
      ,CD_manufact=@CD_manufact
      ,CD_model=@CD_model
      ,CD_serial=@CD_serial
      ,CD_tag_date=@CD_tag_date
      ,H_meter=@H_meter
      ,V_service_mane=@V_service_mane
      ,V_labour=@V_labour
      ,V_travel=@V_travel
      ,V_total=@V_total
      ,failure_code=@failure_code
      ,fair_wear=@fair_wear
    WHERE bjob_id =@bjob_id"
            };

            sql.Parameters.AddWithValue("@B1_model", data.B1_model ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B1_serial", data.B1_serial ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B1_amp_hrs", data.B1_amp_hrs ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B1_date_code", data.B1_date_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B1_spec_gravity", data.B1_spec_gravity ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B1_volt_static", data.B1_volt_static ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B1_volt_load", data.B1_volt_load ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B2_model", data.B2_model ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B2_serial", data.B2_serial ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B2_amp_hrs", data.B2_amp_hrs ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B2_date_code", data.B2_date_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B2_spec_gravity", data.B2_spec_gravity ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B2_volt_static", data.B2_volt_static ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@B2_volt_load", data.B2_volt_load ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@CD_manufact", data.CD_manufact ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@CD_model", data.CD_model ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@CD_serial", data.CD_serial ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@CD_tag_date", CD_tag_date ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@H_meter", data.H_meter ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@V_service_mane", data.V_service_mane ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@V_labour", data.V_labour ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@V_travel", data.V_travel ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@V_total", data.V_total ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@failure_code", data.failure_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fair_wear", data.fair_wear ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@bjob_id", job_id ?? (object)DBNull.Value);


            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_LOCATION_STOREAsync(tbm_location_store data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE  [{DBENV}].[dbo].[tbm_location_store]
     set
      location_name=@location_name
      ,owner_id =@owner_id
      ,update_by =@update_by
      ,update_date=GETDATE()
      WHERE  location_id =@location_id"
            };

            sql.Parameters.AddWithValue("@location_name", data.location_name);
            sql.Parameters.AddWithValue("@owner_id", data.owner_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@update_by", data.create_by);
            sql.Parameters.AddWithValue("@location_id", data.location_id);
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @$"UPDATE  [{DBENV}].[dbo].[tbm_sparepart]
     set
      part_no=@part_no
      ,part_name=@part_name
      ,part_desc=@part_desc
      ,part_type=@part_type
      ,cost_price=@cost_price
      ,sale_price=@sale_price
      ,unit_code=@unit_code
      ,part_value=@part_value
      ,minimum_value=@minimum_value
      ,maximum_value=@maximum_value
      ,location_id=@location_id
      ,update_date =GETDATE()
      ,update_by=@update_by
      ,ref_group=@ref_group
      ,ref_other=@ref_other
      {(data.status == "0" ? $@",cancel_by = @cancel_by
      ,cancel_reason =@cancel_reason
      ,cancal_date =GETDATE()" : "")}
      WHERE  part_id =@part_id"
            };

            sql.Parameters.AddWithValue("@part_no", data.part_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_name", data.part_name ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_desc", data.part_desc ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_type", data.part_type ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@cost_price", data.cost_price?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@sale_price", data.sale_price?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@unit_code", data.unit_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_value", data.part_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@minimum_value", data.minimum_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@maximum_value", data.maximum_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@location_id", data.location_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@update_by", data.create_by ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@part_id", data.part_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@ref_group", data.ref_group ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@ref_other", data.ref_other ?? (object)DBNull.Value);
            if (data.status == "0")
            {
                sql.Parameters.AddWithValue("@cancel_by", data.create_by ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@cancel_reason", data.cancel_reason ?? (object)DBNull.Value);
            }

            await sql.ExecuteNonQueryAsync();
        }


        public async ValueTask UPDATE_TBT_ADJ_SPAREPARTAsync(tbt_adj_sparepart data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @$"UPDATE  [{DBENV}].[dbo].[tbt_adj_sparepart]
     set
       adj_part_value =@adj_part_value
      ,remark =@remark
      ,update_date =GETDATE()
      ,update_by =@update_by
     {(string.IsNullOrEmpty(data.cancel_by) ? "" :
     $@",cancel_by=@cancel_by
      ,cancel_reason =@cancel_reason")}   
      WHERE  adj_id =@adj_id"
            };

            sql.Parameters.AddWithValue("@adj_part_value", data.adj_part_value.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@update_by", data.create_by ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@remark", data.remark ?? (object)DBNull.Value);
            if (!string.IsNullOrEmpty(data.cancel_by))
            {
                sql.Parameters.AddWithValue("@cancel_by", data.cancel_by ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@cancel_reason", data.cancel_reason ?? (object)DBNull.Value);
            }
            sql.Parameters.AddWithValue("@adj_id", data.adj_id ?? (object)DBNull.Value);

            await sql.ExecuteNonQueryAsync();
        }
        #endregion " UPDATE "

        #region " TERMINATE "

        public async ValueTask TERMINATE_TBM_CUSTOMERAsync(string customer_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbm_customer]
                        SET
                            STATUS =0
                     WHERE customer_id =@customer_id "
            };
            sql.Parameters.AddWithValue("@customer_id", customer_id);

            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBM_EMPLOYEEAsync(string user_id, string terminate_id)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbm_employee]
                SET  
                STATUS=0,
                update_date=GETDATE(),
                update_by=@update_by
                where user_id =@user_id"
            };


            sql.Parameters.AddWithValue("@update_by", terminate_id);
            sql.Parameters.AddWithValue("@user_id", user_id);


            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBM_SERVICESAsync(string services_no, string user_id)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbm_services]
           SET          
            status =0
            ,update_date=GETDATE()
            ,update_by=@create_by
            WHERE   services_no=@services_no"

            };
            sql.Parameters.AddWithValue("@create_by", user_id);
            sql.Parameters.AddWithValue("@services_no", services_no);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBM_LOCATION_STOREAsync(string location_id, string user_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE  [{DBENV}].[dbo].[tbm_location_store]
        set    
            status =0
            ,update_by =@update_by
            ,update_date=GETDATE()
      WHERE  location_id =@location_id"
            };

            sql.Parameters.AddWithValue("@update_by", user_id);
            sql.Parameters.AddWithValue("@location_id", location_id);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBM_SPAREPARTAsync(long? part_id, string user_id, string cancel_reason)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE  [{DBENV}].[dbo].[tbm_sparepart]
     set
      cancal_date=GETDATE()
      ,cancel_by =@cancel_by
      ,cancel_reason=@cancel_reason
      WHERE  part_id =@part_id"
            };
            sql.Parameters.AddWithValue("@cancel_by", user_id);
            sql.Parameters.AddWithValue("@cancel_reason", cancel_reason);
            sql.Parameters.AddWithValue("@part_id", part_id);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBT_ADJ_SPAREPARTAsync(long? adj_id, string user_id, string cancel_reason)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE  [{DBENV}].[dbo].[tbt_adj_sparepart]
     set
      cancal_date=GETDATE()
      ,cancel_by =@cancel_by
      ,cancel_reason=@cancel_reason
      WHERE  adj_id =@adj_id"
            };
            sql.Parameters.AddWithValue("@cancel_by", user_id);
            sql.Parameters.AddWithValue("@cancel_reason", cancel_reason);
            sql.Parameters.AddWithValue("@adj_id", adj_id);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBT_JOB_CHECKLISTAsync(string Job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"DELETE [{DBENV}].[dbo].[tbt_job_checklist] WHERE ckjob_id =@job_id"
            };
            sql.Parameters.AddWithValue("@job_id", Job_id);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBT_JOB_PART(string job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"DELETE [{DBENV}].[dbo].[tbt_job_part] WHERE pjob_id =@job_id"
            };

            sql.Parameters.AddWithValue("@job_id", job_id);
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask TERMINATE_TBT_JOB_IMAGE(string job_id, string seq)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbt_job_image]
SET  status =0
WHERE  ijob_id =@ijob_id AND seq =@seq"
            };

            sql.Parameters.AddWithValue("@ijob_id", job_id);
            sql.Parameters.AddWithValue("@seq", seq);

            await sql.ExecuteNonQueryAsync();
        }
        #endregion " TERMINATE "

        #region " REPORT "

        public async ValueTask<tbt_job_image> GET_IMAGE_SIG(string condition)
        {
            tbt_job_image dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"
SELECT  [ijob_id]
      ,[seq]
      ,[img_name]
      ,[img_path]
      ,[create_date]
      ,[create_by]
      ,[status]
      ,[image_type]
  FROM [{DBENV}].[dbo].[tbt_job_image]
  WHERE ijob_id =@job_id 
  AND status =1
  AND image_type='sig'
ORDER BY seq DESC
"
            };
            sql.Parameters.AddWithValue("@job_id", condition);
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_image>().FirstOrDefault();
                }
            }
            return dataObjects;
        }
        /*  public async ValueTask<List<summary_job_list>> GET_Summary_job_list(summary_job_list_condition condition,
          DateTime? jobfrom,
          DateTime? jobto,
          DateTime? fixfrom,
          DateTime? fixto,
          DateTime? closefrom,
          DateTime? closeto)
          {
              List<summary_job_list> dataObjects = null;
              SqlCommand sql = new SqlCommand
              {
                  CommandType = System.Data.CommandType.Text,
                  Connection = this.sqlConnection,
                  CommandText = $@"SELECT JH.job_id
        ,JH.[create_date]
        ,JH.[license_no]
        ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = JH.[create_by]) as [create_by]
        ,CONCAT(emp.fullname,' ',emp.lastname)AS employeename
        ,JH.[summary]
        ,JH.[fix_date]
        ,JH.[close_date]
        ,CONCAT(cus.fname,' ',cus.lname) customername
        ,[invoice_no]
        ,Jt.jobdescription
        ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = COALESCE(JH.transfer_to,JH.[owner_id])) AS owner_id
        ,JH.type_job
        , (select top 1 seq from [{DBENV}].[dbo].[tbt_job_image] where status =1 and image_type ='rpt'
        )seq
    FROM [{DBENV}].[dbo].[tbt_job_header] JH
    INNER JOIN [{DBENV}].[dbo].[tbm_employee] emp ON emp.user_id = COALESCE(JH.transfer_to,JH.[owner_id])
    INNER JOIN [{DBENV}].[dbo].[tbm_customer] cus ON CUS.customer_id =JH.customer_id
    INNER JOIN [{DBENV}].[dbo].[tbm_jobtype] Jt ON JT.jobcode =jh.type_job 
   -- INNER JOIN [{DBENV}].[dbo].[tbt_job_image] img ON img.ijob_id =JH.job_id 
  WHERE 1=1  "
              };
              if (jobfrom is not null)
              {
                  sql.CommandText += $" AND JH.create_date between @createfrm and @createto ";
                  sql.Parameters.AddWithValue("@createfrm", jobfrom);
                  sql.Parameters.AddWithValue("@createto", jobto);
              }
              if (fixfrom is not null)
              {
                  sql.CommandText += $" AND JH.fix_date between @fix_datefrm and @fix_dateto ";
                  sql.Parameters.AddWithValue("@fix_datefrm", fixfrom);
                  sql.Parameters.AddWithValue("@fix_dateto", fixto);
              }
              if (closefrom is not null)
              {
                  sql.CommandText += $" AND JH.close_date between @closefrom and @closeto ";
                  sql.Parameters.AddWithValue("@closefrom", closefrom);
                  sql.Parameters.AddWithValue("@closeto", closeto);
              }
              if (!string.IsNullOrEmpty(condition.license_no))
              {
                  sql.CommandText += $" AND JH.license_no =@license_no ";
                  sql.Parameters.AddWithValue("@license_no", condition.license_no);
              }
              if (!string.IsNullOrEmpty(condition.type_job))
              {
                  sql.CommandText += $"AND JH.type_job =@type_job ";
                  sql.Parameters.AddWithValue("@type_job", condition.type_job.ToUpper());
              }
              if (!string.IsNullOrEmpty(condition.Teachnicial))
              {
                  sql.CommandText += $" AND COALESCE(JH.transfer_to,JH.[owner_id]) =@Teachnicial ";
                  sql.Parameters.AddWithValue("@Teachnicial", condition.Teachnicial);
              }
              if (!string.IsNullOrEmpty(condition.customer_name))
              {
                  sql.CommandText += $" AND cus.fname like @customer_name";
                  sql.Parameters.AddWithValue("@customer_name", $"%{condition.customer_name}%");
              }
              using (DataTable dt = await Utility.FillDataTableAsync(sql))
              {
                  if (dt.Rows.Count > 0)
                  {
                      dataObjects = dt.AsEnumerable<summary_job_list>().ToList();
                  }
              }
              return dataObjects;
          }*/
        /*   public async ValueTask<List<summary_stock_list>> GET_Summary_stock_list(summary_stock_list_condition condition,
                DateTime? partcrtfrom,
                DateTime? partcrtto)
           {
               List<summary_stock_list> dataObjects = null;
               SqlCommand sql = new SqlCommand
               {
                   CommandType = System.Data.CommandType.Text,
                   Connection = this.sqlConnection,
                   CommandText = $@"
   SELECT  sp.[part_id]
         ,sp.[part_no]
         ,[part_name]
         ,[part_desc]
         ,[part_type]
         ,[cost_price]    
         ,sp.[create_date]
         ,[sale_price]
         ,sp.[unit_code]
         , unit_name
         ,[part_value]
         ,[minimum_value]
         ,[maximum_value]
         ,sp.[location_id]
         ,lo.location_name
         ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = sp.[create_by] ) as [create_by]
         ,[cancal_date]
         ,sp.[cancel_by]
         ,sp.[cancel_reason]
         ,sp.[update_date]
         ,(select CONCAT(fullname,' ',lastname) from [{DBENV}].[dbo].[tbm_employee] where user_id = sp.[update_by]) as [update_by]
         ,adj.adj_part_value
         ,adj.remark
     FROM [{DBENV}].[dbo].[tbm_sparepart] sp
   left join [{DBENV}].[dbo].[tbt_adj_sparepart] adj on adj.part_id =sp.part_id
   left join [{DBENV}].[dbo].[tbm_location_store] lo on lo.location_id =sp.location_id
   left JOIN [{DBENV}].[dbo].[tbm_unit] un on un.unit_code =sp.unit_code
   WHERE 1=1 
   "
               };
               if (partcrtfrom is not null)
               {
                   sql.CommandText += $" AND sp.create_date between @createfrm and @createto ";
                   sql.Parameters.AddWithValue("@createfrm", partcrtfrom);
                   sql.Parameters.AddWithValue("@createto", partcrtto);
               }
               if (!string.IsNullOrEmpty(condition.Partno))
               {
                   sql.CommandText += $" AND (sp.part_name like @part_no or sp.part_no like @part_no ) ";
                   sql.Parameters.AddWithValue("@part_no", $"%{condition.Partno}%");
               }
               if (!string.IsNullOrEmpty(condition.locationid))
               {
                   sql.CommandText += $" AND sp.location_id=@location_id ";
                   sql.Parameters.AddWithValue("@location_id", condition.locationid);
               }
               if (!string.IsNullOrEmpty(condition.PartId))
               {
                   sql.CommandText += $" AND sp.part_id=@part_id ";
                   sql.Parameters.AddWithValue("@part_id", condition.PartId);
               }
               using (DataTable dt = await Utility.FillDataTableAsync(sql))
               {
                   if (dt.Rows.Count > 0)
                   {
                       dataObjects = dt.AsEnumerable<summary_stock_list>().ToList();
                   }
               }
               return dataObjects;
           }*/


        public async ValueTask<List<ReportPPM>> sp_getReportPPM(string customerid,
            DateTime? date_from,
            DateTime? date_to)
        {
            List<ReportPPM> dataObjects = null;
            SqlCommand command = new SqlCommand($"[{DBENV}].[dbo].[sp_getReportPPM]", this.sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@date_from", date_from);
            command.Parameters.AddWithValue("@date_to", date_to);
            command.Parameters.AddWithValue("@customer_id", customerid);
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<ReportPPM>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<summary_job_list>> GET_Summary_job_list(summary_job_list_condition condition,
      DateTime? jobfrom,
      DateTime? jobto,
      DateTime? fixfrom,
      DateTime? fixto,
      DateTime? closefrom,
      DateTime? closeto)
        {
            List<summary_job_list> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].[sp_getReportJob]"
            };
            sql.Parameters.AddWithValue("@createfrm", jobfrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@createto", jobto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fix_datefrm", fixfrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fix_dateto", fixto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@closefrom", closefrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@closeto", closeto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@license_no", condition?.license_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@type_job", condition?.type_job?.ToUpper() ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@Teachnicial", condition?.Teachnicial ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@customer_name", condition?.customer_name ?? (object)DBNull.Value);
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<summary_job_list>().ToList();
                }
            }
            return dataObjects;
        }
        #endregion " REPORT "

        #region " CALL STORE "
        public async ValueTask sp_gen_remind_table(string licenseno)
        {
            SqlCommand command = new SqlCommand($"[{DBENV}].[dbo].sp_gen_remind_table", this.sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = this.sqlConnection;
            command.Transaction = this.transaction;
            command.Parameters.AddWithValue("@license_no", licenseno);
            command.ExecuteNonQuery();
        }


        public async ValueTask<List<tbm_sparepart>> GET_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            List<tbm_sparepart> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].[sp_get_tbm_sparepart]"
            };
            command.Parameters.AddWithValue("@P_part_no", data.part_no ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@P_location_id", data.location_id ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@P_job_id", data.jobid ?? (object)DBNull.Value);

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_sparepart>().ToList();
                }
            }
            return dataObjects;
        }


        public async ValueTask<List<summary_stock_list>> GET_Summary_stock_list(summary_stock_list_condition condition,
          DateTime? partcrtfrom,
          DateTime? partcrtto)
        {
            List<summary_stock_list> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].[sp_getReportStock]"
            };
            sql.Parameters.AddWithValue("@P_part_no", condition.Partno ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_create_from", partcrtfrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_create_to", partcrtto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@p_location_id", condition.locationid ?? (object)DBNull.Value);
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<summary_stock_list>().ToList();
                }
            }
            return dataObjects;
        }
        public async ValueTask MOVECAR_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"[{DBENV}].[dbo].[sp_update_sparepart]"
            };

            sql.Parameters.AddWithValue("@P_part_id", data.part_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_location_id", data.location_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_part_value", data.part_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_user_id", data.create_by ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_part_no", data.part_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_part_name", data.part_name ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@p_part_desc", data.part_desc ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@p_part_type", data.part_type ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@p_cost_price", data.cost_price?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@p_sale_price", data.sale_price?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@p_unit_code", data.unit_code?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_minimum", data.minimum_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_maximum", data.maximum_value?.Replace(",", "") ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_ref_group", data.ref_group ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_ref_other", data.ref_other ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@P_part_weight", data.part_weight?.Replace(",", "") ?? (object)DBNull.Value);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask<List<tbm_sparepart>> sp_tbm_sparepart_detail(string part_id)
        {
            List<tbm_sparepart> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].[sp_get_tbm_sparepart_detail]"
            };

            sql.Parameters.AddWithValue("@P_part_id", part_id ?? (object)DBNull.Value);

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_sparepart>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<List<job_detail_list>> GET_JOB_DETAIL_LISTAsync(string userid)
        {
            List<job_detail_list> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].[sp_get_tbm_job_data]"
            };
            command.Parameters.AddWithValue("@owner_id", userid ?? (object)DBNull.Value);
            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<job_detail_list>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask<DataSet> sp_get_tbm_job_data_detail(string job_id)
        {

            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = this.sqlConnection;
                cmd.CommandText = $@"[{DBENV}].[dbo].[sp_get_tbm_job_data_detail]";
                cmd.Parameters.AddWithValue("@job_id", job_id ?? (object)DBNull.Value);
                System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }

        }

        public async ValueTask<List<movement_sparepart>> sp_get_movement_sparepart(summary_stock_list_condition condition)
        {
            List<movement_sparepart> dataObjects = null;
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = this.sqlConnection;
                cmd.CommandText = $@"[{DBENV}].[dbo].[sp_get_movement_sparepart]";
                //cmd.Parameters.AddWithValue("@P_part_no", condition.Partno ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@P_create_from", partcrtfrom ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@P_create_to", partcrtto ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@p_location_id", condition.locationid ?? (object)DBNull.Value);
                using (DataTable dt = await Utility.FillDataTableAsync(cmd))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dataObjects = dt.AsEnumerable<movement_sparepart>().ToList();
                    }
                }
                return dataObjects;
            }

        }

        public async ValueTask<sp_check_onhand> sp_check_onhand(string partid, string Jobid)
        {
            sp_check_onhand dataObjects = null;
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = this.sqlConnection;
                cmd.CommandText = $@"select [{DBENV}].[dbo].fn_get_onhand(@part_id,@job_id) AS PART_VALUE";
                cmd.Parameters.AddWithValue("@part_id", partid ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@job_id", Jobid ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@P_create_to", partcrtto ?? (object)DBNull.Value);
                //cmd.Parameters.AddWithValue("@p_location_id", condition.locationid ?? (object)DBNull.Value);
                using (DataTable dt = await Utility.FillDataTableAsync(cmd))
                {
                    if (dt.Rows.Count > 0)
                    {
                        dataObjects = dt.AsEnumerable<sp_check_onhand>().First();
                    }
                }
            }
            return dataObjects;
        }

        public async ValueTask sp_update_receive_job(string Jobid)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = this.sqlConnection;
                cmd.Transaction = this.transaction;
                cmd.CommandText = $@"[{DBENV}].[dbo].[sp_update_receive_job]";
                cmd.Parameters.AddWithValue("@job_id", Jobid ?? (object)DBNull.Value);
                await cmd.ExecuteNonQueryAsync();

            }
        }
        public async ValueTask sp_update_start_job(string Jobid)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = this.sqlConnection;
                cmd.Transaction = this.transaction;
                cmd.CommandText = $@"[{DBENV}].[dbo].[sp_update_start_job]";
                cmd.Parameters.AddWithValue("@job_id", Jobid ?? (object)DBNull.Value);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async ValueTask sp_update_travel_job(string Jobid)
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = this.sqlConnection;
                cmd.Transaction = this.transaction;
                cmd.CommandText = $@"[{DBENV}].[dbo].[sp_update_travel_job]";
                cmd.Parameters.AddWithValue("@job_id", Jobid ?? (object)DBNull.Value);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async ValueTask<List<rpt_downtime>> sp_getReportDownTime(summary_job_list_condition condition,
      DateTime? jobfrom,
      DateTime? jobto,
      DateTime? fixfrom,
      DateTime? fixto,
      DateTime? closefrom,
      DateTime? closeto)
        {
            List<rpt_downtime> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].[sp_getReportDownTime]"
            };
            sql.Parameters.AddWithValue("@createfrm", jobfrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@createto", jobto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fix_datefrm", fixfrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fix_dateto", fixto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@closefrom", closefrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@closeto", closeto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@license_no", condition?.license_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@type_job", condition?.type_job?.ToUpper() ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@Teachnicial", condition?.Teachnicial ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@customer_name", condition?.customer_name ?? (object)DBNull.Value);
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<rpt_downtime>().ToList();
                }
            }
            return dataObjects;
           
        }


        public async ValueTask<List<sp_getReportRunningCost>> sp_getReportRunningCost(summary_job_list_condition condition,
      DateTime? jobfrom,
      DateTime? jobto,
      DateTime? fixfrom,
      DateTime? fixto,
      DateTime? closefrom,
      DateTime? closeto)
        {
            List<sp_getReportRunningCost> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = this.sqlConnection,
                CommandText = $@"[{DBENV}].[dbo].[sp_getReportRunningCost]"
            };
            sql.Parameters.AddWithValue("@createfrm", jobfrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@createto", jobto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fix_datefrm", fixfrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@fix_dateto", fixto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@closefrom", closefrom ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@closeto", closeto ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@license_no", condition?.license_no ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@type_job", condition?.type_job?.ToUpper() ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@Teachnicial", condition?.Teachnicial ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@customer_name", condition?.customer_name ?? (object)DBNull.Value);
            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<sp_getReportRunningCost>().ToList();
                }
            }
            return dataObjects;

        }
        #endregion " CALL STORE "

    }
}
