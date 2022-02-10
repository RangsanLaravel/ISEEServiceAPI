using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report.subreport
{
    public partial class part_rpt
    {
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private TopMarginBand topMarginBand2;
        private DetailBand detailBand2;
        private BottomMarginBand bottomMarginBand2;
        private BottomMarginBand bottomMarginBand1;

        public part_rpt(List<tbt_job_part> part)
        {
            InitializeComponent();
            this.DataSource = part;
        }

       
    }
}
