using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Final_Project.Controllers
{
    public class MovieReviewsController : Controller
    {
        private readonly AppDbContext _context;

        public MovieReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MovieReviews
        [Authorize(Roles = "Employee,Manager,Customer")]
        public async Task<IActionResult> Index()
        {

            // Prevent movieorders that don't have a status confirmed from being shown     
            if (User.IsInRole("Employee") || User.IsInRole("Manager"))
            {
                return View(await _context.MovieReviews
                .Include(c => c.Movie)
/*                .Include(c => c.Customer)
                .ThenInclude(c => c.MovieOrders)
                .Where(c => c.Customer.MovieOrders.*/
                .ToListAsync());
            }
            else
            {
                return View(await _context.MovieReviews
                .Where(r => r.Customer.UserName == User.Identity.Name)
                .Include(c => c.Movie)
                .ToListAsync());
            }

        }

        // GET: MovieReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "This review was not found!" });
            }

            MovieReview movieReview = await _context.MovieReviews
                .Include(t => t.Customer)
                .Include(t => t.Movie)
                .FirstOrDefaultAsync(m => m.MovieReviewID == id);

            if (movieReview == null)
            {
                return View("Error", new String[] { "This review was not found in the database!" });
            }

            return View(movieReview);
        }

        // GET: MovieShowings/Create
        [Authorize(Roles ="Customer")]
        public IActionResult Create()
        {
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }

        // POST: MovieReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(MovieReview movieReview, int SelectedMovie)
        {
            
            if (ModelState.IsValid == false)
            {
                ViewBag.AllMovies = GetAllMovies();
                return View(movieReview);
            }

            AppUser customer = _context.Users
                                    .Where(t => t.UserName == User.Identity.Name)
                                    .Include(t => t.MovieReviews)
                                    .Include(t => t.MovieOrders)
                                    .ThenInclude(t => t.Tickets)
                                    .ThenInclude(t => t.MovieShowing)
                                    .ThenInclude(t => t.Movie)
                                    .FirstOrDefault();

            Movie movie = _context.Movies.Where(t => t.MovieId == SelectedMovie).FirstOrDefault();
            if (movie is null)
            {
                return View("Error", new String[] { "This movie was not found! You" +
                    "can't review a movie until you have watched it." });
            }

            movieReview.Movie = movie;

            if (customer == null)
            {
                return View("Error", new String[] { "This customer was not found!" });
            }

            /*HashSet<Movie> moviesSet = new HashSet<Movie>();

            customer.MovieOrders.RemoveAll(t => t.isConfirmed == false);

            foreach (MovieOrder mo in customer.MovieOrders)
            {
                foreach (Ticket ticket in mo.Tickets)
                {
                    if (ticket.MovieShowing.IsPublished == true && ticket.MovieShowing.EndTime < DateTime.Now)
                    {
                        moviesSet.Add(ticket.MovieShowing.Movie);
                    }                 
                }
            }

            List<Movie> moviesSeen = moviesSet.ToList();

            if (moviesSeen.Contains(movie) == false)
            {
                return View("Error", new String[] { "You have not seen this movie yet. You cannot review it." });
            }*/

            movieReview.Customer = customer;

            foreach (MovieReview m in customer.MovieReviews)
            {
                if (m.Movie == movieReview.Movie)
                {
                    return View("Error", new String[] { "This customer already has a review for this movie" });
                }
            }

            _context.Add(movieReview);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "MovieReviews", new { MovieReviewID = movieReview.MovieReviewID, SelectedMovieId = SelectedMovie });

        }

        // GET: MovieShowings/Edit/5
        [Authorize(Roles = "Employee,Manager,Customer")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify an review to edit" });
            }

            MovieReview movieReview;

            if (User.IsInRole("Customer"))
            {
                movieReview = await _context.MovieReviews
                                    .Where(r => r.Customer.UserName == User.Identity.Name)
                                    .Include(r => r.Movie)
                                    .FirstOrDefaultAsync(m => m.MovieReviewID == id);
            }
            else
            {
                movieReview = await _context.MovieReviews
                                    .Include(r => r.Customer)
                                    .Include(r => r.Movie)
                                    .FirstOrDefaultAsync(m => m.MovieReviewID == id);
            }

            if (movieReview == null)
            {
                return View("Error", new String[] { "This review was not found in the database!" });
            }

            return View(movieReview);
        }

        // POST: MovieShowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee,Manager,Customer")]
        public async Task<IActionResult> Edit(int id, MovieReview movieReview)
        {
            if (id != movieReview.MovieReviewID)
            {
                return View("Error", new String[] { "There was a problem editing this review. Try again!" });
            }

            if (User.IsInRole("Customer"))
            {
                try
                {
                    if (ModelState.IsValid == false)
                    {
                        return View(movieReview);
                    }

                    MovieReview dbmovieReview = _context.MovieReviews.Find(movieReview.MovieReviewID);

                    dbmovieReview.Rating = movieReview.Rating;
                    dbmovieReview.FullReview = movieReview.FullReview;
                    dbmovieReview.Approved = false;
                    _context.Update(dbmovieReview);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was an error updating this review!", ex.Message });
                }
            }
            else
            {
                try
                {
                    MovieReview dbmovieReview = _context.MovieReviews.Find(movieReview.MovieReviewID);
 
                    dbmovieReview.FullReview = movieReview.FullReview;

                    _context.Update(dbmovieReview);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return View("Error", new String[] { "There was an error updating this review!", ex.Message });
                }
            }


            return RedirectToAction(nameof(Index));
            
        }

        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> ApproveReview(int? id)
        { 
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a review to approve" });
            }

            MovieReview movieReview = await _context.MovieReviews
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(m => m.MovieReviewID == id);

            if (movieReview == null)
            {
                return View("Error", new String[] { "This review was not found!" });
            }

            if (movieReview.Approved == true)
            {
                return View("Error", new String[] { "You have already approved this review" });
            }

            return View(movieReview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> ApproveReview(int id, MovieReview movieReview)
        {
            if (id != movieReview.MovieReviewID)
            {
                return View("Error", new String[] { "There was a problem editing this review. Try again!" });
            }

            if (movieReview.Approved == false)
            {
                return View("Error", new String[] { "You did not choose to approve this review." });
            }

            try
            {
                MovieReview dbmovieReview = _context.MovieReviews.Find(movieReview.MovieReviewID);

                dbmovieReview.Approved = movieReview.Approved;

                _context.Update(dbmovieReview);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return View("Error", new String[] { "There was an error updating this review!", ex.Message });
            }


            return RedirectToAction(nameof(Index));
        }

        private bool MovieReviewExists(int id)
        {

            return _context.MovieReviews.Any(e => e.MovieReviewID == id);
        }


        private SelectList GetAllMovies()
        {
            AppUser customer = _context.Users.Where(r => r.UserName == User.Identity.Name)
                                        .Include(t => t.MovieOrders)
                                        .ThenInclude(t => t.Tickets)
                                        .ThenInclude(t => t.MovieShowing)
                                        .ThenInclude(t => t.Movie)
                                        .Include(t => t.OrdersReceived)
                                        .ThenInclude(t => t.Tickets)
                                        .ThenInclude(t => t.MovieShowing)
                                        .ThenInclude(t => t.Movie)
                                        .Include(t => t.MovieReviews)
                                        .ThenInclude(t => t.Movie)
                                        .FirstOrDefault();

            customer.MovieOrders.RemoveAll(t => t.isConfirmed == false);
            
            List<Movie> allMovies = new List<Movie>();
            foreach (MovieOrder mo in customer.MovieOrders)
            {
                foreach (Ticket t in mo.Tickets)
                {
                    if (t.MovieShowing.EndTime < DateTime.Now)
                    {
                        allMovies.Add(t.MovieShowing.Movie);
                    }
                }
            }

            foreach (MovieOrder mo in customer.OrdersReceived)
            {
                foreach (Ticket t in mo.Tickets)
                {
                    if (t.MovieShowing.EndTime < DateTime.Now)
                    {
                        allMovies.Add(t.MovieShowing.Movie);
                    }
                        
                }
            }
            List<int> toRemove = new List<int>();
            foreach (Movie m in allMovies)
            {
                foreach (MovieReview mr in customer.MovieReviews)
                {
                    if (mr.Movie.MovieId == m.MovieId)
                    {
                        toRemove.Add(mr.Movie.MovieId);
                        break;
                    }
                }
            }
            allMovies.RemoveAll(t => toRemove.Contains(t.MovieId));
            allMovies = allMovies.Distinct().ToList();
            //use the constructor on select list to create a new select list with the options
            SelectList slAllMovies = new SelectList(allMovies.OrderBy(m => m.Title), nameof(Movie.MovieId), nameof(Movie.Title));

            return slAllMovies;
        }

    }
}
