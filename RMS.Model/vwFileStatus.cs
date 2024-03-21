using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class vwFileStatus
    {
       public string DepartmentName { get; set; }
       public int FileStatus { get; set; }
       public int AnyTrue { get; set; }
       public Nullable<bool> IsSkip { get; set; } 
    }
}
