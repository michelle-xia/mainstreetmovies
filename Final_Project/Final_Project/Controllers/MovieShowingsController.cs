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
    [Authorize(Roles = "Manager")]
    public class MovieShowingsController : Controller
    {
        private readonly AppDbContext _context;

        public MovieShowingsController(AppDbContext context)
        {
            _context = context;
        }

        // Extra methods

        //Get exact cut-offs for movies in the time range
        public List<MovieShowing> FilterNextWeekShowings()
        {
            DateTime weekStart = StartCutoff();
            DateTime weekEnd = EndCutoff(weekStart);
            List<MovieShowing> nextWeeks = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Where(c => c.StartTime > weekStart)
                .Where(c => c.StartTime < weekEnd)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();

            return nextWeeks;
        }

        public MovieShowing NextWeekShowing(int id)
        {
            DateTime weekStart = StartCutoff();
            DateTime weekEnd = EndCutoff(weekStart);

            MovieShowing movieShowing = _context.MovieShowings
                                                .Where(t => t.StartTime > weekStart)
                                                .Where(c => c.StartTime < weekEnd)
                                                .Where(c => c.MovieShowingID == id)
                                                .Include(c => c.Movie)
                                                .Include(c => c.Tickets)
                                                .ThenInclude(c => c.MovieOrder)
                                                .ThenInclude(c => c.Customer)
                                                .FirstOrDefault();
            return movieShowing;
        }

        public DateTime StartCutoff()
        {
            var x = DateTime.Now.AddDays(7);
            DateTime weekStart = new DateTime(x.Year, x.Month, x.Day, 0, 0, 0);
            return weekStart;
        }

        public DateTime EndCutoff(DateTime weekStart)
        {
            DateTime weekEnd = weekStart.AddDays(7).AddSeconds(-1);
            return weekEnd;
        }

        private SelectList GetAllMovies()
        {
            //create a list for all the courses
            List<Movie> allMovies = _context.Movies.ToList();

            //use the constructor on select list to create a new select list with the options
            SelectList slAllMovies = new SelectList(allMovies.OrderBy(m => m.Title), nameof(Movie.MovieId), nameof(Movie.Title));

            return slAllMovies;
        }

        public List<String> DaysOfWeek()
        {
            List<String> allDays = new List<String>()
            {
                "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
            };
            return allDays;

        }

        private SelectList GetAllWeekdays()
        {
            //create a list for all the courses
            List<String> allDays = DaysOfWeek();

            //use the constructor on select list to create a new select list with the options
            SelectList slAllMovies = new SelectList(allDays, allDays);

            return slAllMovies;
        }

        public Boolean CheckOverlappingTimes(MovieShowing movieShowing)
        {
            //Check for business rules
            //Get all showings scheduled in the next week
            List<MovieShowing> dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
            dbShowings.RemoveAll(t => t.MovieShowingID == movieShowing.MovieShowingID);
            List<MovieShowing> t1 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.One).OrderBy(t => t.StartTime).ToList();
            List<MovieShowing> t2 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.Two).OrderBy(t => t.StartTime).ToList();
            if (movieShowing.TheaterSelection == TheaterType.One)
            {
                t1.Add(movieShowing);              
            }
            else
            {
                t2.Add(movieShowing);
            }

            t1 = t1.OrderBy(t => t.StartTime).ToList();
            t2 = t2.OrderBy(t => t.StartTime).ToList();

            //Loop through showings and check for business rules
            for (int i = 0; i < t1.Count() - 1; i++)
            {
                if (t1[i].EndTime > t1[i + 1].StartTime)
                {
                    return true;
                }
            }

            for (int i = 0; i < t2.Count() - 1; i++)
            {
                if (t2[i].EndTime > t2[i + 1].StartTime)
                {
                    return true;
                }
            }

            return false;
        }

        //Final schedule publishing check
        public Boolean CheckSameTimeShowing(DateTime? date)
        {
            DateTime defaultDate = new DateTime(1990, 1, 1); ;
            List<MovieShowing> dbShowings;
            if (date == defaultDate)
            {
                dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
            }
            else
            {
                DateTime nonnull = (DateTime)date;
                dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Where(c => c.StartTime.Date == nonnull.Date)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
            }

            var grouped = dbShowings.GroupBy(t => t.Movie.MovieId);
            foreach (var group in grouped)
            {
                var ordered = group.OrderBy(t => t.StartTime).ToList();
                for (int i = 0; i < ordered.Count() - 1; i++)
                {
                    if (ordered[i].StartTime == ordered[i + 1].StartTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Individual movie create check
        public Boolean CheckSameTimeShowing(MovieShowing movieShowing)
        {
            List<MovieShowing> dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();

            dbShowings.RemoveAll(t => t.MovieShowingID == movieShowing.MovieShowingID);
            dbShowings.Add(movieShowing);

            var grouped = dbShowings.GroupBy(t => t.Movie.MovieId);
            foreach (var group in grouped)
            {
                var ordered = group.OrderBy(t => t.StartTime).ToList();
                for (int i = 0; i < ordered.Count() - 1; i++)
                {
                    if (ordered[i].StartTime == ordered[i + 1].StartTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Individual day to publish check
        /*public Boolean CheckSameTimeShowing(DateTime date)
        {
            List < MovieShowing > dbShowings = _context.MovieShowings
                .Include(c => c.Movie)
                .Where(c => c.StartTime.Date == date.Date)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();

            var grouped = dbShowings.GroupBy(t => t.Movie.MovieId);
            foreach (var group in grouped)
            {
                var ordered = group.OrderBy(t => t.StartTime).ToList();
                for (int i = 0; i < ordered.Count() - 1; i++)
                {
                    if (ordered[i].StartTime == ordered[i + 1].StartTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }*/

        public Boolean CheckStartEndTime(MovieShowing showing)
        {
            if (showing.StartTime >= showing.EndTime)
            {
                return true;
            }
            return false;
        }

        public Boolean CheckTimeFrame(MovieShowing showing)
        {
            DateTime weekStart = StartCutoff();
            DateTime weekEnd = EndCutoff(weekStart);
            DateTime open = new DateTime(showing.StartTime.Year, showing.StartTime.Month, showing.StartTime.Day, 9, 0, 0);
            DateTime close = new DateTime(showing.StartTime.Year, showing.StartTime.Month, showing.StartTime.Day, 23, 59, 0);

            //Change per piazza reqs
            if (showing.StartTime < DateTime.Now.AddHours(1))
            {
                return true;
            }
            /*
            if (showing.EndTime > weekEnd)
            {
                return true;
            }*/
            if (showing.StartTime < open)
            {
                return true;
            }
            if (showing.StartTime > close.AddMinutes(1))
            {
                return true;
            }

            return false;
        }
        
        public Boolean CheckForGaps(DateTime? date)
        {
            DateTime defaultDate = new DateTime(1990, 1, 1);
            List<MovieShowing> dbShowings;
            if (date == defaultDate)
            {
                dbShowings = FilterNextWeekShowings();
            }
            else
            {
                DateTime nonnull = (DateTime)date;
                dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
                dbShowings.RemoveAll(c => c.StartTime.Date != nonnull.Date);
            }
            if (dbShowings.Count() == 0)
            {
                return true;
            }
            List<MovieShowing> t1 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.One).OrderBy(t => t.StartTime).ToList();
            List<MovieShowing> t2 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.Two).OrderBy(t => t.StartTime).ToList();
            var g1 = t1.GroupBy(t => t.StartTime.DayOfWeek);
            var g2 = t2.GroupBy(t => t.StartTime.DayOfWeek);

            List<String> days = DaysOfWeek();
            foreach (var group in g1)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                days.Remove(tmp.First().StartTime.DayOfWeek.ToString());
                for (int i = 0; i < tmp.Count() - 1; i++)
                {
                    if (tmp[i].EndTime.AddMinutes(25) > tmp[i + 1].StartTime)
                    {
                        Console.WriteLine("theater 1 tmp 25" + tmp[i].FullShowingName + tmp[i].EndTime + tmp[i+1].FullShowingName + tmp[i+1].StartTime);
                        return true;
                    }
                    if (tmp[i].EndTime.AddMinutes(45) < tmp[i + 1].StartTime)
                    {
                        Console.WriteLine("theater 1 tmp 45" + tmp[i].FullShowingName + tmp[i].EndTime + tmp[i + 1].FullShowingName + tmp[i + 1].StartTime);
                        return true;
                    }
                }
            }
            if (days.Count() != 0 && date == defaultDate)
            {
                return true;
            }

            List<String> days2 = DaysOfWeek();
            foreach (var group in g2)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                days2.Remove(tmp.First().StartTime.DayOfWeek.ToString());
                for (int i = 0; i < tmp.Count() - 1; i++)
                {
                    if (tmp[i].EndTime.AddMinutes(25) > tmp[i + 1].StartTime)
                    {
                        Console.WriteLine("theater 2 tmp 25" + tmp[i].FullShowingName + tmp[i].EndTime + tmp[i + 1].FullShowingName + tmp[i + 1].StartTime);
                        return true;
                    }
                    if (tmp[i].EndTime.AddMinutes(45) < tmp[i + 1].StartTime)
                    {
                        Console.WriteLine("theater 2 tmp 45" + tmp[i].FullShowingName + tmp[i].EndTime + tmp[i + 1].FullShowingName + tmp[i + 1].StartTime);
                        return true;
                    }
                }
            }
            if (days2.Count() != 0 && date == defaultDate)
            {
                return true;
            }

            return false;
        }

        //Checking rescheduling from EDIT
        public Boolean CheckForGaps(MovieShowing showing, DateTime date)
        {
            DateTime weekStart = StartCutoff();
            DateTime weekEnd = EndCutoff(weekStart);

            //See if showing already exists in the database
            MovieShowing db = _context.MovieShowings
                .Where(t => t.MovieShowingID == showing.MovieShowingID)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .FirstOrDefault();

            List<MovieShowing> dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Where(t => t.IsPublished == true)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
            dbShowings.RemoveAll(c => c.StartTime.Date != date.Date);

            dbShowings.Remove(db);
            dbShowings.Add(showing);
            List<MovieShowing> t1 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.One).OrderBy(t => t.StartTime).ToList();
            List<MovieShowing> t2 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.Two).OrderBy(t => t.StartTime).ToList();
            var g1 = t1.GroupBy(t => t.StartTime.DayOfWeek);
            var g2 = t2.GroupBy(t => t.StartTime.DayOfWeek);

            foreach (var group in g1)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                for (int i = 0; i < tmp.Count() - 1; i++)
                {
                    if (tmp[i].EndTime.AddMinutes(25) > tmp[i + 1].StartTime)
                    {
                        return true;
                    }
                    if (tmp[i].EndTime.AddMinutes(45) < tmp[i + 1].StartTime)
                    {
                        return true;
                    }
                }
            }

            foreach (var group in g2)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                for (int i = 0; i < tmp.Count() - 1; i++)
                {
                    if (tmp[i].EndTime.AddMinutes(25) > tmp[i + 1].StartTime)
                    {
                        return true;
                    }
                    if (tmp[i].EndTime.AddMinutes(45) < tmp[i + 1].StartTime)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Boolean CheckLastShowing(DateTime? date)
        {
            DateTime defaultDate = new DateTime(1990, 1, 1); ;
            List<MovieShowing> dbShowings;
            if (date == defaultDate)
            {
                dbShowings = FilterNextWeekShowings();
            }
            else
            {
                DateTime nonnull = (DateTime)date;
                dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
                dbShowings.RemoveAll(c => c.StartTime.Date != nonnull.Date);
            }

            List<MovieShowing> t1 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.One).OrderBy(t => t.StartTime).ToList();
            List<MovieShowing> t2 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.Two).OrderBy(t => t.StartTime).ToList();

            var g1 = t1.GroupBy(t => t.StartTime.Date);
            var g2 = t2.GroupBy(t => t.StartTime.Date);

            foreach(var group in g1)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                MovieShowing last = tmp.Last();
                DateTime limit = new DateTime(last.StartTime.Year, last.StartTime.Month, last.StartTime.Day, 21, 30, 0);
                if (last.EndTime < limit)
                {
                    return true;
                }
            }

            foreach (var group in g2)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                MovieShowing last = tmp.Last();
                DateTime limit = new DateTime(last.StartTime.Year, last.StartTime.Month, last.StartTime.Day, 21, 30, 0);
                if (last.EndTime < limit)
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean CheckLastShowing(MovieShowing showing, DateTime date)
        {
            DateTime weekStart = StartCutoff();
            DateTime weekEnd = EndCutoff(weekStart);

            //See if showing already exists in the database
            MovieShowing db = _context.MovieShowings
                .Where(t => t.MovieShowingID == showing.MovieShowingID)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .FirstOrDefault();

            List<MovieShowing> dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Where(t => t.IsPublished == true)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();

            dbShowings.RemoveAll(c => c.StartTime.Date != date.Date);
            dbShowings.Remove(db);
            dbShowings.Add(showing);

            List<MovieShowing> t1 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.One).OrderBy(t => t.StartTime).ToList();
            List<MovieShowing> t2 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.Two).OrderBy(t => t.StartTime).ToList();

            var g1 = t1.GroupBy(t => t.StartTime.DayOfWeek);
            var g2 = t2.GroupBy(t => t.StartTime.DayOfWeek);

            foreach (var group in g1)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                MovieShowing last = tmp.Last();
                DateTime limit = new DateTime(last.StartTime.Year, last.StartTime.Month, last.StartTime.Day, 17, 30, 0);
                if (last.EndTime < limit)
                {
                    return true;
                }
            }

            foreach (var group in g2)
            {
                var tmp = group.OrderBy(t => t.StartTime).ToList();
                MovieShowing last = tmp.Last();
                DateTime limit = new DateTime(last.StartTime.Year, last.StartTime.Month, last.StartTime.Day, 17, 30, 0);
                if (last.EndTime < limit)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public Dictionary<String, DateTime> DaysToDates()
        {
            //Create dictionary to store key value pairs
            Dictionary<String, DateTime> dict = new Dictionary<string, DateTime>();

            //Iterate through all dates for next week
            DateTime start = StartCutoff().Date;
            DateTime end = EndCutoff(start).Date;

            foreach (DateTime day in EachDay(start, end))
            {
                dict.Add(day.DayOfWeek.ToString(), day);
            }

            return dict;
        }

        public void ClearOldSchedule(TheaterType type, DateTime SelectedTo)
        {
            // Clear old scheduling from the day
            List<MovieShowing>oldSchedule = _context.MovieShowings
                .Where(t => t.TheaterSelection == type)
               .ToList();
            oldSchedule = oldSchedule.FindAll(t => t.StartTime.Date == SelectedTo.Date);
            foreach (MovieShowing ms1 in oldSchedule)
            {
                _context.Remove(ms1);
                
            }
            _context.SaveChanges();
        }

        public List<AppUser> RecipientsFromShowing(MovieShowing ms)
        {
            MovieShowing dbShowing = _context.MovieShowings
                .Where(t => t.MovieShowingID == ms.MovieShowingID)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Recipient)
                .FirstOrDefault();

            var g = dbShowing.Tickets.GroupBy(t => t.MovieOrder);
            List<AppUser> recipients = new List<AppUser>();

            foreach (var group in g)
            {
                AppUser cust = group.First().MovieOrder.Recipient;
                recipients.Add(cust);
            }
            recipients = recipients.Distinct().ToList();
            return recipients;
        }

        public List<AppUser> CustomersFromShowing(MovieShowing ms)
        {
            MovieShowing dbShowing = _context.MovieShowings
                .Where(t => t.MovieShowingID == ms.MovieShowingID)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .FirstOrDefault();

            var g = dbShowing.Tickets.GroupBy(t => t.MovieOrder);
            List<AppUser> customers = new List<AppUser>();

            foreach (var group in g)
            {
                AppUser cust = group.First().MovieOrder.Customer;
                customers.Add(cust);
            }
            customers = customers.Distinct().ToList();
            return customers;
        }

        //TODO: ACTION METHODS
        // GET: MovieShowings
        public IActionResult Index()
        {
            List<MovieShowing> dbShowings = _context.MovieShowings
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
            dbShowings = dbShowings.OrderBy(t => t.StartTime).ToList();
            return View(dbShowings);
        }

        // GET: MovieShowings/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "This showing was not found!" });
            }

            MovieShowing movieShowing = _context.MovieShowings
                .Where(t => t.MovieShowingID == id)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .Include(t => t.Movie)
                .FirstOrDefault();

            if (movieShowing == null)
            {
                return View("Error", new String[] { "This showing was not found in the database!" });
            }

            return View(movieShowing);
        }

        // GET: MovieShowings/Create
        [Authorize(Roles = "Manager")]

        public IActionResult Create()
        {
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }

        // POST: MovieShowings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(MovieShowing movieShowing, int SelectedMovie)
        {
            //Update properties
            Movie movie = _context.Movies.Where( t => t.MovieId == SelectedMovie).FirstOrDefault();
            movieShowing.Movie = movie;
            movieShowing.EndTime = movieShowing.StartTime.AddMinutes(movie.Runtime);

            if (ModelState.IsValid == false)
            {
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (CheckOverlappingTimes(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "You can't schedule overlapping showings in the same theater.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (CheckSameTimeShowing(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "You can't schedule showings of the same movie at the same time.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (CheckStartEndTime(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "You can't schedule start time before or at end time.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (CheckTimeFrame(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "Scheduling time frame invalid.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            _context.Add(movieShowing);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //GET: Select MovieShowing to Copy
        [Authorize(Roles = "Manager")]

        public IActionResult Copy()
        {
            return View();
        }

        //POST: Submit copy
        [Authorize(Roles = "Manager")]

        public async Task<IActionResult> FinalizeCopy(CopyViewModel cvm)
        {
            if (ModelState.IsValid == false)
            {
                return View("Copy", cvm);
            }
            
            //List<String> daysOfWeek = DaysOfWeek();
            //var map = DaysToDates();
            DateTime copyToDate = cvm.CopyToDate;
            DateTime copyFromDate = cvm.CopyFromDate;

            if (copyToDate.Date < DateTime.Now.Date.AddDays(1))
            {
                ViewBag.ErrorMessage = "The day you are trying to copy to is in the past.";
                return View("Copy", cvm);
            }

            // Get all showings to copy from
            List<MovieShowing> dbShowings = _context.MovieShowings
                .Where(t => t.ShowingStatus != PublishStatus.Canceled)
                .Include(c => c.Movie)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
            List<MovieShowing> copyFromShowings1 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.One).OrderBy(t => t.StartTime).ToList();
            List<MovieShowing> copyFromShowings2 = dbShowings.FindAll(t => t.TheaterSelection == TheaterType.Two).OrderBy(t => t.StartTime).ToList();


            //Change to date here if desired
            copyFromShowings1.RemoveAll(t => t.StartTime.Date != copyFromDate.Date);
            copyFromShowings2.RemoveAll(t => t.StartTime.Date != copyFromDate.Date);

            //Iterate through showings and create new MovieShowings with same date
            MovieShowing tmp;
            Movie movie;
            List<MovieShowing> toAdd = new List<MovieShowing>();
            if (cvm.CopyFromTheater == TheaterType.One)
            {
                if (copyFromShowings1.Count() == 0)
                {
                    ViewBag.ErrorMessage = "The schedule you are copying is empty";
                    return View("Copy", cvm);
                }

                if (cvm.CopyToTheater == TheaterType.One)
                {
                    ClearOldSchedule(TheaterType.One, copyToDate);
                }
                else
                {
                    ClearOldSchedule(TheaterType.Two, copyToDate);
                }

                foreach (MovieShowing ms in copyFromShowings1)
                {
                    tmp = new MovieShowing();
                    movie = _context.Movies.Where(t => t.MovieId == ms.Movie.MovieId).FirstOrDefault();
                    tmp.StartTime = copyToDate.Date.Add(ms.StartTime.TimeOfDay);
                    tmp.EndTime = tmp.StartTime.AddMinutes(movie.Runtime);
                    tmp.Movie = movie;
                    if (cvm.CopyToTheater == TheaterType.One)
                    {
                        tmp.TheaterSelection = TheaterType.One;
                    }
                    else
                    {
                        tmp.TheaterSelection = TheaterType.Two;
                    }
                    toAdd.Add(tmp);
                    
                }
            }
            else
            {
                foreach (MovieShowing ms in copyFromShowings2)
                {
                    if (copyFromShowings2.Count() == 0)
                    {
                        ViewBag.ErrorMessage = "The schedule you are copying is empty";
                        return View("Copy", cvm);
                    }

                    if (cvm.CopyToTheater == TheaterType.One)
                    {
                        ClearOldSchedule(TheaterType.One, copyToDate);
                    }
                    else
                    {
                        ClearOldSchedule(TheaterType.Two, copyToDate);
                    }

                    tmp = new MovieShowing();
                    movie = await _context.Movies.Where(t => t.MovieId == ms.Movie.MovieId).FirstOrDefaultAsync();
                    tmp.StartTime = copyToDate.Date.Add(ms.StartTime.TimeOfDay);
                    tmp.EndTime = tmp.StartTime.AddMinutes(movie.Runtime);
                    tmp.Movie = movie;
                    if (cvm.CopyToTheater == TheaterType.One)
                    {
                        tmp.TheaterSelection = TheaterType.One;
                    }
                    else
                    {
                        tmp.TheaterSelection = TheaterType.Two;
                    }
                    toAdd.Add(tmp);
                }
            }
            foreach (MovieShowing ms in toAdd)
            {
                _context.Add(ms);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: MovieShowings/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "This showing was not found!" });
            }

            MovieShowing movieShowing = await _context.MovieShowings
                                                        .Include(t => t.Movie)
                                                        .Where(t => t.MovieShowingID == id)
                                                        .FirstOrDefaultAsync();
            if (movieShowing == null)
            {
                return View("Error", new String[] { "This showing was not found in the database!" });
            }

            //Check for time of editing
            if (DateTime.Now > movieShowing.StartTime)
            {
                return View("Error", new String[] { "This showing has started and cannot be rescheduled." });
            }

            return View(movieShowing);
        }

        // POST: MovieShowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, MovieShowing movieShowing)
        {
            if (id != movieShowing.MovieShowingID)
            {
                return View("Error", new String[] { "There was a problem editing this order. Try again!" });
            }

            if (ModelState.IsValid == false)
            {
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }
            MovieShowing showing = await _context.MovieShowings
                                                        .Include(t => t.Movie)
                                                        .Where(t => t.MovieShowingID == id)
                                                        .FirstOrDefaultAsync();

            if (showing.ShowingStatus == PublishStatus.Canceled)
            {
                return View("Error", new String[] { "You cannot edit a canceled showing." });
            }
            movieShowing.Movie = showing.Movie;
            movieShowing.IsPublished = showing.IsPublished;
            if (movieShowing.IsPublished == true)
            {
                movieShowing.IsSpecial = showing.IsSpecial;
            }
            
            movieShowing.EndTime = movieShowing.StartTime.AddMinutes(showing.Movie.Runtime);
            //movieShowing.TheaterSelection = showing.TheaterSelection;
            //Check start date
            if (movieShowing.StartTime == default)
            {
                ViewBag.ErrorMessage = "You must select a new time to reschedule to.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (CheckOverlappingTimes(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "You can't schedule overlapping showings in the same theater.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (CheckSameTimeShowing(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "You can't schedule showings of the same movie at the same time.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (CheckStartEndTime(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "You can't schedule start time before or at end time.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }
            /*if (CheckTimeFrame(movieShowing) == true)
            {
                ViewBag.ErrorMessage = "Scheduling time frame invalid.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }*/
            
            DateTime open = new DateTime(movieShowing.StartTime.Year, movieShowing.StartTime.Month, movieShowing.StartTime.Day, 9, 0, 0);
            if (movieShowing.StartTime < open)
            {
                ViewBag.ErrorMessage = "Showing can't start before 9:00 am.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (movieShowing.StartTime.AddHours(1) < DateTime.Now)
            {
                ViewBag.ErrorMessage = "Showing can't be rescheduled to before now or within one hour to now.";
                ViewBag.AllMovies = GetAllMovies();
                return View(movieShowing);
            }

            if (movieShowing.IsPublished == true) //TODO: only check for only one day
            {
                //Check for an unscheduled week
                DateTime weekStart = StartCutoff();
                DateTime weekEnd = EndCutoff(weekStart);
                if (movieShowing.StartTime > weekEnd)
                {
                    movieShowing.IsPublished = false;
                    movieShowing.ShowingStatus = PublishStatus.Unpublished;
                }

                if (movieShowing.IsPublished == true)
                {
                    DateTime date = movieShowing.StartTime;
                    if (CheckForGaps(movieShowing, date) == true)
                    {
                        ViewBag.ErrorMessage = "Rescheduling to this time will create illegal gaps in the scheduling.";
                        ViewBag.AllMovies = GetAllMovies();
                        return View(movieShowing);
                    }

                    if (CheckLastShowing(movieShowing, date) == true)
                    {
                        ViewBag.ErrorMessage = "The last movie cannot end before 9:30pm everyday.";
                        ViewBag.AllMovies = GetAllMovies();
                        return View(movieShowing);
                    }
                    movieShowing.ShowingStatus = PublishStatus.Published;
                }                
            }

            try
            {
                //find the record in the database
                MovieShowing dbMovieShowing = _context.MovieShowings.Find(movieShowing.MovieShowingID);

                if (movieShowing.StartTime != default)
                {
                    dbMovieShowing.StartTime = movieShowing.StartTime;
                    dbMovieShowing.EndTime = movieShowing.EndTime;
                }
                
                dbMovieShowing.IsPublished = movieShowing.IsPublished;
                dbMovieShowing.ShowingStatus = movieShowing.ShowingStatus;
                dbMovieShowing.TheaterSelection = movieShowing.TheaterSelection;

                if (dbMovieShowing.IsPublished == false)
                {
                    dbMovieShowing.IsSpecial = movieShowing.IsSpecial;
                }

                _context.Update(dbMovieShowing);
                _context.SaveChanges();

                if (movieShowing.IsPublished == true)
                {
                    List<AppUser> customers = CustomersFromShowing(dbMovieShowing);
                    List<AppUser> recipients = RecipientsFromShowing(dbMovieShowing);
                    foreach (AppUser cust in customers)
                    {
                        Utilities.Email.SendEmail(cust.Email, "Showing Rescheduled", $"The {movieShowing.FullShowingName} Movie Showing has been rescheduled.");
                    }
                    foreach (AppUser rec in recipients)
                    {
                        if (rec != null)
                        {
                            Utilities.Email.SendEmail(rec.Email, "Showing Rescheduled", $"The {movieShowing.FullShowingName} Movie Showing has been rescheduled.");
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error updating this order!", ex.Message });
            }
            return RedirectToAction("Index");
        }

        //Publish
        [Authorize(Roles = "Manager")]
        public IActionResult Publish()
        {
            DateTime date = new DateTime(1990, 1, 1);
            //Perform logic checks
            if (CheckSameTimeShowing(date) == true)
            {
                return View("Error", new String[] { "You can't schedule showings of the same movie at the same time." });
            }

            if (CheckForGaps(date) == true)
            {
                return View("Error", new String[] { "There was an issue with the gaps in scheduling." });
            }

            if (CheckLastShowing(date) == true)
            {
                return View("Error", new String[] { "The last movie cannot end before 9:30pm everyday." });
            }

            //If logic checks pass, give confirmation page and then redirect to confirmation

            return RedirectToAction("Confirm");
        }

        //GET method for day by day publishing
        public IActionResult SelectPublishDate()
        {
            return View();
        }

        public IActionResult PublishSelectedDate(PublishViewModel pvm)
        {
            if (ModelState.IsValid == false)
            {
                return View(pvm);
            }
            DateTime date = pvm.DateSelected;

            if (CheckSameTimeShowing(date) == true)
            {
                return View("Error", new String[] { "You can't schedule showings of the same movie at the same time." });
            }

            if (CheckForGaps(date) == true)
            {
                return View("Error", new String[] { "There was an issue with the gaps in scheduling." });
            }

            if (CheckLastShowing(date) == true)
            {
                return View("Error", new String[] { "The last movie cannot end before 9:30pm everyday." });
            }
            return RedirectToAction("ConfirmSingleDay", date);
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Confirm()
        {
            List<MovieShowing> movieShowings = FilterNextWeekShowings();
            foreach (MovieShowing ms in movieShowings)
            {
                if (ms.ShowingStatus == PublishStatus.Unpublished)
                {
                    ms.IsPublished = true;
                    ms.ShowingStatus = PublishStatus.Published;
                    _context.Update(ms);
                }
            }
            await _context.SaveChangesAsync();
            return View();
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> ConfirmSingleDay(DateTime date)
        {
            List<MovieShowing> movieShowings = _context.MovieShowings
                .Include(c => c.Movie)
                .Where(c => c.StartTime.Date == date.Date)
                .Include(c => c.Tickets)
                .ThenInclude(c => c.MovieOrder)
                .ThenInclude(c => c.Customer)
                .ToList();
            foreach (MovieShowing ms in movieShowings)
            {
                if (ms.ShowingStatus == PublishStatus.Unpublished)
                {
                    ms.IsPublished = true;
                    ms.ShowingStatus = PublishStatus.Published;
                    _context.Update(ms);
                }               
            }
            await _context.SaveChangesAsync();
            return View("Confirm");
        }

        // GET: MovieShowings/Cancel/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "You must enter an ID." });
            }

            var movieShowing = await _context.MovieShowings
                .FirstOrDefaultAsync(m => m.MovieShowingID == id);
            if (movieShowing == null)
            {
                return View("Error", new String[] { "This showing wasn't found in the database." });
            }

            if (DateTime.Now > movieShowing.StartTime)
            {
                return View("Error", new String[] { "This showing has started and cannot be canceled." });
            }

            if (movieShowing.IsPublished == false)
            {
                return View("Error", new String[] { "You cannot cancel an unpublished showing. Delete it instead." });
            }

            return View(movieShowing);
        }

        // POST: MovieShowings/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            MovieShowing movieShowing = await _context.MovieShowings
                                                           .Where(t => t.MovieShowingID == id)
                                                           .Include(t => t.Tickets)
                                                           .ThenInclude(t => t.MovieOrder)
                                                           .ThenInclude(t => t.Customer)
                                                           .ThenInclude(t => t.MovieOrders)
                                                           .Include(t => t.Tickets)
                                                           .Include(t => t.Movie)
                                                           .FirstOrDefaultAsync();
            //Refund popcorn points
            MovieOrder dbOrder;
            List<Ticket> toDelete = new List<Ticket>();
            foreach (Ticket t in movieShowing.Tickets)
            {
                dbOrder = _context.MovieOrders.Where(o => o.MovieOrderID == t.MovieOrder.MovieOrderID)
                                                .Include(o => o.Customer)
                                                .Include(o => o.Recipient)
                                                .FirstOrDefault();
                //dbOrder.OrderStatus = Status.Cancelled;
                toDelete.Add(t);
                if (dbOrder.UsingPopcornPoints == true)
                {
                    dbOrder.PopcornPointsEarned += 100;
                }
                _context.Remove(t);
                _context.Update(dbOrder);
            }

            movieShowing.IsPublished = false;
            movieShowing.ShowingStatus = PublishStatus.Canceled;
            _context.Update(movieShowing);           
            List<AppUser> customers = CustomersFromShowing(movieShowing);
            List<AppUser> recipients = RecipientsFromShowing(movieShowing);
            foreach (AppUser cust in customers)
            {
                Utilities.Email.SendEmail(cust.Email, "Showing Canceled", $"The {movieShowing.FullShowingName} Movie Showing has been canceled. If you " +
                    "paid with Popcorn Points you will be refunded.");
            }
            foreach (AppUser rec in recipients)
            {
                if (rec != null)
                {
                    Utilities.Email.SendEmail(rec.Email, "Showing Canceled", $"The {movieShowing.FullShowingName} Movie Showing has been canceled. If you " +
                    "paid with Popcorn Points you will be refunded.");
                }               
            }
            /*foreach (Ticket tick in toDelete)
            {
                _context.Remove(tick);
            }*/
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieShowingExists(int id)
        {
            return _context.MovieShowings.Any(e => e.MovieShowingID == id);
        }

        // GET: MovieShowings/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "You must enter an ID." });
            }

            var movieShowing = await _context.MovieShowings
                .FirstOrDefaultAsync(m => m.MovieShowingID == id);
            if (movieShowing == null)
            {
                return View("Error", new String[] { "This showing wasn't found in the database." });
            }

            if (movieShowing.ShowingStatus == PublishStatus.Canceled)
            {
                return View("Error", new String[] { "You cannot delete a canceled showing." });
            }

            if (DateTime.Now > movieShowing.StartTime)
            {
                return View("Error", new String[] { "This showing has started and cannot be rescheduled." });
            }

            if (movieShowing.IsPublished == true)
            {
                return View("Error", new String[] { "You cannot delete a published showing. Cancel it instead." });
            }

            return View(movieShowing);
        }

        // POST: MovieShowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieShowing = await _context.MovieShowings.FindAsync(id);
            _context.MovieShowings.Remove(movieShowing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
