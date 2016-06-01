using System.Collections.Generic;
using OdeToFood.Entities;
using System.Linq;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Marios" },
                new Restaurant { Id = 2, Name = "What we eat" },
                new Restaurant { Id = 3, Name = "Good food with good friends" }
            };
        }

        private static List<Restaurant> restaurants;

        public int Commit()
        {
            return 0;
        }

        public void Add(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(restaurant);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return restaurants;
        }

        public Restaurant Get(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }
    }
}
