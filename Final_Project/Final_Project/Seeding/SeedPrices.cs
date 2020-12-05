using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Final_Project.DAL;
using Final_Project.Models;
namespace Final_Project.Seeding
{
    public static class SeedPrices
    {
        public static void SeedAllPrices(AppDbContext db)
        {
            Price price = new Price
            {
                MatineePrice = 5.00m,
                TuesdayPrice = 8.00m,
                WeekdayPrice = 10.00m,
                WeekendPrice = 12.00m,
                SpecialPrice = 12.00m
            };

           
            //Add the job posting to the database
            db.Prices.Add(price);
            db.SaveChanges();

        }
    }
}
