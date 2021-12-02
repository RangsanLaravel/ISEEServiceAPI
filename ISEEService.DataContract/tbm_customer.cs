using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class tbm_customer
    {
        public string customer_id { get; set; }
        public string id_card { get; set; }
        public string cust_type { get; set; }
        public string cust_type_DESCRIPTION { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string sub_district_no { get; set; }
        public string sub_district_name_tha { get; set; }
        public string district_code { get; set; }
        public string district_name_tha { get; set; }
        public string province_code { get; set; }
        public string province_name_tha { get; set; }
        public string zip_code { get; set; }
        public string phone_no { get; set; }
        public string Email { get; set; }

    }
}
