using Microsoft.AspNet.Mvc;
using OdeToFood.Models;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public ObjectResult Index()
        {
            var model = new Restaurant() { Id = 1, Name = "Marios" };
            return new ObjectResult(model);
        }
    }
}
