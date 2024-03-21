using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class FormInfo
    {
        public FormInfo()
        {
            this.FileBackUps = new HashSet<FileBackUp>();
        }
        public long FormId { get; set; }
        public string FormName { get; set; }
        public string FormURL { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        //  Form Serial for view
        public string FormSerial { get; set; }
        public bool IsComplete { get; set; }
        public bool IsSkip { get; set; }
        public string FlowIdentity { get; set; }
        //-----------
        [DisplayName("Project Name")]
        public string ProjectName { set; get; }
        public string FileName { set; get; }
        public string SearchKey { set; get; }
        public string Message { set; get; }
        public string AttachedDocument { set; get; }

        public virtual ICollection<FileBackUp> FileBackUps { get; set; }
    }
}
