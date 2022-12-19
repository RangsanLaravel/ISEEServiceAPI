using DevExpress.XtraReports.UI;
using ISEEService.DataContract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ISEEService.BusinessLogic.report
{
    public partial class rpt_sp_getReportProductive_time : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_sp_getReportProductive_time(List<sp_getReportProductive_time> data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource= data;
        }
    }
}
