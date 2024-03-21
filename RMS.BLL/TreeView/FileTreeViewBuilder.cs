using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.TreeView
{
   public class FileTreeViewBuilder
    {
         private readonly IFileTarget _chartofAccount;

        public FileTreeViewBuilder(IFileTarget chartofAccount)
        {
                _chartofAccount = chartofAccount;
        }
        public List<FileCrafts> GetChartofAccount()
        {
            return _chartofAccount.GetTreeView();
        } 
    }
}
