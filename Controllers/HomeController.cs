using Microsoft.AspNetCore.Mvc;
using OdeToFood2.Services;

namespace OdeToFood2.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        //public string Index()
        public IActionResult Index()
        {
            var model = _restaurantData.GetAll();
            return View(model);
            //return new ObjectResult(model);
            //return Content("Hello from the HomeController!");
        }
    }
}
