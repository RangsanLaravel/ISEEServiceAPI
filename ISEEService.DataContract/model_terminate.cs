using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
  public  class model_terminate
    {
        public string user_id { get; set; }
        public List<detail_terminate> data { get; set; }
    }
    public class detail_terminate
    {
        public string id { get; set; }
        public string msg { get; set; }
    }
}
