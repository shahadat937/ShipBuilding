using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.TreeView
{
    public class FileCrafts : FileListByTree_Result
    {
           public List<FileCrafts> Nodes { get; set; }
           public FileCrafts()
        {
            Nodes = new List<FileCrafts>();
        }
    }
}
