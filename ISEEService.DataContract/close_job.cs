using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class close_job
    {
        public string job_id { get; set; }
        public string type_job { get; set; }
        public string license_no { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string summary { get; set; }
        public string action { get; set; }
        public string result { get; set; }
        public string transfer_to { get; set; }
        public string email_customer { get; set; }
        //public string fix_date { get; set; }
        public string invoice_no { get; set; }
        public string owner_id { get; set; }
        public string owner_name { get; set; }
        public string ref_hjob_id { get; set; }
        public string userid { get; set; }
        public tbt_job_detail job_detail { get; set; }
        public  List<tbt_job_checklist> job_checklists { get; set; }
        public List<job_file> job_images { get; set; }
        public List <tbt_job_image> image_detail { get; set; }
        public List<tbt_job_part> job_parts { get; set; }

        public string flg_close { get; set; }
        public string job_status { get; set; } 
        public string rptsig { get; set; }
        public string receive_date { get; set; }
        public string travel_date { get; set; }
        public string job_date { get; set; }
        public string model_no { get; set; }
        public string substatus { get; set; }
        public string substatus_remark { get; set; }
    }
}
