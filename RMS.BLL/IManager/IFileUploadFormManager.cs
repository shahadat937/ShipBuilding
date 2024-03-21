using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RMS.BLL.IManager
{
    public interface IFileUploadFormManager
    {
        List<FileUploadForm> GetAll();

        string SaveFileUploadForms(FileUploadForm fileUploadForm);

        FileUploadForm GetFileUploadForm(long fileUploadFormId);

        void DeleteFileUploadForm(long fileUploadFormId);

        FileUploadForm GetFileUploadFormByID(long p);
    }
}
