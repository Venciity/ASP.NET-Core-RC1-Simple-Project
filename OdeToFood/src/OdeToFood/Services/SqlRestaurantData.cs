using System.Collections.Generic;
using OdeToFood.Entities;
using System.Linq;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            this.context = context;
        }

        private OdeToFoodDbContext context;

        public int Commit()
        {
            return this.context.SaveChanges();
        }

        public void Add(Restaurant restaurant)
        {
            this.context.Add(restaurant);
        }

        public Restaurant Get(int id)
        {
            return this.context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return this.context.Restaurants.ToList();
        }
    }
}
