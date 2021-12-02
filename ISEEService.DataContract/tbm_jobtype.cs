using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class tbm_jobtype
    {
        public int? tj_id { get; set; }
        public string jobcode { get; set; }
        public string jobdescription { get; set; }
        public string status { get; set; }
        public string running_type { get; set; }
    }
}
