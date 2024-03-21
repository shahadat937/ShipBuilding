using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.TreeView
{
   public interface IFileTarget
    {
       List<FileCrafts> GetTreeView();
    }
}
