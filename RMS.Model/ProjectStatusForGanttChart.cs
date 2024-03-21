using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class ProjectStatusForGanttChart
    {
        public string TaskName { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }

       // For Pic Chart 
        public int TotalProject { get; set; }
        public int OngoingProject { get; set; }
        public int CompleteProject { get; set; }
      
    }
}
