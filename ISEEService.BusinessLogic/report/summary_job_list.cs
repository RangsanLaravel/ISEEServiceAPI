using System;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class summary_job_list
    {
        public summary_job_list(summary_job_list_condition data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }
    }
}
