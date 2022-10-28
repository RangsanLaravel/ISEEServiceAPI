using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class movement_sparepart
    {
        public string part_id { get; set; }
        public string part_no { get; set; }
        public string stock_type { get; set; }
        public string total { get; set; }
        public string user_name { get; set; }
        public string job_number { get; set; }
        public string create_date { get; set; }
        public string location_id { get; set; }

    }
}
