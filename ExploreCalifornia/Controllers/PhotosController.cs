using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.Controllers
{
    public class PhotosController : Controller
    {
        public IActionResult Index()
        {
           return new ContentResult { Content = "hello from PhotosController" };
        }
    }
}
