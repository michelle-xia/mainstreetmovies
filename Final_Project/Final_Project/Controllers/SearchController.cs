using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Final_Project.Models;
using Final_Project.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class SearchController : Controller
    {
        private AppDbContext _context;

        public SearchController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        
        public ActionResult Index()
        {
            var query = from mv in _context.Movies.Include(m => m.MovieShowings).Include(m => m.MovieReviews) select mv;
            //Display all postings, SelectedJobs 
            
            //Default displays all jobs
            if (ViewBag.SelectedMovies is null)
            {
                ViewBag.SelectedMovies = _context.Movies.Count();
            }
            ViewBag.AllMovies = _context.Movies.Count();
            return View(_context.Movies.Include(m => m.Genre).Include(m => m.MovieShowings).Include(m => m.MovieReviews).ToList().OrderByDescending(mv => mv.ReleaseDate));
            
        }

        public ActionResult DisplaySearchResults(SearchViewModel searchViewModel)
        {
            var query = from mv in _context.Movies.Include(m => m.MovieShowings)
                        .ThenInclude(t => t.Tickets)
                        .ThenInclude(t => t.MovieOrder)
                        .ThenInclude(t => t.Customer)
                        .ThenInclude(t => t.MovieReviews)
                        .Include(t => t.MovieReviews) select mv;
            

            //User entered title to search
            if (searchViewModel.SearchTitle != null && searchViewModel.SearchTitle != "")
            {
                query = query.Where(mv => mv.Title.Contains(searchViewModel.SearchTitle));
            }
            //User entered description to search
            if (searchViewModel.SearchTagline != null && searchViewModel.SearchTagline != "")
            {
                query = query.Where(mv => mv.Tagline.Contains(searchViewModel.SearchTagline));
            }
            //User selected category to search
            if (searchViewModel.SearchGenre != 0)
            {
                query = query.Where(mv => mv.Genre.GenreId == searchViewModel.SearchGenre);
            }
            if (searchViewModel.SearchActor != null)
            {
                query = query.Where(mv => mv.Actors.Contains(searchViewModel.SearchActor));
            }
            
            List<Movie> SelectedMovies = query.Include(mv => mv.Genre).Include(m => m.MovieShowings).Include(m => m.MovieReviews).ToList();

            //User selected release year to search
            if (searchViewModel.SearchReleaseYear != null)
            {
                SelectedMovies.RemoveAll(t => t.ReleaseYear != searchViewModel.SearchReleaseYear);
            }

            if (searchViewModel.MinimumRating != null || searchViewModel.MaximumRating != null)
            {
                //Check if user entered valid number
                TryValidateModel(searchViewModel);
                //User did not enter valid number
                if (ModelState.IsValid == false)
                {
                    ViewBag.AllGenres = GetAllGenres();
                    return View("DetailedSearch", searchViewModel);
                }

                if (searchViewModel.MinimumRating != null && searchViewModel.MaximumRating == null)
                {
                    SelectedMovies.RemoveAll(t => t.CustomerRating < 1);
                    SelectedMovies.RemoveAll(t => t.CustomerRating < searchViewModel.MinimumRating);
                }
                else if (searchViewModel.MinimumRating == null && searchViewModel.MaximumRating != null)
                {
                    SelectedMovies.RemoveAll(t => t.CustomerRating < 1);
                    SelectedMovies.RemoveAll(t => t.CustomerRating > searchViewModel.MaximumRating);
                }
                else
                {
                    if (searchViewModel.MaximumRating < searchViewModel.MinimumRating)
                    {
                        ViewBag.ErrorMessage = "Invalid Rating Search";
                        ViewBag.AllGenres = GetAllGenres();
                        return View("DetailedSearch", searchViewModel);
                    }
                    SelectedMovies.RemoveAll(t => t.CustomerRating < 1);
                    SelectedMovies.RemoveAll(t => t.CustomerRating < searchViewModel.MinimumRating || t.CustomerRating > searchViewModel.MaximumRating);
                }
            }

            if (searchViewModel.SearchMPAARating != null)
            {
                switch (searchViewModel.SearchMPAARating)
                {
                    case MPAARatings.G:
                        SelectedMovies.RemoveAll(mv => mv.MPAARating != MPAARatings.G);
                        break;
                    case MPAARatings.PG:
                        SelectedMovies.RemoveAll(mv => mv.MPAARating != MPAARatings.PG);
                        break;
                    case MPAARatings.PG13:
                        SelectedMovies.RemoveAll(mv => mv.MPAARating != MPAARatings.PG13);
                        break;
                    case MPAARatings.NC17:
                        SelectedMovies.RemoveAll(mv => mv.MPAARating != MPAARatings.NC17);
                        break;
                    case MPAARatings.R:
                        SelectedMovies.RemoveAll(mv => mv.MPAARating != MPAARatings.R);
                        break;
                    case MPAARatings.Unrated:
                        SelectedMovies.RemoveAll(mv => mv.MPAARating != MPAARatings.Unrated);
                        break;
                    default:
                        break;

                }

            }

            foreach (Movie m in SelectedMovies)
            {
                m.MovieShowings.RemoveAll(t => t.IsPublished == false);
                m.MovieShowings.RemoveAll(t => t.StartTime < DateTime.Now);
            }

            //Populate the view bag with a count of all job postings
            ViewBag.AllMovies = _context.Movies.Count();


            //Populate the view bag with a count of selected job postings
            ViewBag.SelectedMovies = SelectedMovies.Count();


            return View("Index", SelectedMovies.OrderByDescending(mv => mv.ReleaseDate));

        }

        private SelectList GetAllGenres()
        {
            //Get the list of categories from the database
            List<Genre> genreList = _context.Genres.ToList();

            //add a dummy entry so the user can select all categories
            Genre SelectNone = new Genre() { GenreId = 0, GenreName = "All Genres" };
            genreList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            //CategoryID and CategoryName are the names of the properties on the Category class
            //CategoryID is the primary key
            SelectList genreSelectList = new SelectList(genreList.OrderBy(m =>
           m.GenreId), "GenreId", "GenreName");

            //return the SelectList
            return genreSelectList;
        }

        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        public IActionResult SearchShowings()
        {
            return View();
        }

        public ActionResult DisplayShowings(SearchViewModel searchViewModel)
        {
            var query = from ms in _context.MovieShowings.Include(m => m.Tickets).ThenInclude(m => m.MovieOrder) select ms;
            List<MovieShowing> AllShowings = query.Include(mv => mv.Movie).ThenInclude(mv => mv.MovieReviews)
                                                        .Where(t => t.IsPublished == true)
                                                        .ToList();
            if (searchViewModel.SearchDateStart != null || searchViewModel.SearchDateEnd != null)
            {
                DateTime start = searchViewModel.SearchDateStart ?? new DateTime(1900, 1, 1);
                DateTime end = searchViewModel.SearchDateEnd ?? new DateTime(2200, 1, 1);

                if (end < start)
                {
                    ViewBag.ErrorMessage = "Date Range Invalid";
                    return View("SearchShowings", searchViewModel);
                }

                query = query.Where(mv => mv.StartTime.Date.AddDays(1) > start.Date).Where(mv => mv.StartTime.Date < end.Date.AddDays(1));
            }


            List<MovieShowing> SelectedShowings = query.Include(mv => mv.Movie)
                                                        .Where(t => t.IsPublished == true)
                                                        .Include(t => t.Tickets)
                                                        .ThenInclude(t=> t.MovieOrder)
                                                        .ThenInclude(t => t.Customer)
                                                        .ThenInclude(t => t.MovieReviews)
                                                        .Include(t => t.Tickets)
                                                        .ThenInclude(t => t.MovieOrder)
                                                        .ThenInclude(t => t.Recipient)
                                                        .ThenInclude(t => t.MovieReviews)
                                                        .ToList();

            AllShowings.RemoveAll(t => t.StartTime < DateTime.Now);
            SelectedShowings.RemoveAll(t => t.StartTime < DateTime.Now);

            //Populate the view bag with a count of all job postings
            ViewBag.AllShowings = AllShowings.Count();


            //Populate the view bag with a count of selected job postings
            ViewBag.SelectedShowings = SelectedShowings.Count();


            return View("DisplayShowings", SelectedShowings.OrderBy(mv => mv.StartTime));
        }
    

        public IActionResult Details(int? id)
        {
            if (id == null) //MovieID not specified
            {
                return View("Error", new String[] { "MovieID not specified - which movie do you want to view ?" });
            }

            Movie movie = _context.Movies.Include(mv => mv.Genre).Include(t => t.MovieReviews).FirstOrDefault(mv => mv.MovieId == id);
            List<MovieShowing> showings = _context.MovieShowings
                .Where(t => t.IsPublished == true)
                .Where(t => t.Movie.MovieId == movie.MovieId)
                .Include(t => t.Tickets)
                .ThenInclude(t => t.MovieOrder)
                .ThenInclude(t => t.Customer)
                .ThenInclude(t => t.MovieReviews)
                .Include(t => t.Tickets)
                .ThenInclude(t=> t.MovieOrder)
                .ThenInclude(t => t.Customer)
                .ThenInclude(t => t.MovieReviews)
                .Include(t => t.Tickets)
                .ThenInclude(t => t.MovieOrder)
                .ThenInclude(t => t.Recipient)
                .ThenInclude(t => t.MovieReviews)
                .ToList();

            movie.MovieShowings = showings;
            if (movie == null) //Job posting does not exist in database
            {
                return View("Error", new String[] { "Movie not found in database" });
            }

            //if code gets this far, all is well
            return View(movie);
        }

    } 

}
