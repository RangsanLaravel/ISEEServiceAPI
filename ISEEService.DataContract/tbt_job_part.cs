using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class tbt_job_part
    {
      //public string pjob_id{get;set;}
      public string seq{get;set;}
      public string part_no{get;set;}
      public string part_id{get;set; }
        public string part_name { get;set;}
        public string part_desc { get; set; }
        public string part_type { get; set; }
        public string part_type_desc { get; set; }

        private string _total;

        public string total
        {
            get { return _total?.Replace(",",""); }
            set { _total = value; }
        }

        public string create_date{get;set;}
      public string create_by{get;set;}
      public string status{get;set;}
      public string location_name{get;set; }
    }
}
