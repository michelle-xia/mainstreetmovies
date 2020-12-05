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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class GiftsController : Controller
    {
        private readonly AppDbContext _context;

        public GiftsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DisplayGifts()
        {
            List<MovieOrder> giftOrders;
            giftOrders = _context.MovieOrders
                                .Include(r => r.Tickets)
                                .Include(o => o.Recipient)
                                .ThenInclude(r => r.OrdersReceived)
                                .Where(u => u.Recipient.UserName == User.Identity.Name)
                                .Include(o => o.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ToList();
            giftOrders.RemoveAll(t => t.isConfirmed == false);
            foreach (MovieOrder movieOrder in giftOrders)
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
            return View("DisplayGifts", giftOrders);
        }

        // GET: MovieOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a movie order to view!" });
            }

            MovieOrder movieOrder = await _context.MovieOrders
                                .Include(r => r.Tickets)
                                .ThenInclude(o => o.MovieShowing)
                                .ThenInclude(o => o.Movie)
                                .Include(o => o.Customer)
                                .Include(o => o.Recipient)
                                .ThenInclude(r => r.OrdersReceived)
                                .Where(u => u.Recipient.UserName == User.Identity.Name)
                                .Include(o => o.Tickets)
                                .FirstOrDefaultAsync(m => m.MovieOrderID == id);

            if (movieOrder == null)
            {
                return View("Error", new String[] { "This order was not found!" });
            }

            return View(movieOrder);
        }
    }
}
