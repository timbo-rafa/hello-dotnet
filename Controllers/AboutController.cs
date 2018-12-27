using Microsoft.AspNetCore.Mvc;

namespace OdeToFood2.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        //[Route("")] //Default route
        public string Phone()
        {
            return "+1 647 647 6477";
        }

        //[Route("[action]")]
        public string Address()
        {
            return "CAN";
        }
    }
}
