using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class NoticeViewModel:AppCommonInfo
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string SearchKey { get; set; }
        public Notice Notice { get; set; }
        public List<Notice> Notices { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public NoticeViewModel()
        {
            Notice = new Notice();
            Notices=new List<Notice>();
        }
    }
}