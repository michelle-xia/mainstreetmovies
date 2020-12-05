using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Final_Project.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ReportController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ReportController(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Handles Initial Request
        public IActionResult Totals()
        {
            ViewBag.AllMovies = GetAllMovies();

            return View();
        }

        // Handles Later Request with Filters
        public IActionResult FilteredTotals(TotalReportFilterViewModel trfvm)
        {
            // this only works because each ticket is tied to a seat
            List<Ticket> seats;
            var query = from s in _context.Tickets
                        .Include(r => r.MovieOrder)
                        .ThenInclude(o => o.Customer)
                        .Include(r => r.MovieShowing)
                        .ThenInclude(o => o.Movie)
                        select s;
                
            //Filter by movie
            if (trfvm.MovieId != 0)
            {
                query = query.Where(s => s.MovieShowing.Movie.MovieId == trfvm.MovieId);
            }
            //Filter by MPAA rating
            if (trfvm.MPAARating != null)
            {
                switch (trfvm.MPAARating)
                {
                    case MPAARatings.G:
                        query = query.Where(s => s.MovieShowing.Movie.MPAARating == MPAARatings.G);
                        break;
                    case MPAARatings.PG:
                        query = query.Where(s => s.MovieShowing.Movie.MPAARating == MPAARatings.PG);
                        break;
                    case MPAARatings.PG13:
                        query = query.Where(s => s.MovieShowing.Movie.MPAARating == MPAARatings.PG13);
                        break;
                    case MPAARatings.NC17:
                        query = query.Where(s => s.MovieShowing.Movie.MPAARating == MPAARatings.NC17);
                        break;
                    case MPAARatings.R:
                        query = query.Where(s => s.MovieShowing.Movie.MPAARating == MPAARatings.R);
                        break;
                    case MPAARatings.Unrated:
                        query = query.Where(s => s.MovieShowing.Movie.MPAARating == MPAARatings.Unrated);
                        break;
                    default:
                        break;
                }
            }
            //Filter by date range 
            //Case where start exists and end is indefinite
            if (trfvm.DateRangeStart != null || trfvm.DateRangeEnd != null)
            {
                switch (trfvm.ChoiceTime)
                {
                    case DateOrTime.DateRange:
                        /*if (trfvm.DateRangeStart != null && trfvm.DateRangeEnd == null)
                        {
                            DateTime start = (DateTime)trfvm.DateRangeStart;
                            query = query.Where(s => s.MovieShowing.StartTime.Date.AddDays(1) > start.Date);
                        }
                        else if (trfvm.DateRangeStart == null && trfvm.DateRangeEnd != null)
                        {
                            DateTime end = (DateTime)trfvm.DateRangeEnd;
                            query = query.Where(s => s.MovieShowing.EndTime.Date < end.Date.AddDays(1));
                        }
                        else if (trfvm.DateRangeStart != null && trfvm.DateRangeEnd != null)
                        {
                            DateTime start = (DateTime)trfvm.DateRangeStart;
                            DateTime end = (DateTime)trfvm.DateRangeEnd;
                            query = query.Where(s => s.MovieShowing.StartTime.Date.AddDays(1) > start.Date && s.MovieShowing.EndTime.Date < end.Date.AddDays(1));
                        }*/
                        DateTime start = trfvm.DateRangeStart ?? new DateTime(1900, 1, 1);
                        DateTime end = trfvm.DateRangeEnd ?? new DateTime(2200, 1, 1);
                        if (start > end)
                        {
                            ViewBag.ErrorMessage = "Invalid Time Frame";
                            ViewBag.AllMovies = GetAllMovies();
                            return View("Totals", trfvm);
                        }
                        query = query.Where(mv => mv.MovieShowing.StartTime.Date.AddDays(1) > start.Date).Where(mv => mv.MovieShowing.StartTime.Date < end.Date.AddDays(1));
                        break;
                    case DateOrTime.TimeOfDay:
                        if (trfvm.DateRangeStart != null && trfvm.DateRangeEnd == null)
                        {
                            DateTime startTime = (DateTime)trfvm.DateRangeStart;
                            query = query.Where(s => s.MovieShowing.StartTime.TimeOfDay >= startTime.TimeOfDay);
                        }
                        else if (trfvm.DateRangeStart == null && trfvm.DateRangeEnd != null)
                        {
                            DateTime endTime = (DateTime)trfvm.DateRangeEnd;
                            query = query.Where(s => s.MovieShowing.EndTime.TimeOfDay <= endTime.TimeOfDay);
                        }
                        else if (trfvm.DateRangeStart != null && trfvm.DateRangeEnd != null)
                        {
                            DateTime startTime = (DateTime)trfvm.DateRangeStart;
                            DateTime endTime = (DateTime)trfvm.DateRangeEnd;

                            if (startTime > endTime)
                            {
                                ViewBag.ErrorMessage = "Invalid Time Frame";
                                ViewBag.AllMovies = GetAllMovies();
                                return View("Totals", trfvm);
                            }

                            query = query.Where(s => (s.MovieShowing.StartTime.TimeOfDay >= startTime.TimeOfDay) && (s.MovieShowing.EndTime.TimeOfDay <= endTime.TimeOfDay));
                        }
                        break;
                    default:
                        break;
                }
                        
            }

            List<Ticket> final = new List<Ticket>();          

            seats = query.ToList();
            foreach (Ticket t in seats)
            {
                if (t.MovieShowing != null)
                {
                    if (t.MovieShowing.IsPublished == true)
                    {
                        if (t.MovieOrder.OrderStatus == Status.Past || t.MovieOrder.OrderStatus == Status.Future)
                        {
                            final.Add(t);
                        }
                    }
                }               
            }
            Decimal totalRev = 0;
            var group = final.GroupBy(t => t.MovieOrder.MovieOrderID);
            foreach (var g in group)
            {
                MovieOrder mo = g.ToList().First().MovieOrder;
                if (mo.UsingPopcornPoints != true )
                {
                    totalRev += mo.SubTotal;
                }
            }
            ReportViewModel rvm = new ReportViewModel();
            rvm.SeatsSold = final.Count();
            rvm.TotalRevenue = totalRev;
            
            ViewBag.TotalSeats = final.Count();
            ViewBag.TotalRevenue = totalRev.ToString("C");
            ViewBag.AllMovies = GetAllMovies();
            return View("ReportedTotals", rvm);
        }

        //Return view with filtered totals
        public IActionResult ReportedTotals(ReportViewModel rvm)
        {
           
            List<Ticket> ticks = _context.Tickets
                    .Include(r => r.MovieOrder)
                    .ThenInclude(o => o.Customer)
                    .Include(r => r.MovieShowing)
                    .ThenInclude(o => o.Movie)
                    .ToList();
            List<Ticket> seats = new List<Ticket>();
            foreach (Ticket t in ticks)
            {
                if (t.MovieShowing != null)
                {
                    if (t.MovieShowing.IsPublished == true)
                    {
                        if (t.MovieOrder.OrderStatus == Status.Past || t.MovieOrder.OrderStatus == Status.Future)
                        {
                            seats.Add(t);
                        }
                    }
                }               
            }
            Decimal totalRev = 0;
            var group = seats.GroupBy(t => t.MovieOrder.MovieOrderID);
            foreach (var g in group)
            {
                MovieOrder mo = g.ToList().First().MovieOrder;
                if (mo.UsingPopcornPoints != true)
                {
                    totalRev += mo.SubTotal;
                }
            }

            rvm.TotalRevenue = totalRev;
            rvm.SeatsSold = seats.Count();
            
            return View(rvm);
        }

        //Handles initial request
        public IActionResult Customers()
        {
            // this only works because each ticket is tied to a seat
            List<Ticket> seats;
            seats = _context.Tickets
                        .Include(r => r.MovieOrder)
                        .ThenInclude(o => o.Customer)
                        .Include(r => r.MovieShowing)
                        .ThenInclude(o => o.Movie)
                        .ToList();
            ViewBag.AllCustomers = GetAllCustomers();
            List<Ticket> final = new List<Ticket>();
            foreach (Ticket t in seats)
            {
                if (t.MovieShowing != null)
                {
                    if (t.MovieShowing.IsPublished == true)
                    {
                        if (t.MovieOrder.OrderStatus == Status.Past || t.MovieOrder.OrderStatus == Status.Future)
                        {
                            final.Add(t);
                        }
                    }
                }               
            }
            Decimal totalRev = 0;
            var group = final.GroupBy(t => t.MovieOrder.MovieOrderID);
            foreach (var g in group)
            {
                MovieOrder mo = g.ToList().First().MovieOrder;
                if (mo.UsingPopcornPoints != true)
                {
                    totalRev += mo.SubTotal;
                }
            }
            seats.RemoveAll(t => t.MovieOrder.OrderStatus == Status.Unfinished);

            ViewBag.TotalSeats = final.Count();
            ViewBag.TotalRevenue = totalRev.ToString("C");
            ViewBag.AllMovies = GetAllMovies();
            return View(seats.OrderBy(s => s.MovieOrder.ConfirmationNumber));
        }

        //Handles later request with filters
        public IActionResult FilteredCustomers (String SelectedCustomer)
        {
            // this only works because each ticket is tied to a seat
            List<Ticket> seats;
            var query = from s in _context.Tickets
                        .Include(r => r.MovieOrder)
                        .ThenInclude(o => o.Customer)
                        .Include(r => r.MovieShowing)
                        .ThenInclude(o => o.Movie)
                        select s;
            if (SelectedCustomer != "" && SelectedCustomer != "All Members")
            {
                query = query.Where(s => s.MovieOrder.Customer.Email == SelectedCustomer);
            }
            ViewBag.AllCustomers = GetAllCustomers();
            seats = query.ToList();
            List<Ticket> final = new List<Ticket>();
            foreach (Ticket t in seats)
            {
                if (t.MovieShowing != null)
                {
                    if (t.MovieShowing.IsPublished == true)
                    {
                        if (t.MovieOrder.OrderStatus == Status.Past || t.MovieOrder.OrderStatus == Status.Future)
                        {
                            final.Add(t);
                        }
                    }
                }                
            }
            Decimal totalRev = 0;
            var group = final.GroupBy(t => t.MovieOrder.MovieOrderID);
            foreach (var g in group)
            {
                MovieOrder mo = g.ToList().First().MovieOrder;
                if (mo.UsingPopcornPoints != true)
                {
                    totalRev += mo.SubTotal;
                }
            }

            seats.RemoveAll(t => t.MovieOrder.OrderStatus == Status.Unfinished);

            ViewBag.TotalSeats = final.Count();
            ViewBag.TotalRevenue = totalRev.ToString("C");
            ViewBag.AllMovies = GetAllMovies();
            return View("Customers", seats.OrderBy(s => s.MovieOrder.ConfirmationNumber));
        }

        public IActionResult PopcornTickets()
        {
            List<Ticket> tickets;
            tickets = _context.Tickets
                               .Include(r => r.MovieOrder)
                               .ThenInclude(o => o.Customer)
                               .Where(r => r.MovieOrder.UsingPopcornPoints == true)
                               .Include(r => r.MovieShowing)
                               .ThenInclude(o => o.Movie)
                               .ToList();

            List<Ticket> final = new List<Ticket>();
            foreach (Ticket t in tickets)
            {
                if (t.MovieShowing != null)
                {
                    if (t.MovieShowing.IsPublished == true)
                    {
                        if (t.MovieOrder.OrderStatus == Status.Past || t.MovieOrder.OrderStatus == Status.Future)
                        {
                            final.Add(t);
                        }
                    }
                }               
            }
            return View(final);
        }

        public List<AppUser> AllCustomers()
        {

            //this is a list of all the users who ARE in this role (members)
            List<AppUser> members = new List<AppUser>();

            //loop through ALL the users and decide if they are in the role(member) or not (non-member)
            //every user will be evaluated for every role, so this is a SLOW chunk of code because
            //it accesses the database so many times
            foreach (AppUser user in _userManager.Users)
            {

                if (_userManager.IsInRoleAsync(user, "Customer").Result == true) //user is in the role
                {
                    members.Add(user);
                }
            }
            return members;
        }

        public SelectList GetAllCustomers()
        {

            List<AppUser> members = AllCustomers();
            //add a dummy entry so the user can select all movies
            AppUser SelectNone = new AppUser()
            {
                Email = "All Members",
                FirstName = "",
                LastName = "",
                Address = "",
                Birthday = DateTime.Now
            };
            members = members.OrderBy(t => t.Email).ToList();
            members.Insert(0, SelectNone);
            SelectList slAllCustomers = new SelectList(members, nameof(AppUser.Email), nameof(AppUser.Email));

            return slAllCustomers;

        }

        private SelectList GetAllMovies()
        {
            //create a list for all the courses
             List<Movie> allMovies = _context.Movies.ToList();

            //add a dummy entry so the user can select all movies
            Movie SelectNone = new Movie()
            {
                MovieId = 0,
                Title = "All Movies",
                ReleaseDate = DateTime.Today,
                MPAARating = MPAARatings.Unrated,
                MovieOverview = "",
                Actors = "",
                Runtime = 0,
            };
            allMovies.Add(SelectNone);

            //use the constructor on select list to create a new select list with the options
            SelectList slAllMovies = new SelectList(allMovies.OrderBy(m => m.MovieId), nameof(Movie.MovieId), nameof(Movie.Title));

            return slAllMovies;
        }
    }
}
