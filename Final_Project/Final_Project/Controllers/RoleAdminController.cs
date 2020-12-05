using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

//TODO: Change these using statements to match your project
using Final_Project.DAL;
using Final_Project.Models;

//TODO: Change this namespace to match your project
namespace Final_Project.Controllers
{
    //TODO: Uncomment this line once you have roles working correctly
    [Authorize(Roles = "Manager")]
    public class RoleAdminController : Controller
    {
        //create private variables for the services needed in this controller
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;


        //RoleAdminController constructor
        public RoleAdminController(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //populate the values of the variables passed into the controller
            _context = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /RoleAdmin/
        public async Task<ActionResult> Index()
        {
            //Create a list of roles that will need to be edited
            List<RoleEditModel> roles = new List<RoleEditModel>();
            
            //loop through each of the existing roles
            foreach (IdentityRole role in _roleManager.Roles)
            {
                //this is a list of all the users who ARE in this role (members)
                List<AppUser> members = new List<AppUser>();

                //this is a list of all the users who ARE NOT in this role (non-members)
                List<AppUser> nonMembers = new List<AppUser>();

                //loop through ALL the users and decide if they are in the role(member) or not (non-member)
                //every user will be evaluated for every role, so this is a SLOW chunk of code because
                //it accesses the database so many times
                foreach (AppUser user in _userManager.Users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name) == true) //user is in the role
                    {
                        //add user to list of members
                        members.Add(user);
                    }
                    else //user is NOT in the role
                    {
                        //add user to list of non-members
                        nonMembers.Add(user);
                    }
                }

                //create a new instance of the role edit model
                RoleEditModel rem = new RoleEditModel();

                //populate the properties of the role edit model
                rem.Role = role; //role from database
                rem.Members = members; //list of users in this role
                rem.NonMembers = nonMembers; //list of users NOT in this role
                
                //add this role to the list of role edit models
                roles.Add(rem);  
            }

            //pass the list of roles to the view
            return View(roles);
        }

        /*public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                //attempt to create the new role using the role manager
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

                //if the role was created successfully, take the user to the index page
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else //role was not added succesfully, so add errors to model 
                //and let the user try again
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            //if code gets this far, we need to show an error
            return View(name);
        }*/

        public async Task<ActionResult> Edit(string id)
        {
            //look up the role requested by the user
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            //create a list for the members of the role
            List<AppUser> members = new List<AppUser>();

            //create a list for the non-members of the role
            List<AppUser> nonMembers = new List<AppUser>();

            //through ALL the users and decide if they are in the role(member) or not (non-member)
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name) == true) //user is in the role
                {
                    //add the user to the list of members
                    members.Add(user);
                }
                else //user is NOT in the role
                {
                    nonMembers.Add(user);
                }
            }

            //create a new instance of the role edit model
            RoleEditModel rem = new RoleEditModel();

            //populate the properties of the role edit model
            rem.Role = role; //role looked up from database
            rem.Members = members; //list of users in the role
            rem.NonMembers = nonMembers; //list of users NOT in the role

            //send user to view with populated role edit model
            return View(rem);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel rmm)
        {
            //create a result to refer to later
            IdentityResult result;

            //if RoleModificationModel is valid, add new users
            if (ModelState.IsValid)
            {
                //if there are users to add, then add them
                if (rmm.IdsToAdd != null)
                {
                    foreach (string userId in rmm.IdsToAdd)
                    {
                        //find the user in the database using their id
                        AppUser user = await _userManager.FindByIdAsync(userId);

                        //attempt to add the user to the role using the UserManager
                        result = await _userManager.AddToRoleAsync(user, rmm.RoleName);
                        
                        //if attempt to add user to role didn't work, show user the error page
                        if (result.Succeeded == false)
                        {
                            //send user to error page
                            return View("Error", result.Errors);
                        }
                        _context.Update(user);
                    }
                }

                //if there are users to remove from the role, remove them
                if (rmm.IdsToDelete != null)
                {
                    //loop through all the ids to remove from role
                    foreach (string userId in rmm.IdsToDelete)
                    {
                        //find the user in the database using their id
                        AppUser user = await _userManager.FindByIdAsync(userId);

                        //attempt to remove the user from the role using the UserManager
                        result = await _userManager.RemoveFromRoleAsync(user, rmm.RoleName);

                        //if attempt to remove the user from role didn't work, show the error page
                        if (result.Succeeded == false)
                        {
                            //show user the error page
                            return View("Error", result.Errors);
                        }
                        _context.Update(user);
                    }
                }

                _context.SaveChanges();
                //this is the happy path - all edits worked
                //take the user back to the RoleAdmin Index page
                return RedirectToAction("Index");
            }

            //this is a sad path - the role was not found
            //show the user the error page
            return View("Error", new string[] { "Role Not Found" });
        }

        public async Task<ActionResult> Fire(string userId)
        {
            if (userId ==null || userId == "")
            {
                return View("Error", new string[] { "User Id Not Found" });
            }

            IdentityResult result;
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error", new string[] { "User Not Found" });
            }

            if (_userManager.IsInRoleAsync(user, "Employee").Result == false)
            {
                return View("Error", new string[] { "This user is not an Employee and ca" +
                    "not be fired" });
            }

            result = await _userManager.RemoveFromRoleAsync(user, "Employee");
            if (result.Succeeded == false)
            {
                //show user the error page
                return View("Error", result.Errors);
            }
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("SeeAll", "Account");
        }

        public async Task<ActionResult> Rehire(string userId)
        {
            if (userId == null || userId == "")
            {
                return View("Error", new string[] { "User Id Not Found" });
            }

            IdentityResult result;
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error", new string[] { "User Not Found" });
            }

            if (_userManager.IsInRoleAsync(user, "Employee").Result == true)
            {
                return View("Error", new string[] { "This user is already an Employee and ca" +
                    "not be rehired." });
            }
            if (_userManager.IsInRoleAsync(user, "Customer").Result == true)
            {
                return View("Error", new string[] { "This user is a Customer and ca" +
                    "not be rehired." });
            }
            if (_userManager.IsInRoleAsync(user, "Manager").Result == true)
            {
                return View("Error", new string[] { "This user is already a Manager and ca" +
                    "not be rehired." });
            }
            result = await _userManager.AddToRoleAsync(user, "Employee");
            if (result.Succeeded == false)
            {
                //show user the error page
                return View("Error", result.Errors);
            }
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("SeeAll", "Account");
        }

        public async Task<ActionResult> PromoteToManager(string userId)
        {
            if (userId == null || userId == "")
            {
                return View("Error", new string[] { "User Id Not Found" });
            }

            IdentityResult result;
            IdentityResult result2;
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error", new string[] { "User Not Found" });
            }
            if (_userManager.IsInRoleAsync(user, "Manager").Result == true)
            {
                return View("Error", new string[] { "This user is already a Manage."});
            }
            if (_userManager.IsInRoleAsync(user, "Employee").Result == false)
            {
                return View("Error", new string[] { "This user is not an Employee and ca" +
                    "not be promoted to manager." });
            }
            result = await _userManager.AddToRoleAsync(user, "Manager");
            if (result.Succeeded == false)
            {
                //show user the error page
                return View("Error", result.Errors);
            }
            result2 = await _userManager.RemoveFromRoleAsync(user, "Employee");
            if (result2.Succeeded == false)
            {
                //show user the error page
                return View("Error", result.Errors);
            }
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("SeeAll", "Account");
        }

        /*public async Task<ActionResult> SeeAllEmployees()
        {
            //this is a list of all the users who ARE in this role (members)
            List<AppUser> members = new List<AppUser>();

            //this is a list of all the users who ARE NOT in this role (non-members)
            List<AppUser> nonMembers = new List<AppUser>();

            //loop through ALL the users and decide if they are in the role(member) or not (non-member)
            //every user will be evaluated for every role, so this is a SLOW chunk of code because
            //it accesses the database so many times
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Employee") == true) //user is in the role
                {
                    //add user to list of members
                    members.Add(user);
                }
            }
            return View(members);
        }
        public async Task<ActionResult> SeeAllManagers()
        {
            //this is a list of all the users who ARE in this role (members)
            List<AppUser> members = new List<AppUser>();

            //this is a list of all the users who ARE NOT in this role (non-members)
            List<AppUser> nonMembers = new List<AppUser>();

            //loop through ALL the users and decide if they are in the role(member) or not (non-member)
            //every user will be evaluated for every role, so this is a SLOW chunk of code because
            //it accesses the database so many times
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager") == true) //user is in the role
                {
                    //add user to list of members
                    members.Add(user);
                }
            }
            return View(members);
        }
        public async Task<ActionResult> SeeAllCustomers()
        {
            //this is a list of all the users who ARE in this role (members)
            List<AppUser> members = new List<AppUser>();

            //this is a list of all the users who ARE NOT in this role (non-members)
            List<AppUser> nonMembers = new List<AppUser>();

            //loop through ALL the users and decide if they are in the role(member) or not (non-member)
            //every user will be evaluated for every role, so this is a SLOW chunk of code because
            //it accesses the database so many times
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Customer") == true) //user is in the role
                {
                    //add user to list of members
                    members.Add(user);
                }
            }
            return View(members);
        }
        public async Task<ActionResult> SeeAllFiredEmployees()
        {
            //this is a list of all the users who ARE in this role (members)
            List<AppUser> members = new List<AppUser>();

            //this is a list of all the users who ARE NOT in this role (non-members)
            List<AppUser> nonMembers = new List<AppUser>();

            //loop through ALL the users and decide if they are in the role(member) or not (non-member)
            //every user will be evaluated for every role, so this is a SLOW chunk of code because
            //it accesses the database so many times
            foreach (AppUser user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, "Customer") == false) //user is in the role
                {
                    if (await _userManager.IsInRoleAsync(user, "Employee") == false)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Manager") == false)
                        {
                            nonMembers.Add(user);
                        }
                    }                       
                }
            }
            return View(nonMembers);
        }*/
    }
}