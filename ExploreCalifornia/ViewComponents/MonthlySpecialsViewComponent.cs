using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExploreCalifornia.ViewComponents
{
    [ViewComponent]
    public class MonthlySpecialsViewComponent:ViewComponent
    {
        private readonly BlogDbContext _context;

        public MonthlySpecialsViewComponent(BlogDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var specials = _context.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}
