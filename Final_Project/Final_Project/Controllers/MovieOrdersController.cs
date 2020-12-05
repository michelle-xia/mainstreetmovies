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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;


namespace Final_Project.Controllers
{
    [Authorize(Roles = "Customer,Employee,Manager")]
    public class MovieOrdersController : Controller
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public MovieOrdersController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: MovieOrders
        [Authorize(Roles = "Manager,Employee,Customer")]
        public IActionResult Index()
        {
            List<MovieOrder> movieOrders;


            if (User.IsInRole("Manager") || User.IsInRole("Employee"))
            {
                movieOrders = _context.MovieOrders
                                .Include(o => o.Customer)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ToList();

            }
            else //user is a customer, so only display their records
            {
                movieOrders = _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Where(r => r.Customer.UserName == User.Identity.Name)
                                .Include(r => r.Customer)
                                .Include(o => o.Recipient)
                                .ThenInclude(r => r.OrdersReceived)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ToList();
            }

            foreach (MovieOrder movieOrder in movieOrders)
            {
                if (movieOrder.OrderStatus != Status.Cancelled)
                {
                    foreach (Ticket t in movieOrder.Tickets)
                    {
                        if (t.MovieShowing != null)
                        {
                            if (t.MovieShowing.StartTime < DateTime.Now.AddHours(1))
                            {
                                movieOrder.OrderStatus = Status.Past;
                                _context.Update(movieOrder);
                                _context.SaveChangesAsync();
                                break;
                            }
                        }
                    }
                }
            }

            return View(movieOrders);
        }


        [Authorize(Roles = "Manager,Employee")]
        public IActionResult SelectCustomer()
        {
            ViewBag.AllCustomers = GetAllCustomers();
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Manager,Employee,Customer")]
        public async Task<IActionResult> ConfirmOrder(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a movie order to view!" });
            }
            MovieOrder movieOrder;
            if (User.IsInRole("Customer"))
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Where(r => r.Customer.UserName == User.Identity.Name)
                                .Include(o => o.Customer)
                                .ThenInclude(o => o.MovieOrders)
                                .ThenInclude(o => o.Tickets)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Tickets)
                                .ThenInclude(o => o.MovieOrder)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }
            else
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Include(o => o.Customer)
                                .ThenInclude(o => o.MovieOrders)
                                .ThenInclude(o => o.Tickets)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Tickets)
                                .ThenInclude(o => o.MovieOrder)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }

            if (movieOrder == null)
            {
                return View("Error", new String[] { "This order was not found!" });
            }

            if (User.IsInRole("Manager") == false && User.IsInRole("Employee") == false && movieOrder.Customer.UserName != User.Identity.Name)
            {
                return View("Error", new String[] { "You are not authorized to view this order!" });
            }

            if (movieOrder.Tickets.Count() < 1)
            {
                return View("Error", new string[] { "You cannot checkout without any tickets on your order" });
            }

            if (movieOrder.UsingPopcornPoints == true && movieOrder.Tickets.Count() * 100 > movieOrder.Customer.TotalPopcornPoints)
            {
                return View("Error", new string[] { "You don't have enough popcorn points to pay for this order" });
            }

            foreach (Ticket t in movieOrder.Tickets)
            {
                if ((t.MovieShowing.Movie.MPAARating == MPAARatings.R || t.MovieShowing.Movie.MPAARating == MPAARatings.NC17) && movieOrder.Customer.Age < 18)
                {
                    return View("Error", new string[] { "You must be at least 18 to purchase tickets to an R or NC-17 showing" });
                }

                if (movieOrder.UsingPopcornPoints == true && t.MovieShowing.IsSpecial == true)
                {
                    return View("Error", new string[] { "You cannot use popcorn points to purchase a special showing" });
                }
            }

            if (CheckConflictingStarts(movieOrder) == true)
            {
                return View("Error", new String[] { "You cannot purchase tickets to movies with conflicting showtimes in one order." });
            }

            if (CheckDuplicateMovies(movieOrder) == true)
            {
                return View("Error", new string[] { "You cannot purchase tickets to more than " +
                        "one showing of the same movie in a single order." });
            }

            if (CheckGiftUnder18(movieOrder) == true)
            {
                return View("Error", new string[] { "You cannot purchase " +
                        "these tickets for a customer under 18." });
            }

            //Check popcorn points
            /*List<String> contained = new List<String>();
            foreach (Ticket ticket in movieOrder.Tickets)
            {
                if (ticket.ValidSeat() == false)
                {
                    return View("Error", new string[] { "One of the seats you are trying to purchase" +
                        " has already been purchased" });
                }
                if (contained.Contains(ticket.Seat))
                {
                    return View("Error", new string[] { "You cannot purchase duplicate tickets" +
                        " for the same seat" });
                }
                contained.Add(ticket.Seat);
            }*/
            

            if (movieOrder.Customer.Age >= 60)
            {
                movieOrder.SeniorDiscount();
                movieOrder.OrderDiscount = true;
            }
            else
            {
                movieOrder.OrderDiscount = false;
            }

            return View(movieOrder);
        }


        [HttpPost]
        [Authorize(Roles = "Manager,Employee,Customer")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            MovieOrder movieOrder;
            if (User.IsInRole("Customer"))
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Where(r => r.Customer.UserName == User.Identity.Name)
                                .Include(o => o.Customer)
                                .ThenInclude(o => o.MovieOrders)
                                .ThenInclude(o => o.Tickets)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing) 
                                .ThenInclude(o => o.Movie)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Tickets)
                                .ThenInclude(o => o.MovieOrder)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }
            else
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Include(o => o.Customer)
                                .ThenInclude(o => o.MovieOrders)
                                .ThenInclude(o => o.Tickets)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Tickets)
                                .ThenInclude(o => o.MovieOrder)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }

            if (movieOrder == null)
            {
                return View("Error", new String[] { "This order was not found!" });
            }

            if (User.IsInRole("Manager") == false && User.IsInRole("Employee") == false && movieOrder.Customer.UserName != User.Identity.Name)
            {
                return View("Error", new string[] { "You are not authorized to view this order!" });
            }

            /*List<String> contained = new List<String>();
            foreach (Ticket ticket in movieOrder.Tickets)
            {
                if (ticket.ValidSeat() == false)
                {
                    return View("Error", new string[] { "One of the seats you are trying to purchase" +
                        " has already been purchased" });
                }
                if (contained.Contains(ticket.Seat))
                {
                    return View("Error", new string[] { "You cannot purchase duplicate tickets" +
                        " for the same seat" });
                }
                contained.Add(ticket.Seat);
            }
            */
            movieOrder.ConfirmationNumber = Utilities.GetConfirmationNumber.GetNextConfNumber(_context);

            if (movieOrder.Customer.Age >= 60)
            {
                int count = 0;
                foreach (Ticket t in movieOrder.Tickets)
                {
                    if (count == 2)
                    {
                        break;
                    }
                    if (t.MovieShowing.IsSpecial == true)
                    {
                        continue;
                    }
                    t.TicketPrice = t.TicketPrice - 2;
                    t.SeniorDiscount = true;
                    count += 1;
                }
                movieOrder.OrderDiscount = true;
            }
            else
            {
                movieOrder.OrderDiscount = false;
            }

            movieOrder.UpdatePopcornPoints();

            movieOrder.isConfirmed = true;

            movieOrder.OrderStatus = Status.Future;

            if (movieOrder.UsingPopcornPoints == true)
            {
                foreach (Ticket t in movieOrder.Tickets)
                {
                    t.TicketPrice = 0;
                }
            }

            _context.Update(movieOrder);
            _context.SaveChanges();

            return RedirectToAction("Thanks", "MovieOrders", new { id = movieOrder.MovieOrderID });
        }

        [Authorize(Roles = "Customer,Employee,Manager")]
        public async Task<IActionResult> Thanks(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a movie order to view!" });
            }

            MovieOrder movieOrder;
            if (User.IsInRole("Customer"))
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Where(r => r.Customer.UserName == User.Identity.Name)
                                .Include(o => o.Customer)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }
            else
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Include(o => o.Customer)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }

            if (movieOrder == null)
            {
                return View("Error", new String[] { "This order was not found!" });
            }

            if (User.IsInRole("Manager") == false && User.IsInRole("Employee") == false && movieOrder.Customer.UserName != User.Identity.Name)
            {
                return View("Error", new string[] { "You are not authorized to view this order!" });
            }

            // sets up and creates the confirmation email
            String EmailSubject = $"Confirmation for movie order {movieOrder.MovieOrderID}";
            String EmailBody = $"The confirmation number is {movieOrder.ConfirmationNumber}.\nYour movie order includes tickets for the following movie showings:\n";
            foreach (Ticket t in movieOrder.Tickets)
            {
                EmailBody += $"{t.MovieShowing.FullShowingName}\n";
            }
            Utilities.Email.SendEmail(movieOrder.Customer.Email, EmailSubject, EmailBody);
            if (movieOrder.isGift == true)
            {
                Utilities.Email.SendEmail(movieOrder.Recipient.Email, EmailSubject, EmailBody);
            }

            return View(movieOrder);
        }

        // GET: MovieOrders/Details/5
        [Authorize(Roles = "Customer,Employee,Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a movie order to view!" });
            }
            MovieOrder movieOrder;
            if (User.IsInRole("Customer"))
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Where(r => r.Customer.UserName == User.Identity.Name)
                                .Include(o => o.Customer)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }
            else
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Include(o => o.Customer)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }
            if (movieOrder == null)
            {
                return View("Error", new String[] { "This order was not found!" });
            }

            if (User.IsInRole("Manager") == false && User.IsInRole("Employee") == false && movieOrder.Customer.UserName != User.Identity.Name)
            {
                return View("Error", new string[] { "You are not authorized to view this order!" });
            }

            if (movieOrder.Customer.Age >= 60)
            {
                movieOrder.OrderDiscount = true;
            }

            return View(movieOrder);
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }


        // POST: MovieOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Employee,Manager")]
        public async Task<IActionResult> Create(MovieOrder movieOrder, int SelectedMovie, String SelectedCustomer)
        {
            if (SelectedCustomer != null)
            {
                AppUser customer = _context.Users
                                        .Where(t => t.Email == SelectedCustomer)
                                        .Include(t => t.MovieOrders)
                                        .ThenInclude(t => t.Tickets)
                                        .FirstOrDefault();
                if (customer == null)
                {
                    return View("Error", new String[] { "This customer was not found!" });
                }
                //movieOrder.ConfirmationNumber = Utilities.GetConfirmationNumber.GetNextConfNumber(_context);
                movieOrder.DateOrdered = DateTime.Now;
                movieOrder.Customer = customer;

                if (movieOrder.GiftRecipient != null)
                {
                    AppUser giftRecipient = _context.Users.FirstOrDefault(u => u.Email == movieOrder.GiftRecipient);

                    List<AppUser> allCustomers = AllCustomers();

                    if (giftRecipient != default && allCustomers.Contains(giftRecipient))
                    {
                        movieOrder.Recipient = giftRecipient;
                        movieOrder.isGift = true;
                    }
                    else
                    {
                        ViewBag.GiftError = "Giftee email is not in the database.";
                        ViewBag.AllCustomers = GetAllCustomers();
                        ViewBag.AllMovies = GetAllMovies();
                        return View("SelectCustomer", movieOrder);
                    }
                }

                if (ModelState.IsValid == false)
                {
                    ViewBag.AllCustomers = GetAllCustomers();
                    ViewBag.AllMovies = GetAllMovies();
                    return View("SelectCustomer", movieOrder);
                }

                _context.Add(movieOrder);
                await _context.SaveChangesAsync();
            }
            else
            {
                //set the value of Confirmation Number
                //movieOrder.ConfirmationNumber = Utilities.GetConfirmationNumber.GetNextConfNumber(_context);
                movieOrder.DateOrdered = DateTime.Now;
                movieOrder.Customer = _context.Users
                    .Include(t => t.MovieOrders)
                    .ThenInclude(t => t.Tickets)
                    .FirstOrDefault(u => u.UserName == User.Identity.Name);

                if (movieOrder.GiftRecipient != null)
                {
                    AppUser giftRecipient = _context.Users.FirstOrDefault(u => u.Email == movieOrder.GiftRecipient);
                    List<AppUser> allCustomers = AllCustomers();

                    if (giftRecipient != default && allCustomers.Contains(giftRecipient))
                    {
                        movieOrder.Recipient = giftRecipient;
                        movieOrder.isGift = true;
                    }
                    else
                    {
                        ViewBag.GiftError = "Giftee email is not a customer in the database.";
                        ViewBag.AllMovies = GetAllMovies();
                        return View(movieOrder);
                    }
                }

                if (ModelState.IsValid == false)
                {
                    ViewBag.AllMovies = GetAllMovies();
                    return View(movieOrder);
                }

                _context.Add(movieOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Create", "Tickets", new { MovieOrderID = movieOrder.MovieOrderID, SelectedMovieId = SelectedMovie });
        }


        // GET: MovieOrders/Edit/5
        [Authorize(Roles = "Customer,Employee,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ErrorMessage = TempData["shortMessage"];
            if (id == null)
            {
                return View("Error", new String[] { "Please specify an order to edit" });
            }

            MovieOrder movieOrder;
            if (User.IsInRole("Customer"))
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Include(o => o.Customer)
                                .Where(r => r.Customer.UserName == User.Identity.Name)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }
            else
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Include(o => o.Customer)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }

            if (movieOrder == null)
            {
                return View("Error", new String[] { "This order was not found in the database!" });
            }

            if (movieOrder.isConfirmed == true)
            {
                return View("Error", new String[] { "This order has already been confirmed and cannot be edited." });
            }

            if (movieOrder.OrderStatus == Status.Cancelled)
            {
                return View("Error", new String[] { "This order has already been canceled and cannot be edited." });
            }

            if (User.IsInRole("Customer") && movieOrder.Customer.UserName != User.Identity.Name)
            {
                return View("Error", new String[] { "You are not authorized to edit this order!" });
            }

            if (movieOrder.Customer.Age >= 60)
            {
                movieOrder.SeniorDiscount();
                movieOrder.OrderDiscount = true;
            }
            else
            {
                movieOrder.OrderDiscount = false;
            }

            return View(movieOrder);
        }


        // POST: MovieOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Manager,Employee")]
        public async Task<IActionResult> Edit(int id, MovieOrder movieOrder)
        {
            if (id != movieOrder.MovieOrderID)
            {
                return View("Error", new String[] { "There was a problem editing this order. Try again!" });
            }

            if (ModelState.IsValid == false)
            {
                return View(movieOrder);
            }
            MovieOrder dbMovieOrder = _context.MovieOrders.Include(r => r.Tickets)
                                .Include(o => o.Customer)
                                .ThenInclude(o => o.MovieOrders)
                                .ThenInclude(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .Include(o => o.Recipient)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .FirstOrDefault(m => m.MovieOrderID == id);

            int req = dbMovieOrder.Tickets.Count() * 100;
            int points = dbMovieOrder.Customer.TotalPopcornPoints;
            if (movieOrder.UsingPopcornPoints == true)
            {
                if (req > points)
                {
                    return View("Error", new string[] { "You don't have enough popcorn points to pay for this order." });
                }
            }

            foreach (Ticket t in movieOrder.Tickets)
            {
                if ((t.MovieShowing.Movie.MPAARating == MPAARatings.R || t.MovieShowing.Movie.MPAARating == MPAARatings.NC17) && movieOrder.Customer.Age < 18)
                {
                    return View("Error", new string[] { "You must be at least 18 to purchase tickets to an R or NC-17 showing." });
                }
            }

            if (CheckGiftUnder18(movieOrder) == true)
            {
                return View("Error", new string[] { "You cannot purchase" +
                        "these tickets for a customer under 18." });
            }

            if (CheckConflictingStarts(movieOrder) == true)
            {
                return View("Error", new String[] { "You cannot purchase tickets to movies with conflicting showtimes in one order." });
            }

            if (CheckDuplicateMovies(movieOrder) == true)
            {
                return View("Error", new string[] { "You cannot purchase tickets to more than " +
                        "one showing of the same movie in a single order." });
            }

            //if code gets this far, update the record
            try
            {
                //find the record in the database
                MovieOrder dbmovieOrder = _context.MovieOrders.Find(movieOrder.MovieOrderID);

                //update the notes
                dbmovieOrder.UsingPopcornPoints = movieOrder.UsingPopcornPoints;


                _context.Update(dbmovieOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error updating this order!", ex.Message });
            }

            //send the user to the Confirmation page.
            return RedirectToAction("ConfirmOrder", new { id = movieOrder.MovieOrderID });
        }


        [Authorize(Roles = "Customer,Manager,Employee")]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify an order to cancel" });
            }
            MovieOrder movieOrder;

            if (User.IsInRole("Customer"))
            {
                movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Where(r => r.Customer.UserName == User.Identity.Name)
                                .Include(o => o.Customer)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);
            }
            else
            {
                movieOrder = await _context.MovieOrders
                                   .Include(r => r.Tickets)
                                   .Include(o => o.Customer)
                                   .Include(o => o.Tickets)
                                   .ThenInclude(o => o.MovieShowing)
                                   .ThenInclude(o => o.Movie)
                                   .FirstOrDefaultAsync(m => m.MovieOrderID == id);

            }

            if (movieOrder == null)
            {
                return View("Error", new String[] { "This order was not found in the database!" });
            }

            if (movieOrder.OrderStatus == Status.Cancelled)
            {
                return View("Error", new String[] { "This order has already been canceled and cannot be edited." });
            }

            if (User.IsInRole("Customer") && movieOrder.Customer.UserName != User.Identity.Name)
            {
                return View("Error", new String[] { "You are not authorized to cancel this order!" });
            }

            foreach (Ticket t in movieOrder.Tickets)
            {
                if (t.MovieShowing.StartTime < DateTime.Now.AddHours(1))
                {
                    movieOrder.OrderStatus = Status.Past;
                    _context.Update(movieOrder);
                    await _context.SaveChangesAsync();
                    break;
                }
            }

            if (movieOrder.OrderStatus == Status.Past)
            {
                return View("Error", new String[] { "This order is past and cannot be canceled." });
            }

            return View(movieOrder);

        }


        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Manager,Employee")]

        public async Task<IActionResult> CancelConfirmed(int id, MovieOrder movieOrder)
        {
            if (id != movieOrder.MovieOrderID)
            {
                return View("Error", new String[] { "There was a problem cancelling this order. Try again!" });
            }

            if (ModelState.IsValid == false)
            {
                return View(movieOrder);
            }

            //find the record in the database
            MovieOrder dbmovieOrder = _context.MovieOrders.Where(r => r.MovieOrderID == id)
                .Include(o => o.Customer)
                .Include(o => o.Tickets)
                .FirstOrDefault();

            //if code gets this far, update the record
            try
            {
                //update the notes
                dbmovieOrder.OrderStatus = Status.Cancelled;

                if (dbmovieOrder.UsingPopcornPoints == true)
                {
                    dbmovieOrder.PopcornPointsEarned = 0;//100 * dbmovieOrder.Tickets.Count();
                    dbmovieOrder.UsingPopcornPoints = false;
                }
                else
                {
                    int temp = 0;
                    foreach (Ticket t in dbmovieOrder.Tickets)
                    {
                        temp += Decimal.ToInt32(t.TicketPrice);
                    }
                    dbmovieOrder.PopcornPointsEarned -= temp;
                }

                _context.Update(dbmovieOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error cancelling this order!", ex.Message });
            }
            finally
            {
                Utilities.Email.SendEmail(dbmovieOrder.Customer.Email, $"Your movie order {dbmovieOrder.MovieOrderID} has been cancelled.", "We are sorry you had to cancel! 😢");
                if (dbmovieOrder.isGift == true)
                {
                    Utilities.Email.SendEmail(dbmovieOrder.Recipient.Email, $"Your movie order {dbmovieOrder.MovieOrderID} has been cancelled.", "We are sorry you had to cancel! 😢");
                }
            }

            return RedirectToAction("Index", "MovieOrders");
        }

        [Authorize(Roles ="Customer")]
        public IActionResult GetRecommendations()
        {
            List<Movie> movies = _context.Movies
                .Include(m => m.MovieShowings)
                .Include(t => t.Genre)
                .ToList();
            AppUser user = _context.Users
                .Where(t => t.Email == User.Identity.Name)
                                        .Include(t => t.MovieOrders)
                                        .ThenInclude(t => t.Tickets)
                                        .ThenInclude(t => t.MovieShowing)
                                        .ThenInclude(t => t.Movie)
                                        .ThenInclude(t => t.Genre)
                                        .FirstOrDefault();
            List<Movie> userMovies = new List<Movie>();
            foreach (MovieOrder mo in user.MovieOrders)
            {
                foreach (Ticket t in mo.Tickets)
                {
                    userMovies.Add(t.MovieShowing.Movie);
                }
            }
           
            if (userMovies.Count() == 0)
            {
                return View("Error", new String[] { "You haven't watched any movies yet." });
            }
            if (movies.Count() == 0)
            {
                return View("Error", new String[] { "There are no movies at this time." });
            }
            userMovies = userMovies.Distinct().ToList();
            Double score = 0;
            List<Double> simScores = new List<double>();
            foreach (Movie mov in userMovies)
            {
                foreach (Movie allmov in movies)
                {
                    score = CalculateSimilarity(mov.MovieOverview, allmov.MovieOverview);
                    if (allmov.Genre.GenreName == mov.Genre.GenreName)
                    {
                        score += 1;
                    }
                    if (allmov.Similarity == 0)
                    {
                        allmov.Similarity = score;
                    }
                    else
                    {
                        allmov.Similarity += score;
                    }
                    simScores.Add(score);
                }
            }
            movies.RemoveAll(t => userMovies.Contains(t));
            movies = movies.OrderByDescending(t => t.Similarity).ToList();
            if (movies.Count() < 5)
            {
                return View(movies);
            }

            List<Movie> final = new List<Movie>();
            for (int i =0; i<5;i++)
            {
                final.Add(movies[i]);
            }

            ViewBag.RecCount = final.Count();
            return View(final.OrderByDescending(t => t.Similarity).ToList());
        }


        //NOTE: Non Action Result Methods
        public Boolean CheckDuplicateMovies(MovieOrder movieOrder)
        {
            // Get all unique showings
            HashSet<MovieShowing> showingSet = new HashSet<MovieShowing>();
            foreach (Ticket t in movieOrder.Tickets)
            {
                showingSet.Add(t.MovieShowing);
            }

            List<MovieShowing> showings = showingSet.ToList();

            // Loop through unique showings and find all the movies
            // for each showing
            List<String> movies = new List<String>();
            foreach (MovieShowing ms in showings)
            {
                movies.Add(ms.Movie.Title);
            }

            //Get a count of repetitions of each movie
            var q = from x in movies
                    group x by x into g
                    let count = g.Count()
                    orderby count descending
                    select new { Value = g.Key, Count = count };

            // Check for any movie duplicates
            foreach (var x in q)
            {
                if (x.Count > 1)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean CheckConflictingStarts(MovieOrder movieOrder)
        {
            HashSet<MovieShowing> uniqueShowings = new HashSet<MovieShowing>();
            foreach (Ticket t in movieOrder.Tickets)
            {
                uniqueShowings.Add(t.MovieShowing);
            }

            List<MovieShowing> uniqueShow = uniqueShowings.ToList().OrderBy(c => c.StartTime).ToList();

            List<Ticket> ticks = new List<Ticket>();

            foreach (MovieShowing ms in uniqueShow)
            {
                ticks.Add(ms.Tickets.First());
            }

            for (int i = 0; i < ticks.Count() - 1; i++)
            {
                if ((ticks[i].MovieShowing.EndTime > ticks[i + 1].MovieShowing.StartTime) && (ticks[i].MovieShowing != ticks[i + 1].MovieShowing))
                {
                    return true;
                }

            }

            return false;
        }

        public Boolean CheckGiftUnder18(MovieOrder movieOrder)
        {
            if (movieOrder.isGift == true)
            {
                AppUser giftee = movieOrder.Recipient;
                foreach (Ticket t in movieOrder.Tickets)
                {
                    if (t.MovieShowing.Movie.MPAARating == MPAARatings.NC17 || t.MovieShowing.Movie.MPAARating == MPAARatings.R)
                    {
                        if (giftee.Age < 18)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public Boolean CheckIneligibleDiscounts(MovieOrder movieOrder)
        {
            foreach (Ticket t in movieOrder.Tickets)
            {
                if (t.MovieShowing.IsSpecial == true)
                {
                    return true;
                }
            }
            return false;
        }


        private SelectList GetAllMovies()
        {
            //create a list for all the courses
            List<Movie> allMovies = _context.Movies
                .Include(m => m.MovieShowings)
                .ToList();
            List<Movie> movies = new List<Movie>();
            foreach (Movie m in allMovies)
            {
                foreach (MovieShowing ms in m.MovieShowings)
                {
                    if (ms.IsPublished == true)
                    {
                        movies.Add(m);
                    }
                }
            }
            allMovies = movies.Distinct().ToList();

            //use the constructor on select list to create a new select list with the options
            SelectList slAllMovies = new SelectList(allMovies.OrderBy(m => m.Title), nameof(Movie.MovieId), nameof(Movie.Title));

            return slAllMovies;
        }


        public List<AppUser> AllCustomers()
        {
            //Create a list of roles that will need to be edited
            List<RoleEditModel> roles = new List<RoleEditModel>();

            //this is a list of all the users who ARE in this role (members)
            List<AppUser> members = new List<AppUser>();

            List<AppUser> allCustomers = _context.Users.ToList();

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

            SelectList slAllCustomers = new SelectList(members.OrderBy(m => m.Email), nameof(AppUser.Email), nameof(AppUser.Email));

            return slAllCustomers;

        }

        int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }
    }
}