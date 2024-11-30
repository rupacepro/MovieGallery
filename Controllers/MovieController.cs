using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieGallery.Data;
using MovieGallery.Models;

namespace MovieGallery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovieController : Controller
    {
        private ApplicationDbContext context { get; set; }

        public MovieController(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        [HttpGet]

        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            return View("Edit", new Movie());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
            var movie = context.Movies.Find(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            bool newMovie = false;
            if (ModelState.IsValid)
            {
                if (movie.MovieId == 0) {
                    context.Movies.Add(movie);
                    newMovie = true;
                }
                else
                    context.Movies.Update(movie);
                context.SaveChanges();

                //crate a notification for all users
                var users = context.Users.ToList();
                foreach(var user in users)
                {
                    var notification = new Notification
                    {
                        Message = newMovie ?
                        $"A new movie '{movie.Name}' has been added to the collection." 
                        : $"The movie '{movie.Name}' has been updated.",
                        CreatedAt = DateTime.Now,
                        UserId = user.Id,
                        IsRead = false
                    };
                    context.Notifications.Add(notification);
                }
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (movie.MovieId == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.Genres.OrderBy(g => g.Name).ToList();
                return View(movie);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = context.Movies.Find(id);

            //crate a notification for all users
            var users = context.Users.ToList();
            foreach (var user in users)
            {
                var notification = new Notification
                {
                    Message = $"The movie '{movie.Name}' has been removed.",
                    CreatedAt = DateTime.Now,
                    UserId = user.Id,
                    IsRead = false
                };
                context.Notifications.Add(notification);
            }
            context.SaveChanges();

            return View(movie);
        }
        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            context.Movies.Remove(movie);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
