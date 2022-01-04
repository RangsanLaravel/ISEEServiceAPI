using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class tbt_adj_sparepart
    {
     public string adj_id{get;set;}
     public string part_id{get;set;}
     public string part_no{get;set;}
     public string adj_part_value{get;set;}
     public string create_date{get;set;}
     public string create_by{get;set;}
     public string update_date{get;set;}
     public string update_by{get;set;}
     public string cancel_date{get;set;}
     public string cancel_by{get;set;}
     public string cancel_reason{get;set; }

     public string part_name { get; set; }
      public string part_desc { get; set; }
        public string part_value { get; set; }
    }
}
