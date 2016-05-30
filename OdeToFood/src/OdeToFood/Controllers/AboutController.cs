using Microsoft.AspNet.Mvc;

namespace OdeToFood.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController : Controller
    {
        public IActionResult Phone()
        {
            return this.Content("+359 883444444");
        }

        public IActionResult Country()
        {
            return this.Content("Bulgaria");
        }
    }
}
