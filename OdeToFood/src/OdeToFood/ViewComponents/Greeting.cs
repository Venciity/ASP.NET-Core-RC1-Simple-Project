using Microsoft.AspNet.Mvc;
using OdeToFood.Services;

namespace OdeToFood.ViewComponents
{
    public class Greeting : ViewComponent
    {
        public Greeting(IGreeter greeter)
        {
            this.greeter = greeter;
        }

        private IGreeter greeter;

        public IViewComponentResult Invoke()
        {
            var model = this.greeter.GetGreeting();
            return this.View("Default", model);
        }
    }
}
