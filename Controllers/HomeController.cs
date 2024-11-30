using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieGallery.Data;
using MovieGallery.Models;

namespace MovieGallery.Controllers
{
    [Authorize(Roles ="Admin, User")]
    public class HomeController : Controller
    {
        private ApplicationDbContext context { get; set; }
        //private readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var movies = context.Movies.Include(m => m.Genre).OrderBy(m => m.Name).ToList();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var isAdmin = User.IsInRole("Admin");

            var notifications = new List<Notification>();
            if(!isAdmin)
            {
                notifications = context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
            }

            ViewBag.Notifications = notifications;
            return View(movies);
        }

        // Mark notification as read
        public IActionResult MarkAsRead(int notificationId)
        {
            var notification = context.Notifications.Find(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        /* Removing Iaction methods
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
    }
}
