using Microsoft.AspNet.Mvc;
using OdeToFood.Services;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        private IRestaurantData restaurantData;

        public ViewResult Index()
        {
            var model = this.restaurantData.GetAll();
            return this.View(model);
        }
    }
}
