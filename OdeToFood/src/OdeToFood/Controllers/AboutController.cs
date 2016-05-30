using Microsoft.AspNet.Mvc;

namespace OdeToFood.Controllers
{
    [Route("[controller]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "+359 883444444";
        }

        [Route("[action]")]
        public string Country()
        {
            return "Bulgaria";
        }
    }
}
