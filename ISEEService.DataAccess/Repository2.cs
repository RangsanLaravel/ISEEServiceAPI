using ISEEService.DataContract;
using ITUtility;
using DataAccessUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataAccess
{
    public partial class Repository
    {
        public async ValueTask<tbt_job_header> GET_EMAILAsync(string JOBID)
        {
            tbt_job_header dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT * FROM [{DBENV}].[dbo].[tbt_job_header] WHERE job_id =@job_id AND status =1 "
            };
            sql.Parameters.AddWithValue("@job_id", JOBID ?? (object)DBNull.Value);

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_header>().FirstOrDefault();
                }
            }
            return dataObjects;
        }

        public async ValueTask<tbm_employee> GET_EMAILEmployeeAsync(string user_id)
        {
            tbm_employee dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                CommandText = $@"SELECT * FROM [{DBENV}].[dbo].[tbm_employee] WHERE user_id =@user_id AND status =1 "
            };
            sql.Parameters.AddWithValue("@user_id", user_id ?? (object)DBNull.Value);

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbm_employee>().FirstOrDefault();
                }
            }
            return dataObjects;
        }


        public async ValueTask UPDATE_OWNERID(string ownerid,string jobid,string userid)
        {
            tbt_job_header dataObjects = null;
            SqlCommand sql = new SqlCommand
            {
                CommandType = System.Data.CommandType.Text,
                Connection = this.sqlConnection,
                Transaction = this.transaction,
                CommandText = $@"UPDATE [{DBENV}].[dbo].[tbt_job_header]
                                SET owner_id =@ownerid,
                                    update_date=GETDATE(),
                                    update_by=@userid
                               WHERE job_id =@job_id AND status =1 "
            };
            sql.Parameters.AddWithValue("@ownerid", ownerid ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@userid", userid ?? (object)DBNull.Value);
            sql.Parameters.AddWithValue("@job_id", jobid ?? (object)DBNull.Value);
            await  sql.ExecuteNonQueryAsync();          
        }
    }
}
