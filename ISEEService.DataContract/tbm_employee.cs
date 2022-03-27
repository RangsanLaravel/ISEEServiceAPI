using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class tbm_employee : default_data
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string lastname { get; set; }
        public string idcard { get; set; }
        public string position { get; set; }        
        public string position_description { get; set; }
        public string showstock { get; set; }
        public string location_name { get; set; }
        public string location_id { get; set; }

    }
}
