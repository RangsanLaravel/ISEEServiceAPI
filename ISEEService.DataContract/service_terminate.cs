﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class service_terminate
    {
        public string user_id { get; set; }
        public List<string> services_no { get; set; }
    }
}
