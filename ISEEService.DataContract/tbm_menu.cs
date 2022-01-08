using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
  public  class tbm_menu
    {
      public string menu_id{get;set;}
      public string menu_description{get;set;}
      public string status{get;set;}
      public string icon{get;set;}
      public string menu_controller { get;set; }
        public List<tbm_config> config { get; set; }
    }
}
