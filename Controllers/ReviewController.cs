using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieGallery.Data;
using MovieGallery.Models;
using System.Security.Claims;

namespace MovieGallery.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int movieId)
        {
            ViewBag.MovieId = movieId;
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == movieId);
            ViewBag.movie = movie;
            var reviews = _context.Reviews
                .Include(r => r.User)
                .Where(r => r.MovieId == movieId)
                .ToList();
            return View(reviews);
        }

        public IActionResult Create(int movieId)
        {
            var movie = _context.Movies.Find(movieId);
            if (movie == null) return NotFound();

            ViewBag.MovieName = movie.Name;
            return View(new Review { MovieId = movieId });
        }

        [HttpPost]
        public IActionResult Create(Review review)
        {
            //Console.WriteLine($"UserId: {userId}");
            if (ModelState.IsValid)
            {
                review.CreatedAt = DateTime.Now;
                //review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user ID
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return RedirectToAction("Index", "Review", new {movieId = review.MovieId});
            }
            return View(review);
        }

    }
}
