using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
    public class tbm_substatus
    {
        public string id { get; set; }
        public string STATUS_CODE { get; set; }
        public string STATUS_DESCRIPTION { get; set; }
        public string STATUS_SEQ { get; set; }
        public string ACTIVE_FLG { get; set; }
        public string STATUS_TYPE { get; set; }
    }
}
