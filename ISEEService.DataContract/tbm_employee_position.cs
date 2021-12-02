using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class tbm_employee_position
    {
        public string tep_id { get; set; }
        public string position_code { get; set; }
        public string position_description { get; set; }
        public string status { get; set; }
    }
}
