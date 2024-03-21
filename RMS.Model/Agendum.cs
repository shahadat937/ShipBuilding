using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class Agendum
    {
        public long Id { get; set; }
        public string FileNo { get; set; }
        public string Subject { get; set; }
        public string Heading { get; set; }
        public string Formula { get; set; }
        public Nullable<long> Category { get; set; }
        public Nullable<long> Status { get; set; }
        public string FileUrl { get; set; }
        public Nullable<System.DateTime> StatusDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
    }
}
