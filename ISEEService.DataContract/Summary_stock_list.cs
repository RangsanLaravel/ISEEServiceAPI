﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
  public  class summary_stock_list
    {
        public string part_id { get; set; }
        public string part_no { get; set; }
        public string part_name { get; set; }
        public string part_desc { get; set; }
        public string part_type { get; set; }
        public string part_type_desc { get; set; }

        public string cost_price { get; set; }
        public string sale_price { get; set; }
        public string unit_code { get; set; }
        public string unit_name { get; set; }
        public string part_value { get; set; }
        public string part_weight { get; set; }
        public string stock_in { get; set; }
        public string stock_out { get; set; }

        public string minimum_value { get; set; }
        public string maximum_value { get; set; }
        public string location_id { get; set; }
        public string location_name { get; set; }
        public string create_date { get; set; }
        public string create_by { get; set; }
        public string cancal_date { get; set; }
        public string cancel_by { get; set; }
        public string cancel_reason { get; set; }
        public string update_date { get; set; }
        public string update_by { get; set; }
        public string status { get; set; }
        public string remark { get; set; }

        public string adj_part_value { get; set; }
        public string adj_type { get; set; }
        public string ref_group { get; set; }
        public string ref_other { get; set; }
        public string Adj_StockINOUT_Date { get; set; }

    }
}
