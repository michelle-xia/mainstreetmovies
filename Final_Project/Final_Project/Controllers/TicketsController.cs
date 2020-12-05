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


namespace Final_Project.Controllers
{
    [Authorize(Roles = "Manager,Employee,Customer")]

    public class TicketsController : Controller
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public IActionResult Index(int movieOrderID)
        {
            List<Ticket> t = _context.Tickets
                                    .Where(ms => ms.MovieShowing.IsPublished == true)
                                    .Include(ms => ms.MovieShowing)
                                    .ThenInclude(m => m.Movie)
                                    .Include(ms => ms.MovieOrder)
                                    .ThenInclude(ms => ms.Customer)
                                    .Include(ms => ms.MovieOrder)
                                    .ThenInclude(ms => ms.Tickets)
                                    .Where(ms => ms.MovieOrder.MovieOrderID == movieOrderID)
                                    .ToList();
            var group = t.GroupBy(t => t.MovieOrder.MovieOrderID);

            foreach (var g in group)
            {
                MovieOrder mo = g.ToList().First().MovieOrder;
                if (mo.Customer.Age >= 60)
                {
                    mo.SeniorDiscount();
                }
            }

            return View(t);
        }


        // GET: Tickets/Create
        public IActionResult Create(int movieOrderID, int? SelectedMovieId)
        {
            Ticket t = new Ticket();

            //find the registration that should be associated with this registration
            MovieOrder dbMovieOrder = _context.MovieOrders.Find(movieOrderID);

            //set the new registration detail's registration equal to the registration you just found
            t.MovieOrder = dbMovieOrder;
            if (SelectedMovieId != null && SelectedMovieId != 0)
            {
                //populate the ViewBag with a list of existing showings
                ViewBag.AllShowings = GetAllShowings((int)SelectedMovieId);
            }
            else
            {
                ViewBag.AllShowings = GetAllShowings();
            }

            //pass the newly created registration detail to the view
            return View(t);
        }


        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket, int SelectedShowing)
        {
            
            if (SelectedShowing == 0)
            {
                return View("Error", new String[] { "You didn't select a showing." });
            }
            if (ModelState.IsValid == false)
            {
                ViewBag.AllShowings = GetAllShowings();
                return View(ticket);
            }
            MovieShowing dbMovieShowing = _context.MovieShowings
                                            .Where(ms => ms.IsPublished == true)
                                            .Include(ms => ms.Tickets)
                                            .ThenInclude(t => t.MovieOrder)
                                            .ThenInclude(t => t.Customer)
                                            .Include(t => t.Movie)
                                            .Where(ms => ms.MovieShowingID == SelectedShowing)
                                            .First();
            Price dbPrice = _context.Prices.First();
            
            ticket.MovieShowing = dbMovieShowing;

            Boolean inOrder = false;

            foreach (Ticket t in ticket.MovieOrder.Tickets)
            {
                if (t.Seat == ticket.Seat)
                {
                    inOrder = true;
                }
            }

            if (ticket.MovieShowing.StartTime < DateTime.Now)
            {
                ViewBag.AllShowings = GetAllShowings(ticket.MovieShowing.Movie.MovieId);
                ViewBag.ErrorMessage = "That showing is in the past.";
                return View(ticket);
            }

            if (ticket.ValidSeat() == false || inOrder == true)
            {
                ViewBag.AllShowings = GetAllShowings(ticket.MovieShowing.Movie.MovieId);
                ViewBag.ErrorMessage = "Seat Unavailable.";
                return View(ticket);
            }

            MovieOrder dbMovieOrder = _context.MovieOrders
                                        .Include(t => t.Tickets)
                                        .ThenInclude(t => t.MovieShowing)
                                        .Include(t => t.Customer)
                                        .Include(t => t.Recipient)
                                        .Where(t => t.MovieOrderID == ticket.MovieOrder.MovieOrderID)
                                        .FirstOrDefault();
            

            ticket.MovieOrder = dbMovieOrder;

            /*List<String> contained = new List<String>();
            foreach (Ticket ticket1 in ticket.MovieOrder.Tickets)
            {                
                contained.Add(ticket1.Seat);
            }
            if (contained.Contains(ticket.Seat))
            {
                ViewBag.AllShowings = GetAllShowings(ticket.MovieShowing.Movie.MovieId);
                ViewBag.ErrorMessage = "Can't add duplicate seat to order.";
                return View(ticket);
            }*/

            if (CheckConflictingStarts(dbMovieOrder) == true)
            {
                ViewBag.AllShowings = GetAllShowings(ticket.MovieShowing.Movie.MovieId);
                ViewBag.ErrorMessage = "You cannot purchase tickets to movies with conflicting showtimes in one order.";
                return View(ticket);
                //return View("Error", new String[] { "You cannot purchase tickets to movies with conflicting showtimes in one order." });
            }

            if (ticket.MovieOrder.Customer.Age < 18 &&(ticket.MovieShowing.Movie.MPAARating == MPAARatings.NC17 || ticket.MovieShowing.Movie.MPAARating == MPAARatings.R))
            {
                ViewBag.AllSeats = GetAllSeats(SelectedShowing);
                ViewBag.AllShowings = GetAllShowings(ticket.MovieShowing.Movie.MovieId);
                ViewBag.ErrorMessage = "You must be at least 18 to purchase tickets to an R or NC-17 showing.";
                return View(ticket);
            }


            List<String> MatineeDays = new List<String>()
            {
                "Monday", "Tuesday", "Wednesday", "Thursday", "Friday"
            };
            
            List<String> Weekdays = new List<String>()
            {
                "Monday", "Tuesday", "Wednesday", "Thursday"
            };

            DateTime start = ticket.MovieShowing.StartTime;
            TimeSpan noonCutoff = new TimeSpan(12, 0, 0);
            TimeSpan fiveCutoff = new TimeSpan(17, 0, 0);

            if (ticket.MovieShowing.IsSpecial == false)
            {
                if (MatineeDays.Contains(start.DayOfWeek.ToString()) && start.TimeOfDay < noonCutoff)
                {
                    ticket.Discount = ("Matinee");
                    ticket.TicketPrice = dbPrice.MatineePrice;
                }
                //Tuesday Price
                else if (start.DayOfWeek.ToString() == "Tuesday" && start.TimeOfDay >= noonCutoff && start.TimeOfDay < fiveCutoff)
                {
                    ticket.Discount = ("Discount Tuesday");
                    ticket.TicketPrice = dbPrice.TuesdayPrice;
                }
                //Weekday price
                else if ((Weekdays.Contains(start.DayOfWeek.ToString()) && start.TimeOfDay >= noonCutoff) || (start.DayOfWeek.ToString() == "Friday" && start.TimeOfDay <= noonCutoff))
                {
                    ticket.Discount = ("Weekday Price");
                    ticket.TicketPrice = dbPrice.WeekdayPrice;
                }
                //Weekend price
                else
                {
                    ticket.Discount = ("Weekend Price");
                    ticket.TicketPrice = dbPrice.WeekendPrice;
                }
            }
            else
            {
                ticket.Discount = "Special Showing";
                ticket.TicketPrice = dbPrice.SpecialPrice;
            }

            _context.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("ConfirmOrder", "MovieOrders", new { id = ticket.MovieOrder.MovieOrderID });
        }


        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ErrorMessage = TempData["shortMessage"];
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a ticket to edit!" });
            }

            Ticket ticket = await _context.Tickets
                                            .Where(ms => ms.MovieShowing.IsPublished == true)
                                            .Include(t => t.MovieShowing)
                                            .ThenInclude(t => t.Movie)
                                            .Include(t => t.MovieShowing)
                                            .ThenInclude(t => t.Tickets)
                                            .Include(t => t.MovieOrder)
                                            .ThenInclude(t => t.Customer)
                                            .FirstOrDefaultAsync(t => t.TicketId == id);

            ViewBag.AllSeats = GetAllSeats(ticket.MovieShowing.MovieShowingID);

            if (ticket == null)
            {
                return View("Error", new String[] { "This ticket was not found" });
            }
            
            return View(ticket);
        }

        //TODO: Remember to add the seat back to the list
        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return View("Error", new String[] { "There was a problem editing this ticket. Try again!" });
            }

            if (ModelState.IsValid == false)
            {
                ViewBag.AllSeats = GetAllSeats(ticket.MovieShowing.MovieShowingID);
                return View(ticket);
            }

            Ticket dbTicket;

            // Update record
            try
            {
                dbTicket = _context.Tickets
                                    .Where(ms => ms.MovieShowing.IsPublished == true)
                                    .Include(t => t.MovieShowing)
                                    .ThenInclude(t => t.Movie)
                                    .Include(t => t.MovieShowing)
                                    .ThenInclude(t => t.Tickets)
                                    .Include(t => t.MovieOrder)
                                    .ThenInclude(t => t.Tickets)
                                    .FirstOrDefault(t => t.TicketId == ticket.TicketId);

                MovieShowing dbShowing = _context.MovieShowings
                                                    .Include(t => t.Tickets)
                                                    .ThenInclude(t => t.MovieOrder)
                                                    .Include(t => t.Movie)
                                                    .Where(t => t.MovieShowingID == dbTicket.MovieShowing.MovieShowingID)
                                                    .FirstOrDefault();

                Boolean inOrder = false;

                foreach (Ticket t in dbTicket.MovieShowing.Tickets)
                {
                    if (t.MovieOrder.isConfirmed == true && t.Seat == ticket.Seat)
                    {
                        inOrder = true;
                        break;
                    }
                }

                /*List<String> contained = new List<String>();
                foreach (Ticket ticket1 in dbTicket.MovieOrder.Tickets)
                {
                    contained.Add(ticket1.Seat);
                }
                if (contained.Contains(ticket.Seat))
                {
                    ViewBag.AllShowings = GetAllShowings(ticket.MovieShowing.Movie.MovieId);
                    ViewBag.ErrorMessage = "Duplicate Seat.";
                    return View(dbTicket);
                }*/

                if (inOrder == false && dbShowing.SeatsAvailable.Contains(ticket.Seat) != false)
                //Update scalar properties
                //if (ticket.ValidSeat() == true)
                {
                    //dbTicket.MovieShowing.SeatsAvailable.Add(dbTicket.Seat);
                    dbTicket.Seat = ticket.Seat;
                    //dbTicket.MovieShowing.SelectSeat(dbTicket.Seat);

                    _context.Update(dbTicket);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ViewBag.AllSeats = GetAllSeats(dbTicket.MovieShowing.MovieShowingID);
                    ViewBag.ErrorMessage = "Duplicate Seat.";
                    return View(dbTicket);
                }
                
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was a problem editing this ticket", ex.Message });
            }

            return RedirectToAction("ConfirmOrder", "MovieOrders", new { id = dbTicket.MovieOrder.MovieOrderID });
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a ticket to cancel!" });
            }

            Ticket ticket = await _context.Tickets
                                            .Include(t => t.MovieOrder)
                                            .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
            {
                return View("Error", new String[] { "This ticket was not in the database!" });
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        //TODO: Remember to add the seat back to the list
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            Ticket ticket = await _context.Tickets
                                            .Include(t => t.MovieOrder)
                                            .FirstOrDefaultAsync(t => t.TicketId == id);

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("ConfirmOrder", "MovieOrders", new { id = ticket.MovieOrder.MovieOrderID });
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }

        private SelectList GetAllShowings()
        {
            //create a list for all the courses
            List<MovieShowing> allShowings = _context.MovieShowings
                .Include(m => m.Movie)
                .Where(t => t.IsPublished == true)
                .ToList();
            List<MovieShowing> proper = new List<MovieShowing>();
            foreach (MovieShowing ms in allShowings)
            {
                if (ms.StartTime > DateTime.Now.AddHours(1))
                {
                    proper.Add(ms);
                }
            }

            //the user MUST select a course, so you don't need a dummy option for no course

            //use the constructor on select list to create a new select list with the options
            SelectList slAllShowings = new SelectList(proper, nameof(MovieShowing.MovieShowingID), nameof(MovieShowing.FullShowingName));

            return slAllShowings;
        }

        private SelectList GetAllShowings(int movieId)
        {
            //create a list for all the courses
            List<MovieShowing> allShowings = _context.MovieShowings
                                                        .Include(m => m.Movie)
                                                        .Where(m => m.Movie.MovieId == movieId)
                                                        .Where(t => t.IsPublished == true)
                                                        .ToList();
            List<MovieShowing> proper = new List<MovieShowing>();
            foreach (MovieShowing ms in allShowings)
            {
                if (ms.StartTime > DateTime.Now.AddHours(1))
                {
                    proper.Add(ms);
                }
            }

            //the user MUST select a course, so you don't need a dummy option for no course

            //use the constructor on select list to create a new select list with the options
            SelectList slAllShowings = new SelectList(proper.OrderBy(t => t.StartTime).ToList(), nameof(MovieShowing.MovieShowingID), nameof(MovieShowing.FullShowingName));

            return slAllShowings;
        }

        private SelectList GetAllSeats(int SelectedShowing)
        {

            MovieShowing ms = _context.MovieShowings
                .Where(t => t.MovieShowingID == SelectedShowing)
                .Include(t => t.Tickets)
                .ThenInclude(t => t.MovieOrder)
                .FirstOrDefault();
            List<String> allSeats = ms.SeatsAvailable;

                //Find(SelectedShowing).SeatsAvailable;
            SelectList slAllSeats = new SelectList(allSeats, allSeats);

            return slAllSeats;
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

    }
}
