using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class customer_terminate
    {
        public string user_id { get; set; }
        public List<string> customer_id { get; set; }
    }
    
}
