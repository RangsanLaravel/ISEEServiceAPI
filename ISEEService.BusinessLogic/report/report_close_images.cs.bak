using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using ISEEService.DataContract;
using ITUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace ISEEService.BusinessLogic.report
{
    public partial class report_close_images : DevExpress.XtraReports.UI.XtraReport
    {
        public report_close_images(List<rpt_image> img)
        {
            InitializeComponent();
            this.objectDataSource1.DataSource = img;
        }

        private Image GetImage(byte[] image)
        {
            MemoryStream mem = new MemoryStream(image);
            Bitmap bmp = new Bitmap(mem);
            Image img = bmp;
            return img;
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //var currRow = (rpt_image)GetCurrentRow();
            //if (CurrentRowIndex % 2 == 0)
            //{                
            //   this. xrPictureBox1.ImageSource = new ImageSource(GetImage(currRow.Files.FileData));
            //}
            //else
            //{
            //    this.xrPictureBox2.ImageSource = new ImageSource(GetImage(currRow.Files.FileData));
            //}
        }
    }
}
