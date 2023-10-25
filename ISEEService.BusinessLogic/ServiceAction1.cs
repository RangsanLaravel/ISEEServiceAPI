using DevExpress.ClipboardSource.SpreadsheetML;
using ISEEService.DataAccess;
using ISEEService.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.BusinessLogic
{
    public partial class ServiceAction
    {

        public async ValueTask<List<tbm_substatus>> TBM_SUBSTATUSAsync()
        {
            List<tbm_substatus> dataObjects = null;
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            try
            {
                dataObjects = await repository.TBM_SUBSTATUSAsync();
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

        public async ValueTask INSERT_TBM_SUBSTATUSAsync(string job_id, string job_status, string substatus, string status_remark, string creat_id)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                await repository.INSERT_TBM_SUBSTATUSAsync(job_id, job_status, substatus, status_remark, creat_id); 
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
        public async ValueTask INSERT_TBT_EMAIL_HISTORYAsync(tbt_email_history data)
        {
            Repository repository = new Repository(_connectionstring, DBENV);
            await repository.OpenConnectionAsync();
            await repository.beginTransection();
            try
            {
                await repository.INSERT_TBT_EMAIL_HISTORYAsync(data);
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
