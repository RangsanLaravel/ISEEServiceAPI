using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class tbm_brand
    {
        public string brand_id { get; set; }
        public string brand_code { get; set; }
        public string brand_name_tha { get; set; }
        public string brand_name_eng { get; set; }
        public string active_flag { get; set; }
        public string create_by { get; set; }
        public string update_by { get; set; }
        public string create_date { get; set; }
        public string update_date { get; set; }
    
    }
}
