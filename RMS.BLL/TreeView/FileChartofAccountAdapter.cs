using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.TreeView
{
    public class FileChartofAccountAdapter : IFileTarget
    {

        private readonly List<FileListByTree_Result> _fileTreeLists;

        public FileChartofAccountAdapter(List<FileListByTree_Result> fileTreeLists)
        {
            _fileTreeLists = fileTreeLists;
        }
        public List<FileCrafts> GetTreeView()
        {
            return GetChartofAccounts();
        }

        private List<FileCrafts> GetChartofAccounts()
        {
            var chartofAccounts = new List<FileCrafts>();
            foreach (FileListByTree_Result filelist in _fileTreeLists)
            {
                if (filelist != null && filelist.ParentCode == 0)
                {
                    chartofAccounts.Add(ConvertChartofAcount(filelist));
                }
                else
                {
                    foreach (FileCrafts tree in chartofAccounts)
                    {
                        if (filelist != null && tree.FileId == filelist.ParentCode)
                        {
                            tree.Nodes.Add(ConvertChartofAcount(filelist));
                        }
                        else
                        {
                            if (tree.Nodes.Any())
                            {
                                foreach (FileCrafts firstNode in tree.Nodes)
                                {
                                    if (filelist != null && firstNode.FileId == filelist.ParentCode)
                                    {
                                        firstNode.Nodes.Add(ConvertChartofAcount(filelist));
                                    }
                                    else if (firstNode.Nodes.Any())
                                    {
                                        foreach (FileCrafts secendNode in firstNode.Nodes)
                                        {
                                            if (filelist != null && secendNode.FileId == filelist.ParentCode)
                                            {
                                                secendNode.Nodes.Add(ConvertChartofAcount(filelist));
                                            }
                                            if (secendNode.Nodes.Any())
                                            {
                                                foreach (FileCrafts thirdNode in secendNode.Nodes)
                                                {
                                                    if (filelist != null && thirdNode.FileId == filelist.ParentCode)
                                                    {
                                                        thirdNode.Nodes.Add(ConvertChartofAcount(filelist));
                                                    }

                                                }
                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            return chartofAccounts;
        }
        private FileCrafts ConvertChartofAcount(FileListByTree_Result control)
        {
            return control != null
                ? new FileCrafts()
                {
                    ProjectId = control.ProjectId,
                    ParentCode = control.ParentCode,
                    FileId = control.FileId,
                    FormId = control.FormId,
                    FileName= control.FileName,
                    FormURL = control.FormURL,
                    EntryStatus = control.EntryStatus,
                    Level = control.Level,
                    FlowSerial = control.FlowSerial,
                    IsSkip = control.IsSkip,
                    taskSerial = control.taskSerial,
                    status = Convert.ToBoolean(!control.EntryStatus.ToLower().Contains("true") ? false : true),
                    Nodes = new List<FileCrafts>(),
                    
                }
                : new FileCrafts();
        }
    }
}
