using DevExpress.XtraReports.UI;
using ISEEService.DataContract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ISEEService.BusinessLogic.report
{
    public partial class rpt_tbm_customer : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_tbm_customer(List<tbm_customer> data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }
    }
}
