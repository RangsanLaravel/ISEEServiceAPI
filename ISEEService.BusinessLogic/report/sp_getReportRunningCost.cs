using System;
using DevExpress.XtraReports.UI;
using System.Linq;
using System.Collections.Generic;

namespace ISEEService.BusinessLogic.report
{
    public partial class sp_getReportRunningCost
    {
        public sp_getReportRunningCost(List<DataContract.sp_getReportRunningCost> data )
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }
    }
}
