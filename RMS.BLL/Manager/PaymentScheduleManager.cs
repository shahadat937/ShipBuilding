using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
   public class PaymentScheduleManager : IPaymentScheduleManager
   {
       private readonly IPaymentScheduleRepository _paymentScheduleRepository;
       public PaymentScheduleManager(IPaymentScheduleRepository paymentScheduleRepository)
       {
           _paymentScheduleRepository = paymentScheduleRepository;
       }

       public List<PaymentSchedule> GetAll()
       {
           return _paymentScheduleRepository.All().ToList();
       }
   }
}
