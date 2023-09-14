using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        
        private BlogDbContext _context;

        public BlogController(BlogDbContext context)
        {
            _context=context;
            
        }

        [Route("")]
        public IActionResult Index(int page=0)
        {
            var pageSize = 2;
            var totalPosts = _context.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts =
                _context.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year,int month,string key)
        {

            var post=_context.Posts.FirstOrDefault(x => x.Key == key);
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
        [Authorize]
        [HttpGet,Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost, Route("create")]
        public IActionResult Create(Post post)
        {
            //if (!ModelState.IsValid)
            //    return View();

            post.Author = User.Identity.Name;
            post.Posted = DateTime.UtcNow;

            _context.Posts.Add(post);
            _context.SaveChanges();

            return View();
        }
    }
}
