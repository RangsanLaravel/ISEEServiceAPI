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
                CommandText = $@"SELECT email_customer FROM [{DBENV}].[dbo].[tbt_job_header] WHERE job_id =@job_id AND status =1 "
            };
            sql.Parameters.AddWithValue("@jobid", JOBID ?? (object)DBNull.Value);

            using (DataTable dt = await Utility.FillDataTableAsync(sql))
            {
                if (dt.Rows.Count > 0)
                {
                    dataObjects = dt.AsEnumerable<tbt_job_header>().FirstOrDefault();
                }
            }
            return dataObjects;
        }
    }
}
