using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using OdeToFood.Entities;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        private IRestaurantData restaurantData;

        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = new HomePageViewModel();
            model.Restaurants = this.restaurantData.GetAll();
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
                this.restaurantData.Commit();

                return this.RedirectToAction("Details", new { id = restaurant.Id });
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.restaurantData.Get(id);
            if (model == null)
            {
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, RestaurantEditViewModel input)
        {
            var restaurant = this.restaurantData.Get(id);
            if (restaurant != null && ModelState.IsValid)
            {
                restaurant.Name = input.Name;
                restaurant.Cuisine = input.Cuisine;
                this.restaurantData.Commit();

                return this.RedirectToAction("Details", new { id = restaurant.Id });
            }

            return this.View(restaurant);
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
