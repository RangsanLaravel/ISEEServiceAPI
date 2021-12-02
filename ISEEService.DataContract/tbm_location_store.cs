using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class tbm_location_store
    {
      public string location_id{get;set;}
      public string location_name{get;set;}
      public string owner_id{get;set;}
      public string owner_name{get;set; }
      public string create_by{get;set;}
      public string create_date{get;set;}
      public string update_by{get;set;}
      public string update_date{get;set;}
      public string status{get;set;}
    }
}
