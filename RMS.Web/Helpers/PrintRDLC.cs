using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Net.Mail;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace RMS.Web.Helpers
{
    public class RDLC : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        enum PrintOrientation
        {
            Landscape,
            Portrait
        };
        enum rptFormat {Excel, PDF,Image};
        public RDLC()
        {
        }

        public void SendMail(LocalReport rpt, string filePath,string Sender,string Reciver,string Subject,string Body)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            try
            {
                byte[] bytes = rpt.Render("PDF", null, out mimeType,
                    out encoding, out extension, out streamids, out warnings);

                // Create a file stream and write the report to it

                MemoryStream s = new MemoryStream(bytes);
                s.Seek(0, SeekOrigin.Begin);
                Attachment a = new Attachment(s, filePath);

                MailMessage message = new MailMessage(Sender,Reciver,Subject,Body);
                message.Attachments.Add(a);
                message.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient();
                System.Net.NetworkCredential sc = new System.Net.NetworkCredential("nasimul.gani@infinityexpress.biz", "Apel@Mina");
                client.Host = "smtp.infinityexpress.biz";
                //client.EnableSsl = true;
                client.Port = 25;
                client.Timeout = 180000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials =false ;
                client.Credentials = sc;
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Export(LocalReport rpt, string filePath)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
        
            try
            {
                byte[] bytes = rpt.Render("PDF", null, out mimeType,
                    out encoding, out extension, out streamids, out warnings);

                // Create a file stream and write the report to it
                using (FileStream stream = File.OpenWrite(filePath))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Export2File(LocalReport rpt, string filePath, string output)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            try
            {

                // Create a file stream and write the report to it
                string FileFormat="";
                switch (output)
                {
                    case "xls":
                        filePath = Path.ChangeExtension(filePath, "xls");
                        FileFormat = "Excel";
                        break;
                    case "jpg":
                        filePath = Path.ChangeExtension(filePath, "jpg");
                        FileFormat = "Image";
                        break;
                    case "doc":
                        filePath = Path.ChangeExtension(filePath, "doc");
                        FileFormat = "Word";
                        break;
                    case "pdf":
                        filePath = Path.ChangeExtension(filePath, "pdf");
                        FileFormat = "PDF";
                        break;
                    case "csv":
                        filePath = Path.ChangeExtension(filePath, "csv");
                        FileFormat = "CSV";
                        break;
                }

                byte[] bytes = rpt.Render(FileFormat, null, out mimeType,
                    out encoding, out extension, out streamids, out warnings);

                using (FileStream stream = File.OpenWrite(filePath))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.


        protected Stream CreateStream(string name, string fileNameExtension, Encoding encoding,  string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        protected void ExportToStream(LocalReport report)
        {
            try
            {

                string deviceInfo = "";
                //if (orientation==PrintOrientation.Portrait)
                deviceInfo =
                    @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.27in</PageWidth>
                <PageHeight>11.69in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
                //else
//           deviceInfo= @"<DeviceInfo>
//                <OutputFormat>EMF</OutputFormat>
//                <PageWidth>11.69in</PageWidth>
//                <PageHeight>8.27in</PageHeight>
//                <MarginTop>0.25in</MarginTop>
//                <MarginLeft>0.25in</MarginLeft>
//                <MarginRight>0.25in</MarginRight>
//                <MarginBottom>0.25in</MarginBottom>
//            </DeviceInfo>";

                Warning[] warnings;
                m_streams = new List<Stream>();
                report.Render("Image", deviceInfo, CreateStream,
                    out warnings);
                foreach (Stream stream in m_streams)
                    stream.Position = 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
                Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
                System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Print(LocalReport rptFile)
        {
            try
            {

                ExportToStream(rptFile);
                if (m_streams == null || m_streams.Count == 0)
                    throw new Exception("Error: no stream to print.");
                PrintDocument printDoc = new PrintDocument();
                if (!printDoc.PrinterSettings.IsValid)
                {
                    throw new Exception("Error: cannot find the default printer.");
                }
                else
                {
                    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                    m_currentPageIndex = 0;
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        //private void Run()
        //{
        //    LocalReport report = new LocalReport();
        //    report.ReportPath = @"..\..\Report.rdlc";
        //    report.DataSources.Add(
        //       new ReportDataSource("Sales", LoadSalesData()));
       
        //    Export(report);
        //    Print();
        //}

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

    
    }
}