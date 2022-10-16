using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
  public  class rpt_downtime
    {
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string serv_call { get; set; }
        public string Serial_Num { get; set; }
        public string fleet { get; set; }
        public string jobdescription { get; set; }
        public string job_description { get; set; }
        public string fairware { get; set; }
        public string log_date { get; set; }
        public string complete_date { get; set; }
        public string Total_downtime_day { get; set; }
        public string Total_downtime_hour { get; set; }
        public string Tech { get; set; }
        public string FSM { get; set; }
        public string Note { get; set; }
    }
}
