using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;


namespace Final_Project
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieShowings)
                .Include(m => m.MovieReviews)
                .ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieShowings)
                .Include(m => m.MovieReviews)
                .FirstOrDefaultAsync(m => m.UniqueIdentifier == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
       
        // GET: Movies/Create
        [Authorize(Roles = "Manager")]

        public IActionResult Create()
        {
            ViewBag.AllGenres = GetAllGenres();

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]

        public async Task<IActionResult> Create(Movie movie, int SelectedGenre)
        {
            movie.MovieId = Utilities.GetMovieID.GetNextUI(_context);
            movie.UniqueIdentifier = Utilities.GetMovieID.GetNextUI(_context);
            Genre dbGenre = _context.Genres.Find(SelectedGenre);

            movie.Genre = dbGenre;
            if (ModelState.IsValid == false)
            {
        
                ViewBag.AllGenres = GetAllGenres();
                return View(movie);
            }

     
            _context.Add(movie);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Movies" );
        }
        private SelectList GetAllGenres()
        {
            //create a list for all the courses
            List<Genre> allGenres = _context.Genres.ToList();

            //the user MUST select a course, so you don't need a dummy option for no course

            //use the constructor on select list to create a new select list with the options
            SelectList slAllGenres = new SelectList(allGenres, nameof(Genre.GenreId), nameof(Genre.GenreName));

            return slAllGenres;
        }
        // GET: Movies/Edit/5

        [Authorize(Roles = "Manager")]
        public IActionResult CreateGenre()
        {
            ViewBag.AllGenres = GetAllGenres();

            return View();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Genres()
        {
            List<Genre> allGenres = _context.Genres.ToList();

            return View(allGenres);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]

        public async Task<IActionResult> CreateGenre(Genre genre)
        {

            if (ModelState.IsValid == false)
            {

                ViewBag.AllGenres = GetAllGenres();
                return View(genre);
            }


            _context.Add(genre);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Movies");
        }


        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.UniqueIdentifier == id);
        }
    }
}
