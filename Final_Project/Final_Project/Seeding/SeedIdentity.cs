using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

using Final_Project.DAL;
using Final_Project.Models;


namespace Final_Project.Seeding
{
    //add identity data
    public static class SeedIdentity
    {
        public static async Task AddAdmin(IServiceProvider serviceProvider)
        {
            //Get instances of the services needed to add a user & add a user to a role
            AppDbContext _context = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //TODO: Add the needed roles
            //if the manager role doesn't exist, add it
            if (await _roleManager.RoleExistsAsync("Manager") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            //if the customer role doesn't exist, add it
            if (await _roleManager.RoleExistsAsync("Customer") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }
            //if employee role doesn't exist, add it
            if (await _roleManager.RoleExistsAsync("Employee") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
            }

            //check to see if the admin has already been added
            AppUser newUser = _context.Users.FirstOrDefault(u => u.Email == "manager@example.com");

            //if admin hasn't been created, then add them
            if (newUser == null)
            {
                //create a new instance of the app user class
                newUser = new AppUser();

                //populate the user properties that are from the 
                //IdentityUser base class
                newUser.UserName = "manager@example.com";
                newUser.Email = "manager@example.com";
                newUser.PhoneNumber = "(512)555-1234";

                //TODO: Add additional fields that you created on the AppUser class
                //FirstName is included as an example
                newUser.FirstName = "Manager";
                newUser.MiddleInitial = "I";
                newUser.LastName = "Example";
                newUser.Address = "123 Street";
                newUser.City = "Austin";
                newUser.State = "TX";
                newUser.Zip = "12345";
                newUser.DefaultPopcornPoints = 2;
                newUser.UniqueIdentifier = 1003;

                //create a variable for result
                IdentityResult result = new IdentityResult();
                try
                {
                    //NOTE: Attempt to add the user using the UserManager
                    //"Abc123!" is the password for this user
                    result = await _userManager.CreateAsync(newUser, "Abc123!");
                }
                catch (Exception ex)
                {
                    StringBuilder msg = new StringBuilder();
                    msg.Append("There was an error adding the user with the email ");
                    msg.Append(newUser.Email);
                    msg.Append(". This often happens because you are missing a required field on AppUser");
                    throw new Exception(msg.ToString(), ex);
                }
                //if the user was not added succesfully, throw an exception
                //so the user knows what happened
                if (result.Succeeded == false)
                {
                    //Create a new string builder object to hold the error message(s)
                    StringBuilder msg = new StringBuilder();

                    //loop through all of the errors and add them to the message
                    foreach (var error in result.Errors)
                    {
                        msg.AppendLine(error.ToString());
                    }

                    //throw a new exception to tell the user something is wrong
                    throw new Exception("This user can't be added:" + msg.ToString());
                }
                _context.SaveChanges();
                newUser = _context.Users.FirstOrDefault(u => u.UserName == "manager@example.com");
            }

            //Add the new user we just created to the Manager role
            if (await _userManager.IsInRoleAsync(newUser, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(newUser, "Manager");
            }

            //save changes
            _context.SaveChanges();
        }

    }
}

