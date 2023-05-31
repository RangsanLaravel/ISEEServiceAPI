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

        private void summary_job_list_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var job = (summary_job_list_condition)this.objectDataSource1.DataSource;
            if(job is not null && job.summary_job_list is not null)
            {
                this.tableCell30.Text = job.summary_job_list.Count.ToString();

            }
        }
    }
}
