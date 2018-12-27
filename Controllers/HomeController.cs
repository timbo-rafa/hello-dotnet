using Microsoft.AspNetCore.Mvc;
using OdeToFood2.Services;
using OdeToFood2.ViewModels;

namespace OdeToFood2.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData,
                               IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        //public string Index()
        public IActionResult Index()
        {
            //var model = _restaurantData.GetAll();
            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();
            return View(model);
            //return new ObjectResult(model);
            //return Content("Hello from the HomeController!");
        }

        public IActionResult Details(int id)
        {
            //return Content(id.ToString());
            var model = _restaurantData.Get(id);

            if (model == null)
            {
                //browser
                //return View("NotFound");
                // APIs
                //return NotFound();

                //redirect
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
