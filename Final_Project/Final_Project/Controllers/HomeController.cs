//Names: Corey Li, Arushi Mathavan, Derek Wu, Michelle Xia
//Due Date: 12/4/2020 at 12:00pm
//Assignment: Final Project

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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public HomeController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            List<MovieShowing> todays = new List<MovieShowing>();
            List<MovieShowing> allShowings = _context.MovieShowings
                .Include(o => o.Movie)
                .Where(o => o.IsPublished == true)
                .Include(o => o.Tickets)
                .ThenInclude(o => o.MovieOrder)
                .ToList();
            foreach (MovieShowing ms in allShowings)
            {
                if (ms.StartTime > DateTime.Now && ms.StartTime < DateTime.Now.Date.AddDays(1))
                {
                    todays.Add(ms);
                }
            }
            return View(todays);
        }

        public IActionResult Team19()
        {
            return View();
        }
    }
}
