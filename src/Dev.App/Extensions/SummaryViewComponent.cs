using Dev.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dev.App.Extensions
{
    public class SummaryViewComponent: ViewComponent
    {
        private readonly INotifier _notifier;
        public SummaryViewComponent(INotifier notifier)
        {
            _notifier=notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notifier.GetNotifications());
            notifications.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Message));
            return View();
        }
    }
}
