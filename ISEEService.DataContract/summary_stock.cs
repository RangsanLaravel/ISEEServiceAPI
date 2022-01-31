using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
    public class summary_stock
    {
        public string Partno { get; set; }
        public string PartId { get; set; }
        public string locationid { get; set; }
        public string Part_create_from { get; set; }
        public string Part_create_to { get; set; }
        public string UserName { get; set; }
        public string report_type { get; set; }
    }
}
