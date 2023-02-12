using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class rpt_tbm_employee : DevExpress.XtraReports.UI.XtraReport
    {
        public rpt_tbm_employee(List<tbm_employee> data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }
    }
}
