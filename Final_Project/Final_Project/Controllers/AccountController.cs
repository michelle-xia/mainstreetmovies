using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Final_Project.DAL;
using System.Collections.Generic;
using System.Security.Claims;

using Final_Project.Models;

namespace Final_Project.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private PasswordValidator<AppUser> _passwordValidator;
        private AppDbContext _context;

        public AccountController(AppDbContext appDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _context = appDbContext;
            _userManager = userManager;
            _signInManager = signIn;
            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel rvm)
        {
            //if registration data is valid, create a new user on the database
            Console.WriteLine("RVM Age " + rvm.Age);

            if (rvm.Age < 18 && rvm.MakeEmployee == true)
            {
                ViewBag.BirthdayErrorMessage = "Employee must be at least 18 years old to create an account";
                return View(rvm);
            }

            if (ModelState.IsValid && rvm.Age >= 13)
            {

                //this code maps the RegisterViewModel to the AppUser domain model
                AppUser newUser = new AppUser
                {
                    UserName = rvm.Email,
                    Email = rvm.Email,
                    PhoneNumber = rvm.PhoneNumber,

                    //TODO: Add the rest of the custom user fields here
                    //FirstName is included as an example
                    FirstName = rvm.FirstName,
                    MiddleInitial = rvm.MiddleInitial,
                    LastName = rvm.LastName,
                    Address = rvm.Address,
                    City = rvm.City,
                    State = rvm.State,
                    Zip = rvm.Zip,
                    Birthday = rvm.Birthday,
                };

                //This code uses the UserManager object to create a new user with the specified password
                IdentityResult result = await _userManager.CreateAsync(newUser, rvm.Password);

                //If the add user operation was successful, we need to do a few more things
                if (result.Succeeded)
                {
                    if (rvm.MakeEmployee == true)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Employee");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, "Customer");
                    }
                    //TODO: Add user to desired role. This example adds the user to the customer role

                    //NOTE: This code logs the user into the account that they just created
                    //You may or may not want to log a user in directly after they register - check
                    //the business rules!
                    if (User.Identity.IsAuthenticated == false)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(rvm.Email, rvm.Password, false, lockoutOnFailure: false);
                    }

                    // sets up and creates the confirmation email
                    String EmailBody = $"Your account information is the following:\nUsername/Email: {newUser.Email}\nName: {newUser.FirstName} {newUser.MiddleInitial} {newUser.LastName}";
                    Utilities.Email.SendEmail(newUser.Email, "Your account has been created", EmailBody);

                    //Send the user to the home page
                    return RedirectToAction("Index", "Home");
                }
                else  //the add user operation didn't work, and we need to show an error message
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            if (rvm.Age < 13)
            {
                ViewBag.BirthdayErrorMessage = "You must be at least 13 years old to create an account";
            }
            //this is the sad path - something went wrong, 
            //return the user to the register page to try again
            return View(rvm);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            _signInManager.SignOutAsync(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl; //pass along the page the user should go back to
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel lvm, string returnUrl)
        {
            //if user forgot to include user name or password,
            //send them back to the login page to try again
            if (ModelState.IsValid == false)
            {
                return View(lvm);
            }

            //attempt to sign the user in using the SignInManager
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, lvm.RememberMe, lockoutOnFailure: false);

            //if the login worked, take the user to either the url
            //they requested OR the homepage if there isn't a specific url
            AppUser user = _context.Users.Where(t => t.Email == lvm.Email).FirstOrDefault();
            if (result.Succeeded)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    return Redirect(returnUrl ?? "/");

                }
                if (await _userManager.IsInRoleAsync(user, "Customer"))
                {
                    return Redirect(returnUrl ?? "/");

                }
                if (await _userManager.IsInRoleAsync(user, "Employee"))
                {
                    return Redirect(returnUrl ?? "/");

                }
                else
                {
                    await _signInManager.SignOutAsync();

                    ModelState.AddModelError("", "You no longer have an active account.");
                    return View(lvm);
                }

            //comment out this bottom line when testing above logic
            //return Redirect(returnUrl ?? "/");

            //return ?? "/" means if returnUrl is null, substitute "/" (home)
            }
            else //log in was not successful
            {
                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, "Manager"))
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Customer"))
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Employee"))
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "You no longer have an active account.");
                    }
                }
                
                //send user back to login page to try again
                return View(lvm);
            }

        }

        //GET: Account/Index
        public IActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            String id = User.Identity.Name;
            AppUser user = _context.Users //.FirstOrDefault(u => u.UserName == id);
                                    .Where(u => u.UserName == id)
                                    .Include(mo => mo.MovieOrders )
                                    .ThenInclude(mo => mo.Tickets)
                                    .ThenInclude(mo => mo.MovieShowing)
                                    .ThenInclude(mo => mo.Movie)
                                    .Include(mo => mo.OrdersReceived)
                                    .FirstOrDefault();
            //populate the view model
            //(i.e. map the domain model to the view model)
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.UserName = user.UserName;
            ivm.TotalPopcornPoints = user.TotalPopcornPoints;
            ivm.FullName = user.FullName;
            ivm.FullAddress = user.FullAddress;
            ivm.PhoneNumber = user.PhoneNumber;
            ivm.Birthday = user.Birthday;

            //send data to the view
            return View(ivm);
        }



        //Logic for change password
        // GET: /Account/ChangePassword
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword( ChangePasswordViewModel cpm)
        {
            //if user forgot a field, send them back to 
            //change password page to try again
            if (ModelState.IsValid == false)
            {
                return View(cpm);
            }

            //Find the logged in user using the UserManager
            AppUser user = _context.Users //.FirstOrDefault(u => u.UserName == id);
                                    .Where(u => u.Email == User.Identity.Name)
                                    .Include(mo => mo.MovieOrders)
                                    .ThenInclude(mo => mo.Tickets)
                                    .ThenInclude(mo => mo.MovieShowing)
                                    .ThenInclude(mo => mo.Movie)
                                    .Include(mo => mo.OrdersReceived)
                                    .FirstOrDefault();
            var token = _userManager.
             GeneratePasswordResetTokenAsync(user).Result;
            //Attempt to change the password using the UserManager
            var result = await _userManager.ResetPasswordAsync(user, token, cpm.NewPassword);

            if (_userManager.IsInRoleAsync(user, "Customer").Result == true) //user is in the role
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("SeeAll");
            }

            //if the attempt to change the password worked

        }

        [Authorize(Roles = "Employee,Manager")]
        public IActionResult SeeAll()
        {
            List<AppUser> appUsers = new List<AppUser>();
            if (User.IsInRole("Manager"))
            {
                appUsers = _context.Users.Include(o => o.MovieOrders).ToList();
                return View(appUsers);

            }
            else
            {
                /*foreach (AppUser user in _userManager.Users)
                {
                    if (AllCustomers().Contains(user) == true)
                    {
                        appUsers.Add(user);
                    }
                }*/
                return View(AllCustomers());
                    
                    
                /*appUsers = _context.Users
                    .Include(ord => ord.MovieOrders)
                    .Where(User in AllCustomers())
                    .ToList();*/
            } //TODO: MAKE SURE employees ONLY SEE WHAT THEY ARE AUTHORIZED TO SEE
        }

        [Authorize(Roles = "Employee, Manager")]
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

        [Authorize(Roles = "Employee,Manager")]

        public ActionResult ChangeOtherAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee,Manager")]
        public async Task<ActionResult> ChangeOtherAccount(String id, ChangeAccountViewModel cavm)
        {
            //if user forgot a field, send them back to 
            //change password page to try again

            //AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);

            AppUser user = _context.Users //.FirstOrDefault(u => u.UserName == id);
                                   .Where(u => u.Email == id)
                                   .Include(mo => mo.MovieOrders)
                                   .ThenInclude(mo => mo.Tickets)
                                   .ThenInclude(mo => mo.MovieShowing)
                                   .ThenInclude(mo => mo.Movie)
                                   .Include(mo => mo.OrdersReceived)
                                   .FirstOrDefault();

            String userType = "";
            if (_userManager.IsInRoleAsync(user, "Employee").Result)
            {
                userType = "Employee";
            }
            else if (_userManager.IsInRoleAsync(user, "Manager").Result)
            {
                userType = "Manager";
            }
            else
            {
                userType = "Customer";
            }

            if (ModelState.IsValid == false && cavm.NewBirthday != default)
            {
                return View(cavm);
            }

            //Find the logged in user using the UserManager
            if (cavm.NewBirthday != default)
            {
                if (userType == "Customer" && cavm.Age < 13)
                {
                    ViewBag.BirthdayErrorMessage = "Customers must be at least 13 years old to own an account";
                    return View(cavm);
                }

                if (userType == "Manager" && cavm.Age < 18)
                {
                    ViewBag.BirthdayErrorMessage = "Employees must be at least 18 years old to own an account";
                    return View(cavm);
                }

                if (userType == "Employee" &&  cavm.Age < 18)
                {
                    ViewBag.BirthdayErrorMessage = "Employees must be at least 18 years old to own an account";
                    return View(cavm);
                }
            }

            //Attempt to change the password using the UserManager
            try
            {
                //update the scalar properties
                if (cavm.NewFirst != default)
                {
                    user.FirstName = cavm.NewFirst;
                }
                if (cavm.NewMiddle != default)
                {
                    user.MiddleInitial = cavm.NewMiddle;
                }
                if (cavm.NewLast != default)
                {
                    user.LastName = cavm.NewLast;
                }
                if (cavm.NewBirthday != default)
                {
                    user.Birthday = cavm.NewBirthday;
                }
                if (cavm.NewPhoneNumber != null)
                {
                    user.PhoneNumber = cavm.NewPhoneNumber;
                }
                if (cavm.NewAddress != null)
                {
                    user.Address = cavm.NewAddress;
                }
                if (cavm.NewCity != null)
                {
                    user.City = cavm.NewCity;
                }
                if (cavm.NewState != null)
                {
                    user.State = cavm.NewState;
                }
                if (cavm.NewZip != null)
                {
                    user.Zip = cavm.NewZip;
                }

                //save changes
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was a problem editing this record", ex.Message });
            }


            return RedirectToAction("SeeAll");


        }
        [Authorize(Roles = "Employee,Manager")]
        public IActionResult Details(String id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify an customer to view!" });
            }
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            //String id = User.Identity.Name;
            AppUser user = _context.Users //.FirstOrDefault(u => u.UserName == id);
                                    .Where(u => u.Email == id)
                                    .Include(mo => mo.MovieOrders)
                                    .ThenInclude(mo => mo.Tickets)
                                    .ThenInclude(mo => mo.MovieShowing)
                                    .ThenInclude(mo => mo.Movie)
                                    .Include(mo => mo.OrdersReceived)
                                    .FirstOrDefault();
            //populate the view model
            //(i.e. map the domain model to the view model)
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.UserName = user.UserName;
            ivm.TotalPopcornPoints = user.TotalPopcornPoints;
            ivm.FullName = user.FullName;
            ivm.FullAddress = user.FullAddress;
            ivm.PhoneNumber = user.PhoneNumber;
            ivm.Birthday = user.Birthday;

            //send data to the view
            return View(ivm);
        }
      
        [Authorize(Roles = "Employee,Manager")]

        public ActionResult ChangeOtherPassword()
        {
           
            return View();
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee,Manager")]
        public async Task<ActionResult> ChangeOtherPassword(String id, ResetPasswordViewModel cpvm)
        {
            //if user forgot a field, send them back to 
            //change password page to try again
            if (ModelState.IsValid == false)
            {
                return View(cpvm);
            }

            //Find the logged in user using the UserManager
            AppUser user = _context.Users //.FirstOrDefault(u => u.UserName == id);
                                    .Where(u => u.Email == id)
                                    .Include(mo => mo.MovieOrders)
                                    .ThenInclude(mo => mo.Tickets)
                                    .ThenInclude(mo => mo.MovieShowing)
                                    .ThenInclude(mo => mo.Movie)
                                    .Include(mo => mo.OrdersReceived)
                                    .FirstOrDefault();
            var token = _userManager.
             GeneratePasswordResetTokenAsync(user).Result;
            //Attempt to change the password using the UserManager
            var result = await _userManager.ResetPasswordAsync(user, token, cpvm.NewPassword);

            return RedirectToAction("SeeAll");

            //if the attempt to change the password worked

        }




        public ActionResult ChangeAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeAccount(ChangeAccountViewModel cavm)
        {
            //if user forgot a field, send them back to 
            //change password page to try again

            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid == false && cavm.NewBirthday != default)
            {
                return View(cavm);
            }

            //Find the logged in user using the UserManager

            String userType = "";

            if (_userManager.IsInRoleAsync(userLoggedIn, "Employee").Result)
            {
                userType = "Employee";
            }
            else if (_userManager.IsInRoleAsync(userLoggedIn, "Manager").Result)
            {
                userType = "Manager";
            }
            else
            {
                userType = "Customer";
            }

            if (cavm.NewBirthday != default)
            {
                if (userType == "Customer" && cavm.Age < 13)
                {
                    ViewBag.BirthdayErrorMessage = "You must be at least 13 years old to own an account";
                    return View(cavm);
                }

                if (userType == "Manager" && cavm.Age < 18)
                {
                    ViewBag.BirthdayErrorMessage = "You must be at least 18 years old to own a manager account";
                    return View(cavm);
                }

                if (userType == "Employee" && cavm.Age < 18)
                {
                    ViewBag.BirthdayErrorMessage = "You must be at least 18 years old to own an employee account";
                    return View(cavm);
                }
            }
            //Attempt to change the password using the UserManager
            try
            {
                //update the scalar properties
                if (cavm.NewBirthday != default)
                {
                    userLoggedIn.Birthday = cavm.NewBirthday;
                }
                if (cavm.NewPhoneNumber != null)
                {
                    userLoggedIn.PhoneNumber = cavm.NewPhoneNumber;
                }
                if (cavm.NewAddress != null)
                {
                    userLoggedIn.Address = cavm.NewAddress;
                }
                if (cavm.NewCity != null)
                {
                    userLoggedIn.City = cavm.NewCity;
                }
                if (cavm.NewState != null)
                {
                    userLoggedIn.State = cavm.NewState;
                }
                if (cavm.NewZip != null)
                {
                    userLoggedIn.Zip = cavm.NewZip;
                }

                //save changes
                _context.Update(userLoggedIn);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was a problem editing this record", ex.Message });
            }

           
                return RedirectToAction("Index");
                     

        }

        //GET:/Account/AccessDenied
        public IActionResult AccessDenied(String ReturnURL)
        {
            return View("Error", new string[] { "Access is denied" });
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            //sign the user out of the application
            _signInManager.SignOutAsync();

            //send the user back to the home page
            return RedirectToAction("Index", "Home");
        }           
    }
}