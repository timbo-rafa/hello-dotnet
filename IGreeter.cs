using Microsoft.Extensions.Configuration;

namespace OdeToFood2
{
    public interface IGreeter
    {
        string GetMessageOfTheDay();
    }

    public class Greeter : IGreeter
    {
        private IConfiguration _configuration;

        public Greeter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetMessageOfTheDay()
        {
            return "Greetings from Greeter class!";
        }
    }
}