using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.DAL;

namespace RMS.BLL.Manager
{
    public class StatusManager : IStatusManager
    {
        private readonly IStatusRepository _statusRepository;
        DSBDBEntities db = new DSBDBEntities();
        public StatusManager(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public List<Status> GetStatus()
        {
            List<Status> statuses = _statusRepository.All().ToList();
            return statuses;
        }
  
    }
}
