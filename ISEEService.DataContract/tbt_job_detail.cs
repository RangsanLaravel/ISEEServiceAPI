using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
    public class tbt_job_detail
    {
        public string bjob_id { get; set; }
        public string B1_model { get; set; }
        public string B1_serial { get; set; }
        public string B1_amp_hrs { get; set; }
        public string B1_date_code { get; set; }
        public string B1_spec_gravity { get; set; }
        public string B1_volt_static { get; set; }
        public string B1_volt_load { get; set; }
        public string B2_model { get; set; }
        public string B2_serial { get; set; }
        public string B2_amp_hrs { get; set; }
        public string B2_date_code { get; set; }
        public string B2_spec_gravity { get; set; }
        public string B2_volt_static { get; set; }
        public string B2_volt_load { get; set; }
        public string CD_manufact { get; set; }
        public string CD_model { get; set; }
        public string CD_serial { get; set; }
        public string CD_tag_date { get; set; }
        public string H_meter { get; set; }
        public string V_service_mane { get; set; }
        public string V_labour { get; set; }
        public string V_travel { get; set; }
        public string V_total { get; set; }
        public string failure_code { get; set; }
        public string fair_wear { get; set; }
    }
}
