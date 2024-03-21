using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;

    public partial class FileUploadForm
    {
        public long FileUploadFormId { get; set; }
        public long ProjectId { get; set; }
        public string TEA { get; set; }
        public string FATSchedule { get; set; }
        public string FATProcedure { get; set; }
        public string FATReport { get; set; }
        public string HATSAT { get; set; }
        public string PIT { get; set; }
        public string LetterGo { get; set; }
        public string ApprovedGo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual Demand Demand { get; set; }

        //-----------
        public string ProjectName { get; set; }
        public string TE_APDF { get; set; }
        public string FATSchedulePDF { get; set; }
        public string FATProcedurePDF { get; set; }
        public string FATReportPDF { get; set; }
        public string HATSATPDF { get; set; }
        public string PITPDF { get; set; }
        public string LETTERPDF { get; set; }
        public string APPROVEPDF { get; set; }
    }
}
