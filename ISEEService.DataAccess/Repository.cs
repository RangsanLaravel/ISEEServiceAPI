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

        public Repository(string connectionstring) : this(new SqlConnection(connectionstring))
        {

        }
        public Repository(SqlConnection ConnectionString)
        {
            this.sqlConnection = ConnectionString;
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
        public async ValueTask<List<tbm_province>> GET_PROVINCEAsync()
        {
            List<tbm_province> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT *
                                FROM [ISEE].[dbo].[tbm_province]
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
                CommandText = @"SELECT *
                            FROM [ISEE].[dbo].[tbm_district]
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
                CommandText = @"SELECT *
                                FROM [ISEE].[dbo].[tbm_sub_district]
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
                CommandText = @"SELECT *
                                FROM [ISEE].[dbo].[tbm_employee_position]
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
                CommandText = @"SELECT *
                            FROM [ISEE].[dbo].[tbm_jobtype]
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
        public async ValueTask<List<Customer>> GET_CUSTOMERAsync(string license_no)
        {
            List<Customer> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"select v.license_no
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
      ,Email from [ISEE].[dbo].[tbm_vehicle] v
                            INNER JOIN [ISEE].[dbo].[tbm_customer] cu ON cu.customer_id =v.customer_id
  INNER JOIN [ISEE].[dbo].[tbm_province] pr on pr.province_code =cu.province_code
  INNER JOIN [ISEE].[dbo].[tbm_district] ds on ds.district_code =cu.district_code
  INNER JOIN [ISEE].[dbo].[tbm_sub_district] sd on sd.sub_district_code =cu.sub_district_no
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
                CommandText = @"SELECT *
                             FROM [ISEE].[dbo].[tbt_job_header]
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
            SqlCommand cmd = new SqlCommand("[ISEE].[dbo].[sp_get_running_no]", this.sqlConnection);
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
                CommandText = @"SELECT *
                             FROM [ISEE].[dbo].[tbm_checklist_group]
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
                CommandText = @"SELECT *
  FROM [ISEE].[dbo].[tbm_checklist]
  WHERE check_group_id =@check_group_id
  AND status =1"
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
                CommandText = @"SELECT *
  FROM [ISEE].[dbo].[tbt_job_detail]
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

        #region " GET JOB DETAIL "
        public async ValueTask<close_job> GET_TBT_JOB_HEADERAsync(string job_id)
        {
           close_job dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT *
                                FROM [ISEE].[dbo].[tbt_job_header]
                                where job_id =@job_id AND STATUS =1"
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
                CommandText = @"SELECT *
                                FROM [ISEE].[dbo].[tbt_job_detail]
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
                CommandText = @"SELECT [ckjob_id]
                                    ,[ck_id]
                                    ,[description]
	                                ,'Y' AS check_list
                                FROM [ISEE].[dbo].[tbt_job_checklist]
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
                CommandText = @"
SELECT  [pjob_id]
      ,[seq]
      ,jpar.[part_no]
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
  FROM [ISEE].[dbo].[tbt_job_part] jpar
  INNER JOIN [ISEE].[dbo].[tbm_sparepart] spar on jpar.part_no =spar.part_no
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
                CommandText = @"
SELECT [ijob_id]
      ,[seq]
      ,[img_name]
	  ,imt.image_description
      ,[img_path]
      ,[create_date]
      ,[create_by]
      ,im.[status]
      ,[image_type]
  FROM [ISEE].[dbo].[tbt_job_image] im
  INNER JOIN tbm_image_type imt ON im.image_type =imt.image_code
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

        public async ValueTask<List<job_detail_list>> GET_JOB_DETAIL_LISTAsync(string userid,bool isAdmin)
        {
            List<job_detail_list> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT [job_id]
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
                FROM [ISEE].[dbo].[tbt_job_header] jh
                INNER JOIN [ISEE].[dbo].[tbm_customer] cus on jh.customer_id =cus.customer_id
                INNER JOIN [ISEE].[dbo].[tbm_employee] emp on emp.user_id =jh.owner_id
                where jh.status =1"
            };

            using (DataTable dt = await Utility.FillDataTableAsync(command))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<job_detail_list>().ToList();
                }
            }
            return dataObjects;
        }

        #endregion " GET JOB DETAIL "

        public async ValueTask<List<tbt_job_part>> GET_TBT_JOB_PART(string pjob_id)
        {
            List<tbt_job_part> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT *
                                FROM [ISEE].[dbo].[tbt_job_part]
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
                CommandText = @"SELECT MAX(SEQ)
                                FROM [ISEE].[dbo].[tbt_job_part]
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
                CommandText = @"SELECT [user_id]
      ,[user_name]
      ,[password]
      ,[fullname]
      ,[lastname]
      ,[idcard]
      ,[position]
	  ,emp.position_description
      ,em.[status]
      ,[create_date]
      ,[create_by]
      ,[update_date]
      ,[update_by]
  FROM [ISEE].[dbo].[tbm_employee] em
  INNER JOIN [ISEE].[dbo].[tbm_employee_position] emp on em.position =emp.position_code
  WHERE em.status =1
  AND em.status =1 "
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
                command.Parameters.AddWithValue("@fullname", $"{data.fullname}%" );
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
                CommandText = @"SELECT customer_id
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
  FROM [ISEE].[dbo].[tbm_customer] cu
  INNER JOIN [ISEE].[dbo].[tbm_province] pr on pr.province_code =cu.province_code
  INNER JOIN [ISEE].[dbo].[tbm_district] ds on ds.district_code =cu.district_code
  INNER JOIN [ISEE].[dbo].[tbm_sub_district] sd on sd.sub_district_code =cu.sub_district_no
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

        public async ValueTask<List<tbm_vehicle>> GET_TBM_VEHICLEAsync(tbm_vehicle data,DateTime? sExpire_date,DateTime? eExpire_date,DateTime? sEffective_date,DateTime? eEffective_date)
        {
            List<tbm_vehicle> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT  license_no
      ,seq
      ,brand_no
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
  FROM [ISEE].[dbo].[tbm_vehicle] v
  INNER JOIN [ISEE].[dbo].[tbm_services] s on s.services_no =v.service_no
  INNER JOIN [ISEE].[dbo].[tbm_customer] cus on cus.customer_id =v.customer_id
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
                CommandText = @"SELECT *
                        FROM [ISEE].[dbo].[tbm_services]
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
        public async ValueTask<List<tbm_sparepart>> GET_TBM_SPAREPARTAsync(tbm_sparepart data)
        {
            List<tbm_sparepart> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"
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
      ,part_value
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
  FROM [ISEE].[dbo].[tbm_sparepart] part 
  INNER JOIN [ISEE].[dbo].[tbm_unit] un ON un.unit_code =part.unit_code
  INNER JOIN [ISEE].[dbo].[tbm_location_store] loca on loca.location_id =part.location_id
  WHERE 1=1 "
            };
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
                command.CommandText += " AND maximum_value =@maximum_value";
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
        }

        public async ValueTask<long?> GET_IMAGE_ID()
        {
            long? image_id = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT NEXT VALUE FOR image_id"
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

        public async ValueTask<List<tbm_image_type>> GET_TBM_IMAGE_TYPEAsync()
        {
            List<tbm_image_type> dataObjects = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT *
                                FROM [ISEE].[dbo].[tbm_image_type]
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
                CommandText = @"SELECT MAX(SEQ)
                               FROM [ISEE].[dbo].[tbt_job_image]
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
                CommandText = @"SELECT  user_id
                                        ,em.user_name 
                                        ,em.password
                                        ,em.fullname
                                        ,em.lastname 
                                        ,em.position
                                        ,po.position_description
                                FROM [ISEE].[dbo].[tbm_employee] em 
                                INNER JOIN [ISEE].[dbo].[tbm_employee_position] po on em.position =po.position_code
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
                CommandText = @"select menu.* from [ISEE].[dbo].[tbm_employee] em
INNER JOIN [ISEE].[dbo].[tbm_employee_position] pos on pos.position_code =em.position
INNER JOIN [ISEE].[dbo].tbm_permission per on per.tep_id =pos.tep_id
INNER JOIN [ISEE].[dbo].tbm_menu menu on menu.menu_id =per.menu_id
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
        public async ValueTask<List<tbm_unit>> GET_TBM_UNITAsync()
        {
            List<tbm_unit> menu = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"select * from [ISEE].[dbo].[tbm_unit]
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

        public async ValueTask<List<tbm_location_store>> GET_TBM_LOCATION_STOREAsync(tbm_location_store condition )
        {
            List<tbm_location_store> menu = null;
            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = @"SELECT sto.location_id
      ,sto.location_name
      ,sto.owner_id
	  ,CONCAT(emp.fullname,' ',emp.lastname) owner_name
      ,sto.create_by
      ,sto.create_date
      ,sto.update_by
      ,sto.update_date
      FROM [ISEE].[dbo].[tbm_location_store] sto 
      INNER JOIN [ISEE].[dbo].[tbm_employee] emp on emp.user_id =sto.owner_id
      WHERE sto.status =1
      AND emp.status =1
                "
            };
            if(condition is not null &&!string.IsNullOrWhiteSpace( condition.location_id))
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
                CommandText = @"INSERT INTO [dbo].[tbm_employee]
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
           ,update_by)
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
           ,null)"
            };

            sql.Parameters.AddWithValue("@user_name", data.user_name);
            sql.Parameters.AddWithValue("@password", data.password);
            sql.Parameters.AddWithValue("@fullname", data.fullname);
            sql.Parameters.AddWithValue("@lastname", data.lastname);
            sql.Parameters.AddWithValue("@idcard", data.idcard);
            sql.Parameters.AddWithValue("@position", data.position);
            sql.Parameters.AddWithValue("@create_by", data.create_by);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBM_CUSTOMERAsync(tbm_customer data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"INSERT INTO [dbo].[tbm_customer]
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

        public async ValueTask INSERT_TBM_VEHICLEAsync(tbm_vehicle data,DateTime? effective_date,DateTime? expire_date)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"INSERT INTO [dbo].[tbm_vehicle]
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
           ,customer_id)
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
           ,@customer_id)"
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

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBM_SERVICESAsync(tbm_services data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"INSERT INTO [dbo].[tbm_services]
           (
             services_no
            ,services_name
            ,period_year
            ,create_date
            ,create_by
            ,status)
     VALUES
           (@services_no
           ,@services_name
           ,@period_year
           ,GETDATE()
           ,@create_by
           ,1)"
            };

            sql.Parameters.AddWithValue("@services_no", data.services_no);
            sql.Parameters.AddWithValue("@services_name", data.services_name);
            sql.Parameters.AddWithValue("@period_year", data.period_year);
            sql.Parameters.AddWithValue("@create_by", data.create_by);          
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBT_JOB_HEADERAsync(create_job data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @$"INSERT INTO [dbo].[tbt_job_header]
           (job_id
           ,license_no    
           ,customer_id
           ,owner_id
           ,email_customer
           ,create_by
           ,create_date      
          {(string.IsNullOrWhiteSpace(data.ref_hjob_id)?"": ",ref_hjob_id")}
           ,status)
     VALUES
           (@job_id
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
                CommandText = @"INSERT INTO [dbo].[tbt_job_header]
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
        public async ValueTask INSERT_TBT_JOB_DETAIL(tbt_job_detail data, string job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"INSERT INTO [dbo].[tbt_job_header]
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

            sql.Parameters.AddWithValue("@bjob_id", job_id);
            sql.Parameters.AddWithValue("@B1_model", data.B1_model);
            sql.Parameters.AddWithValue("@B1_serial", data.B1_serial);
            sql.Parameters.AddWithValue("@B1_amp_hrs", data.B1_amp_hrs);
            sql.Parameters.AddWithValue("@B1_date_code", data.B1_date_code);
            sql.Parameters.AddWithValue("@B1_spec_gravity", data.B1_spec_gravity);
            sql.Parameters.AddWithValue("@B1_volt_static", data.B1_volt_static);
            sql.Parameters.AddWithValue("@B1_volt_load", data.B1_volt_load);
            sql.Parameters.AddWithValue("@B2_model", data.B2_model);
            sql.Parameters.AddWithValue("@B2_serial", data.B2_serial);
            sql.Parameters.AddWithValue("@B2_amp_hrs", data.B2_amp_hrs);
            sql.Parameters.AddWithValue("@B2_date_code", data.B2_date_code);
            sql.Parameters.AddWithValue("@B2_spec_gravity", data.B2_spec_gravity);
            sql.Parameters.AddWithValue("@B2_volt_static", data.B2_volt_static);
            sql.Parameters.AddWithValue("@B2_volt_load", data.B2_volt_load);
            sql.Parameters.AddWithValue("@CD_manufact", data.CD_manufact);
            sql.Parameters.AddWithValue("@CD_model", data.CD_model);
            sql.Parameters.AddWithValue("@CD_serial", data.CD_serial);
            sql.Parameters.AddWithValue("@CD_tag_date", data.CD_tag_date);
            sql.Parameters.AddWithValue("@H_meter", data.H_meter);
            sql.Parameters.AddWithValue("@V_service_mane", data.V_service_mane);
            sql.Parameters.AddWithValue("@V_labour", data.V_labour);
            sql.Parameters.AddWithValue("@V_travel", data.V_travel);
            sql.Parameters.AddWithValue("@V_total", data.V_total);
            sql.Parameters.AddWithValue("@failure_code", data.failure_code);
            sql.Parameters.AddWithValue("@fair_wear", data.fair_wear);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask INSERT_TBT_JOB_IMAGE(tbt_job_image data,string userid)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"INSERT INTO  [ISEE].[dbo].[tbt_job_image]
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
      ,@create_date
      ,GETDATE()
      ,1
      ,@image_type)"
            };

            sql.Parameters.AddWithValue("@ijob_id", data.ijob_id);
            sql.Parameters.AddWithValue("@seq", data.seq);
            sql.Parameters.AddWithValue("@img_name",data.img_name);
            sql.Parameters.AddWithValue("@img_path",data.img_path);
            sql.Parameters.AddWithValue("@create_by", userid);
            sql.Parameters.AddWithValue("@image_type",data.image_type);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBT_JOB_PART(tbt_job_part data, string userid,string job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"INSERT INTO [ISEE].[dbo].[tbt_job_part]
     ( pjob_id
      ,seq
      ,part_no
      ,total
      ,create_date
      ,create_by
      ,status)
     VALUES
      (@pjob_id
      ,@seq
      ,@part_no
      ,@total
      ,GETDATE()
      ,@create_by
      ,1)"
            };

            sql.Parameters.AddWithValue("@pjob_id", job_id);
            sql.Parameters.AddWithValue("@seq", data.seq);
            sql.Parameters.AddWithValue("@part_no", data.part_no);
            sql.Parameters.AddWithValue("@total", data.total);
            sql.Parameters.AddWithValue("@create_by", userid);
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBM_LOCATION_STOREAsync(tbm_location_store data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"INSERT INTO  [ISEE].[dbo].[tbm_location_store]
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
                CommandText = @"INSERT INTO  [ISEE].[dbo].[tbm_sparepart]
     ( 
      [part_no]
      ,[part_name]
      ,[part_desc]
      ,[part_type]
      ,[cost_price]
      ,[sale_price]
      ,[unit_code]
      ,[part_value]
      ,[minimum_value]
      ,[maximum_value]
      ,[location_id]
      ,[create_date]
      ,[create_by]
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
      ,@minimum_value
      ,@maximum_value
      ,@location_id
      ,GETDATE()
      ,@create_by)"
            };

            sql.Parameters.AddWithValue("@part_no", data.part_no);
            sql.Parameters.AddWithValue("@part_name", data.part_name);
            sql.Parameters.AddWithValue("@part_desc", data.part_desc);
            sql.Parameters.AddWithValue("@part_type", data.part_type);
            sql.Parameters.AddWithValue("@cost_price", data.cost_price);
            sql.Parameters.AddWithValue("@sale_price", data.sale_price);
            sql.Parameters.AddWithValue("@unit_code", data.unit_code);
            sql.Parameters.AddWithValue("@part_value", data.part_value);
            sql.Parameters.AddWithValue("@minimum_value", data.minimum_value);
            sql.Parameters.AddWithValue("@maximum_value", data.maximum_value);
            sql.Parameters.AddWithValue("@location_id", data.location_id);
            sql.Parameters.AddWithValue("@create_by", data.create_by);
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
                CommandText = @"UPDATE [dbo].[tbt_job_header] 
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
        public async ValueTask Close_jobAsync(close_job data)
        {

            SqlCommand command = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [dbo].[tbt_job_header] 
                                SET
                                    summary=@summary,
                                    action= @action,
                                    result =@result,
                                    transfer_to =@transfer_to,
                                     {(data.fix_date == "N" ? "" : "fix_date =GETDATE(),")}
                                    invoice_no=@invoice_no,
                                    update_date=GETDATE(),
                                    update_by=@update_by
                                WHERE job_id =@job_id
"
            };
            command.Parameters.AddWithValue("@summary", data.summary);
            command.Parameters.AddWithValue("@action", data.action);
            command.Parameters.AddWithValue("@result", data.result);
            command.Parameters.AddWithValue("@transfer_to", data.transfer_to);
            command.Parameters.AddWithValue("@invoice_no", data.invoice_no);
            command.Parameters.AddWithValue("@update_by", data.userid);
            command.Parameters.AddWithValue("@job_id", data.job_id);
            await command.ExecuteNonQueryAsync();

        }

        public async ValueTask UPDATE_TBM_CUSTOMERAsync(tbm_customer data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE [dbo].[tbm_customer]
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

            sql.Parameters.AddWithValue("@cust_type", data.cust_type);
            sql.Parameters.AddWithValue("@fname", data.fname);
            sql.Parameters.AddWithValue("@lname", data.lname);
            sql.Parameters.AddWithValue("@address", data.address);
            sql.Parameters.AddWithValue("@sub_district_no", data.sub_district_no);
            sql.Parameters.AddWithValue("@district_code", data.district_code);
            sql.Parameters.AddWithValue("@province_code", data.province_code);
            sql.Parameters.AddWithValue("@zip_code", data.zip_code);
            sql.Parameters.AddWithValue("@phone_no", data.phone_no);
            sql.Parameters.AddWithValue("@Email", data.Email);
            sql.Parameters.AddWithValue("@customer_id", data.customer_id);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_EMPLOYEEAsync(employee data)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE [dbo].[tbm_employee]
           SET  
           fullname =@fullname
           ,lastname=@lastname
           ,idcard=@idcard
           ,position=@position
           ,update_date =GETDATE()
           ,update_by =@create_by
                where user_id =@user_id"
            };

          
            sql.Parameters.AddWithValue("@fullname", data.fullname);
            sql.Parameters.AddWithValue("@lastname", data.lastname);
            sql.Parameters.AddWithValue("@idcard", data.idcard);
            sql.Parameters.AddWithValue("@position", data.position);
            sql.Parameters.AddWithValue("@create_by", data.create_by);
            sql.Parameters.AddWithValue("@user_id", data.user_id);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_VEHICLEAsync(tbm_vehicle data, DateTime? effective_date, DateTime? expire_date)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE[dbo].[tbm_vehicle]
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
                CommandText = @"UPDATE [dbo].[tbm_services]
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

        public async ValueTask UPDATE_TBT_JOB_DETAIL(tbt_job_detail data, string job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE [dbo].[tbt_job_detail]
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
    WHERE bjob_id =@job_id"
            };

            sql.Parameters.AddWithValue("@B1_model", data.B1_model);
            sql.Parameters.AddWithValue("@B1_serial", data.B1_serial);
            sql.Parameters.AddWithValue("@B1_amp_hrs", data.B1_amp_hrs);
            sql.Parameters.AddWithValue("@B1_date_code", data.B1_date_code);
            sql.Parameters.AddWithValue("@B1_spec_gravity", data.B1_spec_gravity);
            sql.Parameters.AddWithValue("@B1_volt_static", data.B1_volt_static);
            sql.Parameters.AddWithValue("@B1_volt_load", data.B1_volt_load);
            sql.Parameters.AddWithValue("@B2_model", data.B2_model);
            sql.Parameters.AddWithValue("@B2_serial", data.B2_serial);
            sql.Parameters.AddWithValue("@B2_amp_hrs", data.B2_amp_hrs);
            sql.Parameters.AddWithValue("@B2_date_code", data.B2_date_code);
            sql.Parameters.AddWithValue("@B2_spec_gravity", data.B2_spec_gravity);
            sql.Parameters.AddWithValue("@B2_volt_static", data.B2_volt_static);
            sql.Parameters.AddWithValue("@B2_volt_load", data.B2_volt_load);
            sql.Parameters.AddWithValue("@CD_manufact", data.CD_manufact);
            sql.Parameters.AddWithValue("@CD_model", data.CD_model);
            sql.Parameters.AddWithValue("@CD_serial", data.CD_serial);
            sql.Parameters.AddWithValue("@CD_tag_date", data.CD_tag_date);
            sql.Parameters.AddWithValue("@H_meter", data.H_meter);
            sql.Parameters.AddWithValue("@V_service_mane", data.V_service_mane);
            sql.Parameters.AddWithValue("@V_labour", data.V_labour);
            sql.Parameters.AddWithValue("@V_travel", data.V_travel);
            sql.Parameters.AddWithValue("@V_total", data.V_total);
            sql.Parameters.AddWithValue("@failure_code", data.failure_code);
            sql.Parameters.AddWithValue("@fair_wear", data.fair_wear);
            sql.Parameters.AddWithValue("@bjob_id", job_id);


            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask UPDATE_TBM_LOCATION_STOREAsync(tbm_location_store data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE  [ISEE].[dbo].[tbm_location_store]
     set
      location_name=@location_name
      ,owner_id =@owner_id
      ,update_by =@update_by
      ,update_date=GETDATE()
      WHERE  location_id =@location_id"
            };

            sql.Parameters.AddWithValue("@location_name", data.location_name);
            sql.Parameters.AddWithValue("@owner_id", data.owner_id);
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
                CommandText = @$"UPDATE  [ISEE].[dbo].[tbm_sparepart]
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
      ${(data.status == "0" ? @",cancel_by = @cancel_by
      ,cancel_reason =@cancel_reason
      ,cancal_date =GETDATE()" :"")}
      WHERE  part_id =@part_id"
            };

            sql.Parameters.AddWithValue("@part_no", data.part_no);
            sql.Parameters.AddWithValue("@part_name", data.part_name);
            sql.Parameters.AddWithValue("@part_desc", data.part_desc);
            sql.Parameters.AddWithValue("@part_type", data.part_type);
            sql.Parameters.AddWithValue("@cost_price", data.cost_price);
            sql.Parameters.AddWithValue("@sale_price", data.sale_price);
            sql.Parameters.AddWithValue("@unit_code", data.unit_code);
            sql.Parameters.AddWithValue("@part_value", data.part_value);
            sql.Parameters.AddWithValue("@minimum_value", data.minimum_value);
            sql.Parameters.AddWithValue("@maximum_value", data.maximum_value);
            sql.Parameters.AddWithValue("@location_id", data.location_id);
            sql.Parameters.AddWithValue("@update_by", data.create_by);
            sql.Parameters.AddWithValue("@part_id", data.part_id);
            if(data.status == "0")
            {
                sql.Parameters.AddWithValue("@cancel_by", data.create_by);
                sql.Parameters.AddWithValue("@cancel_reason", data.cancel_reason);
            }
           
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
                CommandText = @"UPDATE [dbo].[tbm_customer]
                        SET
                            STATUS =0
                     WHERE customer_id =@customer_id "
            };          
            sql.Parameters.AddWithValue("@customer_id", customer_id);

            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask TERMINATE_TBM_EMPLOYEEAsync(string user_id,string terminate_id)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE [dbo].[tbm_employee]
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
        public async ValueTask TERMINATE_TBM_SERVICESAsync(string services_no,string user_id)
        {

            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE [dbo].[tbm_services]
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
        public async ValueTask TERMINATE_TBM_LOCATION_STOREAsync(string location_id,string user_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE  [ISEE].[dbo].[tbm_location_store]
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
        public async ValueTask TERMINATE_TBM_SPAREPARTAsync(long? part_id,string user_id,string cancel_reason)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"UPDATE  [ISEE].[dbo].[tbm_sparepart]
     set
      cancal_date=GETDATE()
      ,cancel_by =@cancel_by
      ,cancel_reason
      WHERE  part_id =@part_id"
            };               
            sql.Parameters.AddWithValue("@cancel_by", user_id);
            sql.Parameters.AddWithValue("@cancel_reason", cancel_reason);
            sql.Parameters.AddWithValue("@part_id", part_id);
            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask TERMINATE_TBT_JOB_CHECKLISTAsync(string Job_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = @"DELETE [ISEE].[dbo].[tbt_job_checklist] WHERE ckjob_id =@job_id"
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
                CommandText = @"DELETE [ISEE].[dbo].[tbt_job_part] WHERE pjob_id =@job_id"
            };

            sql.Parameters.AddWithValue("@job_id", job_id);         
            await sql.ExecuteNonQueryAsync();
        }
        #endregion " TERMINATE "



    }
}
