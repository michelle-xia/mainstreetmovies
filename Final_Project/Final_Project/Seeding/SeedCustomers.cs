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
    public static class SeedCustomers
    {
		public static async Task SeedAllCustomers(IServiceProvider serviceProvider)
		{
			List<AppUser> AllCustomers = new List<AppUser>();
			List<String> TmpPass = new List<String>();

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5001,
				LastName = "Baker",
				FirstName = "Christopher",
				MiddleInitial = "L",
				Birthday = new DateTime(1949, 11, 23),
				Address = "1245 Lake Anchorage Blvd.",
				City = "Austin",
				State = "TX",
				Zip = "37705",
				PhoneNumber = "5125551132",
				Email = "cbaker@puppy.com",
				UserName = "cbaker@puppy.com",
				DefaultPopcornPoints = 90
			});

			TmpPass.Add("hello1");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5002,
				LastName = "Banks",
				FirstName = "Martin",
				MiddleInitial = "T",
				Birthday = new DateTime(1962, 11, 27),
				Address = "6700 Small Pine Lane",
				City = "Austin",
				State = "TX",
				Zip = "37712",
				PhoneNumber = "5125552243",
				Email = "banker@longhorn.net",
				UserName = "banker@longhorn.net",
				DefaultPopcornPoints = 80
			});

			TmpPass.Add("snowing");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5003,
				LastName = "Broccolo",
				FirstName = "Franco",
				MiddleInitial = "V",
				Birthday = new DateTime(1992, 10, 11),
				Address = "562 Sad Road",
				City = "Austin",
				State = "TX",
				Zip = "37704",
				PhoneNumber = "5125555546",
				Email = "franco@puppy.com",
				UserName = "franco@puppy.com",
				DefaultPopcornPoints = 10
			});

			TmpPass.Add("skating");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5004,
				LastName = "Chang",
				FirstName = "Wiseman",
				MiddleInitial = "L",
				Birthday = new DateTime(1997, 05, 16),
				Address = "7202 Big Hall",
				City = "Round Rock",
				State = "TX",
				Zip = "37681",
				PhoneNumber = "5125553376",
				Email = "wchang@puppy.com",
				UserName = "wchang@puppy.com",
				DefaultPopcornPoints = 20
			});

			TmpPass.Add("Fighter");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5005,
				LastName = "Chou",
				FirstName = "Lim",
				MiddleInitial = "C",
				Birthday = new DateTime(1970, 04, 06),
				Address = "8600 Cherry Lane",
				City = "Austin",
				State = "TX",
				Zip = "37705",
				PhoneNumber = "5125555379",
				Email = "limchou@gogle.com",
				UserName = "limchou@gogle.com",
				DefaultPopcornPoints = 70
			});

			TmpPass.Add("Dallas63");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5006,
				LastName = "Dixon",
				FirstName = "Shaman",
				MiddleInitial = "D",
				Birthday = new DateTime(1984, 01, 12),
				Address = "8234 Puppy Circle",
				City = "Austin",
				State = "TX",
				Zip = "37712",
				PhoneNumber = "5125556607",
				Email = "shdixon@aoll.com",
				UserName = "shdixon@aoll.com",
				DefaultPopcornPoints = 10
			});

			TmpPass.Add("peppero");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5007,
				LastName = "Evans",
				FirstName = "Jim Bob",
				MiddleInitial = "C",
				Birthday = new DateTime(1959, 09, 09),
				Address = "9506 Kitten Circle",
				City = "Georgetown",
				State = "TX",
				Zip = "37628",
				PhoneNumber = "5125552289",
				Email = "j.b.evans@aheca.org",
				UserName = "j.b.evans@aheca.org",
				DefaultPopcornPoints = 0
			});

			TmpPass.Add("longhorn");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5008,
				LastName = "Feeley",
				FirstName = "Lou Ann",
				MiddleInitial = "K",
				Birthday = new DateTime(2001, 01, 12),
				Address = "7600 N 7th Street W",
				City = "Austin",
				State = "TX",
				Zip = "37746",
				PhoneNumber = "5125559999",
				Email = "feeley@penguin.org",
				UserName = "feeley@penguin.org",
				DefaultPopcornPoints = 200
			});

			TmpPass.Add("aggiesuck");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5009,
				LastName = "Freeley",
				FirstName = "Teresa",
				MiddleInitial = "P",
				Birthday = new DateTime(1991, 02, 04),
				Address = "5448 Clearview Ave.",
				City = "Horseshoe Bay",
				State = "TX",
				Zip = "37657",
				PhoneNumber = "5125558827",
				Email = "tfreeley@minnetonka.ci.us",
				UserName = "tfreeley@minnetonka.ci.us",
				DefaultPopcornPoints = 250
			});

			TmpPass.Add("raiders75");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5010,
				LastName = "Garcia",
				FirstName = "Mikaela",
				MiddleInitial = "L",
				Birthday = new DateTime(1991, 10, 02),
				Address = "3594 Cowview",
				City = "Austin",
				State = "TX",
				Zip = "37727",
				PhoneNumber = "5125550002",
				Email = "mgarcia@gogle.com",
				UserName = "mgarcia@gogle.com",
				DefaultPopcornPoints = 40
			});

			TmpPass.Add("mustang54");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5011,
				LastName = "Haley",
				FirstName = "Charmander",
				MiddleInitial = "E",
				Birthday = new DateTime(1974, 07, 10),
				Address = "43 One Pigboy Pkwy",
				City = "Austin",
				State = "TX",
				Zip = "37712",
				PhoneNumber = "5125550198",
				Email = "chaley@mug.com",
				UserName = "chaley@mug.com",
				DefaultPopcornPoints = 30
			});

			TmpPass.Add("onetime76");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5012,
				LastName = "Hampton",
				FirstName = "Jeff",
				MiddleInitial = "T",
				Birthday = new DateTime(2004, 03, 10),
				Address = "7337 67th St.",
				City = "San Marcos",
				State = "TX",
				Zip = "37666",
				PhoneNumber = "5125552134",
				Email = "jeffh@mario.com",
				UserName = "jeffh@mario.com",
				DefaultPopcornPoints = 50
			});

			TmpPass.Add("hampton98");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5013,
				LastName = "Hearn",
				FirstName = "John",
				MiddleInitial = "B",
				Birthday = new DateTime(1950, 08, 05),
				Address = "8225 South First",
				City = "Plano",
				State = "TX",
				Zip = "37705",
				PhoneNumber = "5125559729",
				Email = "wjhearniii@umich.org",
				UserName = "wjhearniii@umich.org",
				DefaultPopcornPoints = 60
			});

			TmpPass.Add("jhearn99");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5014,
				LastName = "Hicks",
				FirstName = "Abadon",
				MiddleInitial = "J",
				Birthday = new DateTime(2004, 12, 08),
				Address = "632 NE Dog Ln., Ste 910",
				City = "Austin",
				State = "TX",
				Zip = "37712",
				PhoneNumber = "5125553967",
				Email = "ahick@yaho.com",
				UserName = "ahick@yaho.com",
				DefaultPopcornPoints = 60
			});

			TmpPass.Add("hickemon");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5015,
				LastName = "Ingram",
				FirstName = "Brock",
				MiddleInitial = "S",
				Birthday = new DateTime(2001, 09, 05),
				Address = "9548 El Perro Ct.",
				City = "New York",
				State = "NY",
				Zip = "10101",
				PhoneNumber = "5125552142",
				Email = "ingram@jack.com",
				UserName = "ingram@jack.com",
				DefaultPopcornPoints = 90
			});

			TmpPass.Add("ingram2098");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5016,
				LastName = "Jack",
				FirstName = "Todd",
				MiddleInitial = "L",
				Birthday = new DateTime(1999, 01, 20),
				Address = "2564 Tree St.",
				City = "Austin",
				State = "TX",
				Zip = "37729",
				PhoneNumber = "5125555557",
				Email = "toddj@yourmom.com",
				UserName = "toddj@yourmom.com",
				DefaultPopcornPoints = 140
			});

			TmpPass.Add("toddy53");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5017,
				LastName = "Lancer",
				FirstName = "Vic",
				MiddleInitial = "M",
				Birthday = new DateTime(2000, 04, 14),
				Address = "1639 Butter Ln.",
				City = "Beverly Hills",
				State = "CA",
				Zip = "90210",
				PhoneNumber = "5125550156",
				Email = "thequeen@aska.net",
				UserName = "thequeen@aska.net",
				DefaultPopcornPoints = 110
			});

			TmpPass.Add("nothing34");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5018,
				LastName = "Lineback",
				FirstName = "Sweeney",
				MiddleInitial = "W",
				Birthday = new DateTime(2003, 12, 02),
				Address = "1700 Land St",
				City = "Austin",
				State = "TX",
				Zip = "37758",
				PhoneNumber = "5125550168",
				Email = "linebacker@gogle.com",
				UserName = "linebacker@gogle.com",
				DefaultPopcornPoints = 50
			});

			TmpPass.Add("Password5");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5019,
				LastName = "Lowe",
				FirstName = "Ernesto",
				MiddleInitial = "S",
				Birthday = new DateTime(1977, 12, 07),
				Address = "2301 Snail Drive",
				City = "New Braunfels",
				State = "TX",
				Zip = "37130",
				PhoneNumber = "5125556959",
				Email = "elowe@scare.net",
				UserName = "elowe@scare.net",
				DefaultPopcornPoints = 40
			});

			TmpPass.Add("aclfest2076");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5020,
				LastName = "Luce",
				FirstName = "Charles",
				MiddleInitial = "B",
				Birthday = new DateTime(1949, 03, 16),
				Address = "7945 Small Clouds",
				City = "Cactus",
				State = "TX",
				Zip = "79013",
				PhoneNumber = "5125556919",
				Email = "cluce@gogle.com",
				UserName = "cluce@gogle.com",
				DefaultPopcornPoints = 160
			});

			TmpPass.Add("nothinggreat");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5021,
				LastName = "MacLeod",
				FirstName = "Jackson",
				MiddleInitial = "D",
				Birthday = new DateTime(1947, 02, 21),
				Address = "2804 Near West Blvd.",
				City = "Plano",
				State = "TX",
				Zip = "37654",
				PhoneNumber = "5125553223",
				Email = "mackcloud@george.com",
				UserName = "mackcloud@george.com",
				DefaultPopcornPoints = 130
			});

			TmpPass.Add("However");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5022,
				LastName = "Markham",
				FirstName = "Candice",
				MiddleInitial = "P",
				Birthday = new DateTime(1972, 03, 20),
				Address = "9761 Bike Chase",
				City = "Kissimmee",
				State = "FL",
				Zip = "34741",
				PhoneNumber = "5125554445",
				Email = "cmartin@beets.com",
				UserName = "cmartin@beets.com",
				DefaultPopcornPoints = 200
			});

			TmpPass.Add("nobodycares");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5023,
				LastName = "Martin",
				FirstName = "Clarence",
				MiddleInitial = "A",
				Birthday = new DateTime(1992, 07, 19),
				Address = "387 Alcedo St.",
				City = "Austin",
				State = "TX",
				Zip = "37709",
				PhoneNumber = "5125554447",
				Email = "clarence@yoho.com",
				UserName = "clarence@yoho.com",
				DefaultPopcornPoints = 230
			});

			TmpPass.Add("eggsellent");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5024,
				LastName = "Martinez",
				FirstName = "Greg",
				MiddleInitial = "R",
				Birthday = new DateTime(1947, 05, 28),
				Address = "2495 Sunrise Blvd.",
				City = "Red Rock",
				State = "TX",
				Zip = "37662",
				PhoneNumber = "5125556666",
				Email = "gregmartinez@drdre.com",
				UserName = "gregmartinez@drdre.com",
				DefaultPopcornPoints = 70
			});

			TmpPass.Add("rainrain");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5025,
				LastName = "Miller",
				FirstName = "Charles",
				MiddleInitial = "R",
				Birthday = new DateTime(1990, 10, 15),
				Address = "897762 Main St.",
				City = "South Padre Island",
				State = "TX",
				Zip = "37597",
				PhoneNumber = "5125555923",
				Email = "cmiller@bob.com",
				UserName = "cmiller@bob.com",
				DefaultPopcornPoints = 0
			});

			TmpPass.Add("mypuppyspot");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5026,
				LastName = "Nelson",
				FirstName = "Kelly",
				MiddleInitial = "T",
				Birthday = new DateTime(1971, 07, 13),
				Address = "5601 Blue River",
				City = "Disney",
				State = "OK",
				Zip = "74340",
				PhoneNumber = "5125557213",
				Email = "knelson@aoll.com",
				UserName = "knelson@aoll.com",
				DefaultPopcornPoints = 10
			});

			TmpPass.Add("spotmycat");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5027,
				LastName = "Nguyen",
				FirstName = "Joe",
				MiddleInitial = "C",
				Birthday = new DateTime(1984, 03, 17),
				Address = "8249 54th SW St.",
				City = "Del Rio",
				State = "TX",
				Zip = "37841",
				PhoneNumber = "5125557774",
				Email = "joewin@xfactor.com",
				UserName = "joewin@xfactor.com",
				DefaultPopcornPoints = 30
			});

			TmpPass.Add("joejoebob");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5028,
				LastName = "O'Reilly",
				FirstName = "Bill",
				MiddleInitial = "T",
				Birthday = new DateTime(1959, 07, 08),
				Address = "9870 Gato Drive",
				City = "Fort Worth",
				State = "TX",
				Zip = "37746",
				PhoneNumber = "5125551111",
				Email = "orielly@foxnews.cnn",
				UserName = "orielly@foxnews.cnn",
				DefaultPopcornPoints = 120
			});

			TmpPass.Add("bobbyboy");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5029,
				LastName = "Radkovich",
				FirstName = "Anka",
				MiddleInitial = "L",
				Birthday = new DateTime(1966, 05, 19),
				Address = "7900 Mark Pl",
				City = "Plano",
				State = "TX",
				Zip = "37712",
				PhoneNumber = "5125555631",
				Email = "ankaisrad@gogle.com",
				UserName = "ankaisrad@gogle.com",
				DefaultPopcornPoints = 150
			});

			TmpPass.Add("chadgirl");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5030,
				LastName = "Rhodes",
				FirstName = "Megan",
				MiddleInitial = "C",
				Birthday = new DateTime(1965, 03, 12),
				Address = "1187 Carpet Rd.",
				City = "Austin",
				State = "TX",
				Zip = "37705",
				PhoneNumber = "5125557700",
				Email = "megrhodes@freserve.co.uk",
				UserName = "megrhodes@freserve.co.uk",
				DefaultPopcornPoints = 50
			});

			TmpPass.Add("megan55");

			AllCustomers.Add(new AppUser
			{
				UniqueIdentifier = 5031,
				LastName = "Rice",
				FirstName = "Eryn",
				MiddleInitial = "M",
				Birthday = new DateTime(1975, 04, 28),
				Address = "2205 Rio Pequeno",
				City = "Austin",
				State = "TX",
				Zip = "37375",
				PhoneNumber = "5125550006",
				Email = "erynrice@aoll.com",
				UserName = "erynrice@aoll.com",
				DefaultPopcornPoints = 70
			});

			TmpPass.Add("ricearoni");

			//Get instances of the services needed to add a user & add a user to a role
			AppDbContext _context = serviceProvider.GetRequiredService<AppDbContext>();
			UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
			RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

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
						newUser.Birthday = seedCustomer.Birthday;
						newUser.Address = seedCustomer.Address;
						newUser.City = seedCustomer.City;
						newUser.State = seedCustomer.State;
						newUser.Zip = seedCustomer.Zip;
						newUser.DefaultPopcornPoints = seedCustomer.DefaultPopcornPoints;
						newUser.UniqueIdentifier = seedCustomer.UniqueIdentifier;

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

					intCust++;

					if (await _userManager.IsInRoleAsync(newUser, "Customer") == false)
					{
						await _userManager.AddToRoleAsync(newUser, "Customer");
					}

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