using OdeToFood2.Models;
using System.Collections.Generic;

namespace OdeToFood2.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public string CurrentMessage { get; set; }
    }
}
