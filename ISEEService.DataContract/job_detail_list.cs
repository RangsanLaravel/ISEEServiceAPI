using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class job_detail_list
    {
        public string job_id{get;set;}
        public string type_job { get; set; }
        public string license_no{get;set;}
      public string customer_id{get;set;}
       public string cus_fullname { get; set; }
        public string summary{get;set;}
       public string action{get;set;}
       public string result{get;set;}
       public string transfer_to{get;set;}
       public string fix_date{get;set;}
       public string close_date{get;set;}
       public string email_customer{get;set;}
       public string invoice_no{get;set;}
       public string owner_id{get;set;}
       public string  emp_fullname { get; set; }
        public string create_by{get;set;}
     public string create_date{get;set;}
     public string update_by{get;set;}
     public string update_date{get;set;}
       public string ref_hjob_id{get;set;}
    }
}
