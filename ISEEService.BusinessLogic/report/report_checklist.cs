using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class report_checklist
    {
        public report_checklist(check_list_header_rpt header)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = header;          
        }
    }
}
