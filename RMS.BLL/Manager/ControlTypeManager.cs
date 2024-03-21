using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL;
using RMS.Model;


namespace RMS.BLL.Manager
{
    public class ControlTypeManager : IControlTypeManager
    {
        private readonly IControlTypeRepository _controlTypeRepository;
        public ControlTypeManager(IControlTypeRepository controlTypeRepository)
        {
            _controlTypeRepository = controlTypeRepository;
        }

        public List<ControlType> GetControlType()
        {
            List<ControlType> controlTypes = _controlTypeRepository.All().ToList();
            return controlTypes;
        }

       
    }
}
