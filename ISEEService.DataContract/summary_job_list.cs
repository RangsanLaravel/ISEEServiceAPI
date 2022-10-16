using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class summary_job_list
    {
        public string job_id { get; set; }
        public string create_date { get; set; }
        public string license_no { get; set; }
        public string create_by { get; set; }
        public string employeename { get; set; }
        public string summary { get; set; }
        public string fix_date { get; set; }
        public string close_date { get; set; }
        public string customername { get; set; }
        public string invoice_no { get; set; }
        public string jobdescription { get; set; }
        public string owner_id { get; set; }
        public string type_job { get; set; }
        public string seq { get; set; }
        public string downtime_day { get; set; }
        public string downtime_hour { get; set; }
        public string receive_date_time { get; set; }
        public string start_travel_date_time { get; set; }
        public string start_job_date_time { get; set; }
        public string reponse_day { get; set; }
        public string reponse_hour { get; set; }
        public string travel_day { get; set; }
        public string travel_hour { get; set; }

    }
}
