using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
  public  class sp_check_onhand
    {
        public string part_value { get; set; }
        public string job { get; set; }
        public string adj { get; set; }
        public string ttm { get; set; }
        public string tfm { get; set; }
    }
}
