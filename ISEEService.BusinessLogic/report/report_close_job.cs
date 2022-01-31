using System;
using System.Drawing;
using System.IO;
using DevExpress.XtraReports.UI;
using ISEEService.BusinessLogic.report.subreport;
using ISEEService.DataContract;

namespace ISEEService.BusinessLogic.report
{
    public partial class report_close_job
    {
        public report_close_job(close_job  data)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = data;
        }

       
        private void report_close_job_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var data = (close_job)this.objectDataSource1.DataSource;
            part_rpt prt = new part_rpt(data.job_parts);
            subreport1.ReportSource = prt;
            if (!string.IsNullOrEmpty(data.rptsig))
            {
                Image img = ByteArrayToImage(Convert.FromBase64String(data.rptsig));
                pictureBox2.Image = img;
            }
            
        }
        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
