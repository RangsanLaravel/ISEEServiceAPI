using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
  public  class sparepart_terminate
    {
        public string user_id { get; set; }
        public List<sparepart_id> part_detail { get; set; }
    }
    public class sparepart_id
    {
        public long? part_id { get; set; }
        public string cancel_reason { get; set; }
    }
}
