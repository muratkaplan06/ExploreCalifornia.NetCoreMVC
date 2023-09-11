using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    
    public class BlogController : Controller
    {
        private BlogDbContext _db;

        public BlogController(BlogDbContext db)
        {
            _db = db;
        }

        [Route("")]
        public IActionResult Index()
        {
            var posts = _db.Posts.OrderByDescending(x => x.Posted).Take(5).ToArray();

            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year,int month,string key)
        {

            var post=_db.Posts.FirstOrDefault(x => x.Key == key);
            return View(post);
            //var post=new Post
            //{
            //    Title = "My blog post",
            //    Posted = DateTime.UtcNow,
            //    Author = "Mehmet",
            //    Body = "This is a great blog post, don't you think?"
            //};

            //return new ContentResult { Content = string.Format("year:{0}, month:{1} key:{2}", year, month,key)};
            //ViewBag.Title = "My blog post";
            //ViewBag.Posted = DateTime.Now;
            //ViewBag.Author = "Mehmet";
            //ViewBag.Body = "This is a great blog post, don't you think?";
           
        }

        [HttpGet,Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("create")]
        public IActionResult Create(Post post)
        {
            //if (!ModelState.IsValid)
            //    return View();

            //post.Author = User.Identity.Name;
            post.Author = "Mehmet";
            post.Posted = DateTime.UtcNow;

            _db.Posts.Add(post);
            _db.SaveChanges();

            return View();
        }
    }
}
