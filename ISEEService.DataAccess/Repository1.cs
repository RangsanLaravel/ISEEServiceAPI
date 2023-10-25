using ITUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISEEService.DataContract;
using ITUtility;
using DataAccessUtility;

namespace ISEEService.DataAccess
{
    public partial class Repository
    {
        public async ValueTask<List<tbm_substatus>> TBM_SUBSTATUSAsync()
        {
            List<tbm_substatus> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"select  * from [{DBENV}].dbo.tbm_substatus WHERE ACTIVE_FLG='1' ORDER BY STATUS_SEQ ASC"
            };

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_substatus>().ToList();
                }
            }
            return dataObjects;
        }

        public async ValueTask INSERT_TBM_SUBSTATUSAsync(string job_id,string job_status,string substatus,string status_remark,string create_id)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbt_job_substatus]
           (
job_id,
job_status,
substatus,
status_remark,
status_dt,
create_id,
active_flg)
     VALUES
           (
@job_id,
@job_status,
@substatus,
@status_remark,
GETDATE(),
@create_id,
'1')"
            };

            sql.Parameters.AddWithValue("@job_id", job_id ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@job_status", job_status ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@substatus", substatus ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@status_remark", status_remark ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@create_id", create_id ?? (object)DBNull.Value);
           
            await sql.ExecuteNonQueryAsync();
        }
        public async ValueTask INSERT_TBT_EMAIL_HISTORYAsync(tbt_email_history data)
        {
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"INSERT INTO [{DBENV}].[dbo].[tbt_email_history]
           (
email_code,
job_id,
customer_id,
email_address,
send_dt,
send_by,
active_flg,
license_no)
     VALUES
           (
@email_code,
@job_id,
@customer_id,
@email_address,
GETDATE(),
@send_by,
'1',
@license_no)"
            };
            sql.Parameters.AddWithValue("@email_code", data.email_code ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@job_id", data.job_id  ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@customer_id", data.customer_id?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@email_address", data.email_address ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@send_by", data.send_by ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@license_no", data.license_no ?? (object)DBNull.Value);

            await sql.ExecuteNonQueryAsync();
        }

        public async ValueTask<List<tbt_job_substatus>> TBT_JOB_SUBSTATUSAsync(string JOBID)
        {
            List<tbt_job_substatus> dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"
                           SELECT 
	tbt.status_remark,
	tb.STATUS_DESCRIPTION as substatus,
	tbt.status_dt,
	(SELECT fullname+' '+lastname FROM [{DBENV}].[dbo].tbm_employee
where status=1
and user_id =tbt.create_id) AS create_id
  FROM [{DBENV}].[dbo].[tbm_substatus] tb
  INNER JOIN [{DBENV}].[dbo].tbt_job_substatus tbt
  ON tb.STATUS_CODE =tbt.substatus
  WHERE tbt.active_flg ='1'
  AND tb.ACTIVE_FLG ='1'
  AND tb.STATUS_TYPE ='JOB'
  AND UPPER(tbt.job_id)= UPPER(@jobid)
ORDER BY tbt.status_dt ASC"
            };
            sql.Parameters.AddWithValue("@jobid", JOBID ?? (object)DBNull.Value);

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_substatus>().ToList();
                }
            }
            return dataObjects;
        }

    }
}
