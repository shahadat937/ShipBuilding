using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class PaymentScheduleController : BaseController
    {
        private readonly IPaymentScheduleManager _paymentScheduleManager;

        public PaymentScheduleController(IPaymentScheduleManager paymentScheduleManager)
        {
            _paymentScheduleManager = paymentScheduleManager;
        }
        public ActionResult Index(PaymentScheduleViewModel model)
        {
            model.PaymentSchedules = _paymentScheduleManager.GetAll();
            return View(model);
        }

        public ActionResult Test(FlowSetupViewModel model)
        {
             model.DepId = new[] {"test1","Test2"};
            return View(model);
        }

        public ActionResult Save(FlowSetupViewModel model)
        {
           
            return View();
        }

        public ActionResult DragDrop(FlowSetupViewModel model)
        {
            model.DepId = new[] { "test1", "Test2" };
            return View(model);
        }
    }
}