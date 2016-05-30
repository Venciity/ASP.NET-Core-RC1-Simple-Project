using System.Collections.Generic;
using OdeToFood.Entities;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        public InMemoryRestaurantData()
        {
            this.restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Marios" },
                new Restaurant { Id = 2, Name = "What we eat" },
                new Restaurant { Id = 3, Name = "Good food with good friends" }
            };
        }

        private List<Restaurant> restaurants;

        public IEnumerable<Restaurant> GetAll()
        {
            return this.restaurants;
        }
    }
}
