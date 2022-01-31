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
            this.objectDataSource1.DataSource = condition;
        }
    }
}
