using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class report_movement_sparepart
    {
        public report_movement_sparepart(List<movement_sparepart> data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }
    }
}
