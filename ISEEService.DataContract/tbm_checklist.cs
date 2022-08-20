using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
    public class tbm_checklist
    {
        public string ch_id { get; set; }
        public string check_name { get; set; }
        public string show_in_rpt { get; set; }
        public string status { get; set; }
        public string status_check { get; set; }
        public string remark { get; set; }
        public string check_group_id { get; set; }
        public string create_date { get; set; }
        public string create_by { get; set; }
        public string update_date { get; set; }
        public string update_by { get; set; }
    }
}
