using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Final_Project.DAL;
using Final_Project.Models;
namespace Final_Project.Seeding
{
    public static class SeedGenres
    {
        public static void SeedAllGenres(AppDbContext db)
        {
            List<Genre> AllGenres = new List<Genre>();

            Genre g1 = new Genre() { GenreName = "Thriller" };
            AllGenres.Add(g1);

            Genre g2 = new Genre() { GenreName = "Science Fiction" };
            AllGenres.Add(g2);

            Genre g3 = new Genre() { GenreName = "Comedy" };
            AllGenres.Add(g3);

            Genre g4 = new Genre() { GenreName = "Musical" };
            AllGenres.Add(g4);

            Genre g5 = new Genre() { GenreName = "Fantasy" };
            AllGenres.Add(g5);

            Genre g6 = new Genre() { GenreName = "Western" };
            AllGenres.Add(g6);

            Genre g7 = new Genre() { GenreName = "Animation" };
            AllGenres.Add(g7);

            Genre g8 = new Genre() { GenreName = "Drama" };
            AllGenres.Add(g8);

            Genre g9 = new Genre() { GenreName = "Romance" };
            AllGenres.Add(g9);

            Genre g10 = new Genre() { GenreName = "Adventure" };
            AllGenres.Add(g10);

            Genre g11 = new Genre() { GenreName = "Crime" };
            AllGenres.Add(g11);

            Genre g12 = new Genre() { GenreName = "War" };
            AllGenres.Add(g12);

            Genre g13 = new Genre() { GenreName = "Action" };
            AllGenres.Add(g13);

            Genre g14 = new Genre() { GenreName = "Family" };
            AllGenres.Add(g14);

            Genre g15 = new Genre() { GenreName = "Horror" };
            AllGenres.Add(g15);

            //create a counter and flag to help with debugging
            int intGenreId = 0;
            String strGenreName = "Start";

            //we are now going to add the data to the database
            //this could cause errors, so we need to put this code
            //into a Try/Catch block
            try
            {
                //loop through each of the categories
                foreach (Genre seedGenre in AllGenres)
                {
                    //updates the counters to get info on where the problem is
                    intGenreId = seedGenre.GenreId;
                    strGenreName = seedGenre.GenreName;

                    //try to find the category in the database
                    Genre dbGenre = db.Genres.FirstOrDefault(c => c.GenreName == seedGenre.GenreName);

                    //if the category isn't in the database, dbCategory will be null
                    if (dbGenre == null)
                    {
                        //add the Category to the database
                        db.Genres.Add(seedGenre);
                        db.SaveChanges();
                    }
                    else //the record is in the database
                    {
                        //update all the fields
                        //this isn't really needed for category because it only has one field
                        //but you will need it to re-set seeded data with more fields
                        dbGenre.GenreName = seedGenre.GenreName;
                        //you would add other fields here
                        db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)  //something about adding to the database caused a problem
            {
                //create a new instance of the string builder to make building out 
                //our message neater - we don't want a REALLY long line of code for this
                //so we break it up into several lines
                StringBuilder msg = new StringBuilder();

                msg.Append("There was an error adding the ");
                msg.Append(strGenreName);
                msg.Append(" genre (GenreId = ");
                msg.Append(intGenreId);
                msg.Append(")");

                //have this method throw the exception to the calling method
                //this code wraps the exception from the database with the 
                //custom message we built above.  The original error from the
                //database becomes the InnerException
                throw new Exception(msg.ToString(), ex);
            }

        }

    }

}
