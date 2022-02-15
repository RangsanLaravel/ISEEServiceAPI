using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report.subreport
{
    public partial class part_rpt
    {
        

        public part_rpt(List<tbt_job_part> part)
        {
            InitializeComponent();
            this.DataSource = part;
        }

       
    }
}
