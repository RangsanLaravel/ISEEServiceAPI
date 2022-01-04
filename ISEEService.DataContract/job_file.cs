using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITUtility;

namespace ISEEService.DataContract
{
  public  class job_file 
    {
        public string image_type { get; set; }
        public string FileName
        { get; set; }

        public string ContentType
        { get; set; }

        public string FileData
        { get; set; }

    }
}
