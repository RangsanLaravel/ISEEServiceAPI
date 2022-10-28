using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
    public class sp_getReportRunningCost
    {
        public string CUST_NAME { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string MODEL_CODE { get; set; }
        public string SJC_NUMBER { get; set; }
        public string JOB_DESC { get; set; }
        public string SJC_DATE { get; set; }
        public string FAIR_WEAR { get; set; }
        public string METER_READING { get; set; }
        public string ITEM_NUMBER { get; set; }
        public string DESCRIPTION { get; set; }
        public string SELLING_PRICE { get; set; }
        public string QTY { get; set; }
        public string TOTAL_SELLING_PRICE { get; set; }
        public string BRANCH_PRICE { get; set; }
        public string TOTAL_BRANCH_PRICE { get; set; }
        public string FLeet { get; set; }
        public string Job_Location { get; set; }
        public string SJC_Free_Text_Lines { get; set; }
    }
}
