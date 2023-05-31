using System;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class summary_stock_list
    {
        public summary_stock_list(summary_stock_list_condition condition)
        {
            InitializeComponent();
            this.objectDataSource2.DataSource = condition;
        }

        private void summary_stock_list_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (((summary_stock_list_condition)this.objectDataSource2.DataSource).report_type == "EXCEL")
            {
                this.tableCell21.Visible = !this.tableCell21.Visible;
                this.tableCell42.Visible = !this.tableCell42.Visible;
                this.tableCell41.Visible = !this.tableCell41.Visible;

                this.tableCell25.Visible = !this.tableCell25.Visible;
                this.tableCell43.Visible = !this.tableCell43.Visible;
                this.tableCell40.Visible = !this.tableCell40.Visible;

                this.tableCell11.Visible = !this.tableCell11.Visible;
                this.tableCell14.Visible = !this.tableCell14.Visible;
                this.tableCell13.Visible = !this.tableCell13.Visible;
            }
        }
    }
}
