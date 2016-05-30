using Microsoft.AspNet.Mvc;

namespace OdeToFood.Controllers
{
    [Route("about")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "+359 883444444";
        }

        [Route("country")]
        public string Country()
        {
            return "Bulgaria";
        }
    }
}
