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
	public static class SeedEmployees
	{
		public static async Task SeedAllEmployees(IServiceProvider serviceProvider)
		{
			List<AppUser> AllCustomers = new List<AppUser>();
			List<String> TmpPass = new List<String>();
			List<String> EmpType = new List<String>();

			//Get instances of the services needed to add a user & add a user to a role
			AppDbContext _context = serviceProvider.GetRequiredService<AppDbContext>();
			UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			AllCustomers.Add(new AppUser
			{
				LastName = "Jacobs",
				FirstName = "Todd",
				MiddleInitial = "L",
				Birthday = new DateTime(1958, 04, 25),
				Address = "4564 Elm St.",
				City = "Georgetown",
				State = "TX",
				Zip = "78628",
				PhoneNumber = "9074653365",
				Email = "t.jacobs@mainstreetmovies.com",
				UserName = "t.jacobs@mainstreetmovies.com",
			});

			TmpPass.Add("toddyboy");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Rice",
				FirstName = "Eryn",
				MiddleInitial = "M",
				Birthday = new DateTime(1959, 07, 02),
				Address = "3405 Rio Grande",
				City = "Austin",
				State = "TX",
				Zip = "78746",
				PhoneNumber = "9073876657",
				Email = "e.rice@mainstreetmovies.com",
				UserName = "e.rice@mainstreetmovies.com",
			});

			TmpPass.Add("ricearoni");

			EmpType.Add("Manager");

			AllCustomers.Add(new AppUser
			{
				LastName = "Taylor",
				FirstName = "Allison",
				MiddleInitial = "R",
				Birthday = new DateTime(1964, 09, 02),
				Address = "467 Nueces St.",
				City = "Austin",
				State = "TX",
				Zip = "78727",
				PhoneNumber = "9074748452",
				Email = "a.taylor@mainstreetmovies.com",
				UserName = "a.taylor@mainstreetmovies.com",
			});

			TmpPass.Add("nostalgic");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Martinez",
				FirstName = "Gregory",
				MiddleInitial = "R",
				Birthday = new DateTime(1992, 03, 30),
				Address = "8295 Sunset Blvd.",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				PhoneNumber = "9078746718",
				Email = "g.martinez@mainstreetmovies.com",
				UserName = "g.martinez@mainstreetmovies.com",
			});

			TmpPass.Add("fungus");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Sheffield",
				FirstName = "Martin",
				MiddleInitial = "J",
				Birthday = new DateTime(1996, 12, 29),
				Address = "3886 Avenue A",
				City = "San Marcos",
				State = "TX",
				Zip = "78666",
				PhoneNumber = "9075479167",
				Email = "m.sheffield@mainstreetmovies.com",
				UserName = "m.sheffield@mainstreetmovies.com",
			});

			TmpPass.Add("longhorns");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Tanner",
				FirstName = "Jeremy",
				MiddleInitial = "S",
				Birthday = new DateTime(1970, 08, 12),
				Address = "4347 Almstead",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				PhoneNumber = "9074590929",
				Email = "j.tanner@mainstreetmovies.com",
				UserName = "j.tanner@mainstreetmovies.com",
			});

			TmpPass.Add("tanman");

			EmpType.Add("Manager");

			AllCustomers.Add(new AppUser
			{
				LastName = "Rhodes",
				FirstName = "Megan",
				MiddleInitial = "C",
				Birthday = new DateTime(1970, 12, 18),
				Address = "4587 Enfield Rd.",
				City = "Austin",
				State = "TX",
				Zip = "78729",
				PhoneNumber = "9073744746",
				Email = "m.rhodes@mainstreetmovies.com",
				UserName = "m.rhodes@mainstreetmovies.com",
			});

			TmpPass.Add("countryrhodes");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Stuart",
				FirstName = "Eric",
				MiddleInitial = "F",
				Birthday = new DateTime(1971, 03, 11),
				Address = "5576 Toro Ring",
				City = "San Antonio",
				State = "TX",
				Zip = "78758",
				PhoneNumber = "9078178335",
				Email = "e.stuart@mainstreetmovies.com",
				UserName = "e.stuart@mainstreetmovies.com",
			});

			TmpPass.Add("stewboy");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Taylor",
				FirstName = "Rachel",
				MiddleInitial = "O",
				Birthday = new DateTime(1972, 12, 20),
				Address = "345 Longview Dr.",
				City = "Austin",
				State = "TX",
				Zip = "78746",
				PhoneNumber = "9074512631",
				Email = "r.taylor@mainstreetmovies.com",
				UserName = "r.taylor@mainstreetmovies.com",
			});

			TmpPass.Add("swansong");

			EmpType.Add("Manager");

			AllCustomers.Add(new AppUser
			{
				LastName = "Lawrence",
				FirstName = "Tori",
				MiddleInitial = "Y",
				Birthday = new DateTime(1973, 04, 28),
				Address = "6639 Butterfly Ln.",
				City = "Austin",
				State = "TX",
				Zip = "78712",
				PhoneNumber = "9079457399",
				Email = "v.lawrence@mainstreetmovies.com",
				UserName = "v.lawrence@mainstreetmovies.com",
			});

			TmpPass.Add("lottery");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Alberts",
				FirstName = "Allen",
				MiddleInitial = "H",
				Birthday = new DateTime(1978, 06, 21),
				Address = "4965 Oak Hill",
				City = "Austin",
				State = "TX",
				Zip = "78705",
				PhoneNumber = "9078752943",
				Email = "a.rogers@mainstreetmovies.com",
				UserName = "a.rogers@mainstreetmovies.com",
			});

			TmpPass.Add("evanescent");

			EmpType.Add("Manager");

			AllCustomers.Add(new AppUser
			{
				LastName = "Baker",
				FirstName = "Christopher",
				MiddleInitial = "E",
				Birthday = new DateTime(1993, 03, 16),
				Address = "1245 Lake Anchorage Blvd.",
				City = "Cedar Park",
				State = "TX",
				Zip = "78613",
				PhoneNumber = "9075571146",
				Email = "c.baker@mainstreetmovies.com",
				UserName = "c.baker@mainstreetmovies.com",
			});

			TmpPass.Add("hecktour");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Sewell",
				FirstName = "William",
				MiddleInitial = "G",
				Birthday = new DateTime(1986, 05, 25),
				Address = "2365 51st St.",
				City = "Austin",
				State = "TX",
				Zip = "78755",
				PhoneNumber = "9074510084",
				Email = "w.sewell@mainstreetmovies.com",
				UserName = "w.sewell@mainstreetmovies.com",
			});

			TmpPass.Add("walkamile");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Mason",
				FirstName = "Jack",
				MiddleInitial = "L",
				Birthday = new DateTime(1986, 06, 06),
				Address = "444 45th St",
				City = "Austin",
				State = "TX",
				Zip = "78701",
				PhoneNumber = "9018833432",
				Email = "j.mason@mainstreetmovies.com",
				UserName = "j.mason@mainstreetmovies.com",
			});

			TmpPass.Add("changalang");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Jackson",
				FirstName = "Jack",
				MiddleInitial = "J",
				Birthday = new DateTime(1989, 10, 16),
				Address = "222 Main",
				City = "Austin",
				State = "TX",
				Zip = "78760",
				PhoneNumber = "9075554545",
				Email = "j.jackson@mainstreetmovies.com",
				UserName = "j.jackson@mainstreetmovies.com",
			});

			TmpPass.Add("offbeat");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Nguyen",
				FirstName = "Andy",
				MiddleInitial = "M",
				Birthday = new DateTime(1988, 04, 05),
				Address = "465 N. Bear Cub",
				City = "Austin",
				State = "TX",
				Zip = "78734",
				PhoneNumber = "9075524141",
				Email = "m.nguyen@mainstreetmovies.com",
				UserName = "m.nguyen@mainstreetmovies.com",
			});

			TmpPass.Add("landus");

			EmpType.Add("Manager");

			AllCustomers.Add(new AppUser
			{
				LastName = "Barnes",
				FirstName = "Susan",
				MiddleInitial = "M",
				Birthday = new DateTime(1993, 02, 22),
				Address = "888 S. Main",
				City = "Kyle",
				State = "TX",
				Zip = "78640",
				PhoneNumber = "9556662323",
				Email = "s.barnes@mainstreetmovies.com",
				UserName = "s.barnes@mainstreetmovies.com",
			});

			TmpPass.Add("rhythm");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Johns",
				FirstName = "Sarah",
				MiddleInitial = "L",
				Birthday = new DateTime(1996, 06, 29),
				Address = "999 LeBlat",
				City = "Austin",
				State = "TX",
				Zip = "78747",
				PhoneNumber = "9886662222",
				Email = "l.jones@mainstreetmovies.com",
				UserName = "l.jones@mainstreetmovies.com",
			});

			TmpPass.Add("kindly");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Silva",
				FirstName = "Cindy",
				MiddleInitial = "S",
				Birthday = new DateTime(1997, 12, 29),
				Address = "900 4th St",
				City = "Austin",
				State = "TX",
				Zip = "78758",
				PhoneNumber = "9221113333",
				Email = "c.silva@mainstreetmovies.com",
				UserName = "c.silva@mainstreetmovies.com",
			});

			TmpPass.Add("arched");

			EmpType.Add("Employee");

			AllCustomers.Add(new AppUser
			{
				LastName = "Rankin",
				FirstName = "Suzie",
				MiddleInitial = "R",
				Birthday = new DateTime(1999, 12, 17),
				Address = "23 Polar Bear Road",
				City = "Buda",
				State = "TX",
				Zip = "78712",
				PhoneNumber = "9893336666",
				Email = "s.rankin@mainstreetmovies.com",
				UserName = "s.rankin@mainstreetmovies.com",
			});

			TmpPass.Add("decorate");

			EmpType.Add("Employee");

			int intCust = 0;

			try
			{
				foreach (AppUser seedCustomer in AllCustomers)
				{
					AppUser newUser = _context.Users.FirstOrDefault(u => u.Email == seedCustomer.Email);

					//if job posting is null, job posting is not in database
					if (newUser is null)
					{
						newUser = new AppUser();

						//populate the user properties that are from the 
						//IdentityUser base class
						newUser.UserName = seedCustomer.UserName;
						newUser.Email = seedCustomer.Email;
						newUser.PhoneNumber = seedCustomer.PhoneNumber;

						//TODO: Add additional fields that you created on the AppUser class
						//FirstName is included as an example
						newUser.FirstName = seedCustomer.FirstName;
						newUser.MiddleInitial = seedCustomer.MiddleInitial;
						newUser.LastName = seedCustomer.LastName;
						newUser.Address = seedCustomer.Address;
						newUser.Birthday = seedCustomer.Birthday;
						newUser.City = seedCustomer.City;
						newUser.State = seedCustomer.State;
						newUser.Zip = seedCustomer.Zip;

						IdentityResult result = new IdentityResult();
						try
						{
							//NOTE: Attempt to add the user using the UserManager
							//"Abc123!" is the password for this user
							result = await _userManager.CreateAsync(newUser, TmpPass[intCust]);
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
						newUser = _context.Users.FirstOrDefault(u => u.UserName == seedCustomer.Email);
					}


					if (await _userManager.IsInRoleAsync(newUser, EmpType[intCust]) == false)
					{
						await _userManager.AddToRoleAsync(newUser, EmpType[intCust]);
					}

					intCust++;

					//save changes
					_context.SaveChanges();
				}
			}
			catch (Exception ex) //throw error if there is a problem in the database
			{
				StringBuilder msg = new StringBuilder();
				msg.Append("There was a problem adding the job posting with the title: ");
				msg.Append(" (UniqueIdentifier: ");
				msg.Append(")");
				throw new Exception(msg.ToString(), ex);
			}

		}
	}
}