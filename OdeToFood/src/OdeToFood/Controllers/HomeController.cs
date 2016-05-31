using Microsoft.AspNet.Mvc;
using OdeToFood.Entities;
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

        [HttpGet]
        public ViewResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var restaurant = new Restaurant();
                restaurant.Name = model.Name;
                restaurant.Cuisine = model.Cuisine;

                this.restaurantData.Add(restaurant);

                return this.RedirectToAction("Details", new { id = restaurant.Id });
            }

            return this.View();
        }

        public IActionResult Details(int id)
        {
            var model = this.restaurantData.Get(id);
            if (model == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}
