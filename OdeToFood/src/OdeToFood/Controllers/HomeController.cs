using Microsoft.AspNet.Mvc;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IRestaurantData restaurantData, IGreeter gretter)
        {
            this.restaurantData = restaurantData;
            this.greeter = gretter;
        }

        private IRestaurantData restaurantData;
        private IGreeter greeter;

        public ViewResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = this.restaurantData.GetAll();
            model.CurrentGreeting = this.greeter.GetGreeting();
            return this.View(model);
        }
    }
}
