using Microsoft.Extensions.Configuration;

namespace OdeToFood.Services
{
    public class Greeter : IGreeter
    {
        private string greeting;

        public Greeter(IConfiguration configuration)
        {
            this.greeting = configuration["greetingMessage"];
        }

        public string GetGreeting()
        {
            return greeting;
        }
    }
}
