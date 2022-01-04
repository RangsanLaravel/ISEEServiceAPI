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
        private string _B1_spec_gravity;

        public string B1_spec_gravity
        {
            get { return _B1_spec_gravity?.Replace(",",""); }
            set { _B1_spec_gravity = value; }
        }


        private string _B1_volt_static;

        public string B1_volt_static
        {
            get { return _B1_volt_static?.Replace(",", ""); }
            set { _B1_volt_static = value; }
        }


        private string _B1_volt_load;

        public string B1_volt_load
        {
            get { return _B1_volt_load?.Replace(",", ""); }
            set { _B1_volt_load = value; }
        }

        public string B2_model { get; set; }
        public string B2_serial { get; set; }
        public string B2_amp_hrs { get; set; }
        public string B2_date_code { get; set; }

        private string _B2_spec_gravity;
        public string B2_spec_gravity
        {
            get { return _B2_spec_gravity?.Replace(",", ""); }
            set { _B2_spec_gravity = value; }
        }


        private string _B2_volt_static;

        public string B2_volt_static
        {
            get { return _B2_volt_static?.Replace(",", ""); }
            set { _B2_volt_static = value; }
        }


        private string _B2_volt_load;

        public string B2_volt_load
        {
            get { return _B2_volt_load?.Replace(",", ""); }
            set { _B2_volt_load = value; }
        }
        public string CD_manufact { get; set; }
        public string CD_model { get; set; }
        public string CD_serial { get; set; }
        public string CD_tag_date { get; set; }
        public string H_meter { get; set; }
        public string V_service_mane { get; set; }
        private string _V_labour;

        public string V_labour
        {
            get { return _V_labour?.Replace(",", ""); }
            set { _V_labour = value; }
        }

        private string _V_travel;

        public string V_travel
        {
            get { return _V_travel?.Replace(",", ""); }
            set { _V_travel = value; }
        }


        private string _V_total;

        public string V_total
        {
            get { return _V_total?.Replace(",", ""); }
            set { _V_total = value; }
        }

        public string failure_code { get; set; }
        public string fair_wear { get; set; }
    }
}
