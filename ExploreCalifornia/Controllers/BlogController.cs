using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("blog/{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year,int month,string key)
        {
            var post=new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Mehmet",
                Body = "This is a great blog post, don't you think?"
            };

            //return new ContentResult { Content = string.Format("year:{0}, month:{1} key:{2}", year, month,key)};
            //ViewBag.Title = "My blog post";
            //ViewBag.Posted = DateTime.Now;
            //ViewBag.Author = "Mehmet";
            //ViewBag.Body = "This is a great blog post, don't you think?";
            return View(post);
        }
    }
}
