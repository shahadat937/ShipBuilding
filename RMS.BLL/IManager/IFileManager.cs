using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public  interface IFileManager
    {
      int CreateFile(File file);
      List<File> GetFilesByProjectId(long id);
      List<File> GetFiles();
      List<File> GetFilesByFileName(string FileName);
      List<File> GetFilesByFileCode(string FileCode);
      File GetFilesByFileId(long FileId);
    }
}
