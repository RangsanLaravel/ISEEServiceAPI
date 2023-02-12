using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using ISEEService.DataContract;
using System.Collections.Generic;

namespace ISEEService.BusinessLogic.report
{
    public partial class rpt_tmb_vehicle : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_tmb_vehicle(List<tbm_vehicle> data)
        {
            InitializeComponent();
            this.objectDataSource2.DataSource = data;
        }
    }
}
