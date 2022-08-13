using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class ReportPPM
    {
        public string license_no { get; set; }
        public string seq { get; set; }
        public string NT { get; set; }
        public string Alldates { get; set; }
        public string customerid { get; set; }
        public string date_from { get; set; }
        public string date_to { get; set; }
    }
}
