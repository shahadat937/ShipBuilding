using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Utility
{
   public class SelectionList
    {
        public long Value { get; set; }
        public string Text { get; set; }
        public Nullable<bool> SkipStatus { get; set; }
        public Nullable<bool> CompleteStatus { get; set; } 
    }
}
