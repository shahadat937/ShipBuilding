using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class FieldValueBack
    {
        public int id { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
