using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using RMS.Utility;

namespace RMS.Web.Reports
{
    public partial class TreeReportViwer : Page
    {
        private string ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        private DataSet ds = new DataSet();
        private Dictionary<string, string> paramertes = new Dictionary<string, string>();
        private DataRow dr = null;
        string slNo="";
        private int RecNo;
        private string txtSQL = "";
        string ReportName, ReportPath, QueryName, xmlTable; // , Para1, Para2, Para3, Para4;
        bool IsEventLog;
        private string para1, para2, para3, para4, para2Value, para3Value, para4Value;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                rptViwer.ZoomMode = ZoomMode.PageWidth;
                paramertes = Session["paramertes"] as Dictionary<string, string> ?? new Dictionary<string, string>();
                slNo = Session["slNo"].ToString();
                RenderReprot();
                if (paramertes.Any())
                {
                    Session["paramertes"] = null;
                }
            }
        }

        private void RenderReprot()
        {
            string queryType = PortalContext.CurrentUser.QryType;
            string userBranchCode = "";

            switch (queryType)
            {
                case "H":
                    userBranchCode = PortalContext.CurrentUser.BankCode;
                    break;
                case "R":
                    userBranchCode = PortalContext.CurrentUser.BranchCode;
                    break;
                case "I":
                    userBranchCode = PortalContext.CurrentUser.BranchCode;
                    break;
                case "Z":
                    userBranchCode = PortalContext.CurrentUser.BranchCode;
                    break;
                case "S":
                    userBranchCode = PortalContext.CurrentUser.BranchCode;
                    break;
            }
            var conn = new SqlConnection(ConnString);
            var cmd = new SqlCommand();
            txtSQL = "Select * From Reporting Where SLNO='" + slNo + "'";
            conn.ConnectionString = ConnString;
            conn.Open();
            var adp11 = new SqlDataAdapter(txtSQL, conn);
            RecNo = adp11.Fill(ds, "DataTable");
            if (RecNo >= 0)
            {

                dr = ds.Tables[0].Rows[0];
                ReportPath = dr["ReportPath"].ToString();
                QueryName = dr["QryName"].ToString();
                xmlTable = dr["xmlTableName"].ToString();
            }
            ReportPath = ReportPath.Replace(".rdlc", "");
            ReportPath= ReportPath.Replace(".", "/");
            ReportPath = "~/"+ReportPath + ".rdlc";
            var cmd1 = new SqlCommand(QueryName, conn);
            var parm0 = new SqlParameter("UserBranchCode", userBranchCode);
            cmd1.Parameters.Add(parm0);
            foreach (var paramerte in paramertes)
            {
                var parm = new SqlParameter();
                if (paramerte.Key.ToLower().Contains("date"))
                {
                    parm.ParameterName = paramerte.Key;
                    parm.Value = Convert.ToDateTime(paramerte.Value).ToString("dd MMM yyyy");
                }
                else
                {
                    parm.ParameterName = paramerte.Key;
                    parm.Value = paramerte.Value;
                }
                cmd1.Parameters.Add(parm);
            }

            if (QueryName.Trim().Length != 0)
            {
                var rDs = new DataSet();
                var adp = new SqlDataAdapter(cmd1);
                adp.Fill(rDs, xmlTable);
                rptViwer.Reset();
                rptViwer.ProcessingMode = ProcessingMode.Local;
                rptViwer.LocalReport.ReportPath = Server.MapPath(ReportPath);
                rptViwer.LocalReport.EnableExternalImages = true;
                rptViwer.LocalReport.DataSources.Add(new ReportDataSource(xmlTable, rDs.Tables[0]));
            }
        }
    }
}