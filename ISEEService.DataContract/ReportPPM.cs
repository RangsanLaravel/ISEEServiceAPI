using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEEService.DataContract
{
   public class ReportPPM
    {
        public string Serial_Number { get; set; }
        public string Fleet_Number { get; set; }
        public string Model_code { get; set; }
        public string contract_type { get; set; }
        public string PMP { get; set; }
        public string STD_PMP_TIME { get; set; }
        public string Customer_Name { get; set; }
        public string Site_Address_Line1 { get; set; }
        public string PREF_FSR1 { get; set; }
        public string PREF_FSR1_NAME { get; set; }
        public string LAST_CONTACT { get; set; }
        public string Contact_Phone_Number { get; set; }

        public string JANUARY { get; set; }
        public string FEBRUARY { get; set; }
        public string MARCH { get; set; }
        public string APRIL { get; set; }
        public string MAY { get; set; }
        public string JUNE { get; set; }
        public string JULY { get; set; }
        public string AUGUST { get; set; }
        public string SEPTEMBER { get; set; }
        public string OCTOBER { get; set; }
        public string NOVEMBER { get; set; }
        public string DECEMBER { get; set; }
        public string MONTH_TOTAL { get; set; }

        public string JANUARY_INVOICE { get; set; }
        public string FEBRUARY_INVOICE { get; set; }
        public string MARCH_INVOICE { get; set; }
        public string APRIL_INVOICE { get; set; }
        public string MAY_INVOICE { get; set; }
        public string JUNE_INVOICE { get; set; }
        public string JULY_INVOICE { get; set; }
        public string AUGUST_INVOICE { get; set; }
        public string SEPTEMBER_INVOICE { get; set; }
        public string OCTOBER_INVOICE { get; set; }
        public string NOVEMBER_INVOICE { get; set; }
        public string DECEMBER_INVOICE { get; set; }
        public string MONTH_TOTAL_INVOICE { get; set; }
        public string Agree_Start_Date { get; set; }
        public string Expiry_Date { get; set; }


    }
}
