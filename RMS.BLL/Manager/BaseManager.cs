using System.Web;
using RMS.DAL;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class BaseManager
    {
        internal DSBDBEntities Context;
        public ResponseModel ResponseModel { get; set; }

         public static BaseManager Instance
        {
            get
            {
                var manager = (BaseManager)HttpContext.Current.Items["DatabaseManager"];
                if (manager == null)
                {
                    manager = new BaseManager();
                    HttpContext.Current.Items["DatabaseManager"] = manager;
                }
                return manager;
            }
        }

        public BaseManager()
        {
            ResponseModel=new ResponseModel();
            Context = new DSBDBEntities();
        }
    }
}
