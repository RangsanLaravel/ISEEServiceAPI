using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
 public   class check_list_header_rpt
    {
        public string Customer { get; set; }
        public string Job_id { get; set; }
        public List<check_list_rpt> checklist { get; set; }
    }
}
