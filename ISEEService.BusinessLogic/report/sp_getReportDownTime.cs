using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class sp_getReportDownTime
    {
        public sp_getReportDownTime(List<rpt_downtime> data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }
    }
}
