using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class FileStore
    {
        public int id { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
    }
}
