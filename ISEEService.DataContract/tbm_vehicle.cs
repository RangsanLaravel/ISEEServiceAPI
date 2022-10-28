using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
  public  class tbm_vehicle
    {
        public string license_no { get; set; }
        public string seq { get; set; }
        public string brand_no { get; set; }
        public string brand_name_tha { get; set; }
        public string model_no { get; set; }
        public string chassis_no { get; set; }
        public string Color { get; set; }
        public string effective_date { get; set; }
        public string expire_date { get; set; }
        public string service_price { get; set; }
        public string service_no { get; set; }
        public string services_name { get; set; }
        public string contract_no { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string contract_type { get; set; }
        public string std_pmp { get; set; }
        public string employee_id { get; set; }
    }
}
