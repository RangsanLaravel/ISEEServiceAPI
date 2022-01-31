using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
    public class summary_job_list_condition
    {
        public string job_date_from { get; set; }
        public string job_date_to { get; set; }
        public string customer_name { get; set; }
        public string user_id { get; set; }
        public string fix_date_from { get; set; }
        public string fix_date_to { get; set; }
        public string close_dt_from { get; set; }
        public string close_dt_to { get; set; }
        public string license_no { get; set; }
        public string user_print { get; set; }
        public string type_job { get; set; }
        public string Teachnicial { get; set; }
        public string report_type { get; set; }
        public List<summary_job_list> summary_job_list { get; set; }
    }
}
