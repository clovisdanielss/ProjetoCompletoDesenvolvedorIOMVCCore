using Dev.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dev.App.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotifier _notifier;
        public BaseController(INotifier notifier)
        {
            _notifier=notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

    }
}
