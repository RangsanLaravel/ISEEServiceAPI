using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
    public class sp_getReportProductive_time
    {
        public string full_name { get; set; }
        public string job_in_month { get; set; }
        public string day_in_month { get; set; }
        public string holiday_in_month { get; set; }
        public string work_actual_in_month { get; set; }
        public string leave_in_month { get; set; }
        public string day_in_month_leave { get; set; }
        public string actual_hour_in_month { get; set; }
        public string all_hour_in_month { get; set; }
        public string range_date { get; set; }
    }
}
