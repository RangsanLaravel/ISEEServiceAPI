using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class create_job
    {
        public string job_id { get; set; }
        public string type_job { get; set; }
        public string summary { get; set; }
        public string owner_id { get; set; }
        public string license_no { get; set; }
        public string customer_id { get; set; }
        public string email_customer { get; set; }
        public string ref_hjob_id { get; set; }
        public string user_id { get; set; }
    }
}
