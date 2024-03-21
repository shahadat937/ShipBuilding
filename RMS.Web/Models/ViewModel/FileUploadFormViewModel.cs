using RMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class FileUploadFormViewModel
    {

        public FileUploadFormViewModel()
        {
            FileUploadForm = new FileUploadForm();
            FileUploadForms = new List<FileUploadForm>();
        }
        public FileUploadForm FileUploadForm { set; get; }
        public List<FileUploadForm> FileUploadForms { set; get; }
        public List<Demand> Projects { set; get; }
        public IEnumerable<SelectListItem> SelectedProjects
        {
            get
            {
                return new SelectList(Projects, "DemandId", "FileNo");
            }
        }
        public string Message { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase TE_AUpload { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FATScheduleUpload { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FATProcedureUpload { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FATReportUpload { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase HATSATUpload { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PITUpload { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase LettergoUpload { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ApproveltrUpload { set; get; }
    }
}