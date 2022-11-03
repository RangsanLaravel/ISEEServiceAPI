using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class sp_getReportPMP
    {
        public sp_getReportPMP(List<ReportPPM> data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }
    }
}
