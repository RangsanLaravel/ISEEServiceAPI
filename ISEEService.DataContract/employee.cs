using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class employee
    {
        public string user_id { get; set; }
  
        public string user_name { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public string lastname { get; set; }
        public string idcard { get; set; }
        public string position { get; set; }
        public string create_by { get; set; }
    }
}
