using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class PaymentScheduleViewModel
    {
        public List<PaymentSchedule> PaymentSchedules { get; set; }
        public PaymentSchedule PaymentSchedule { get; set; }
        public PaymentScheduleViewModel()
        {
            PaymentSchedules = new List<PaymentSchedule>();
            PaymentSchedule =new PaymentSchedule();
        }
        public string SearcKey { get; set; }
    }
}