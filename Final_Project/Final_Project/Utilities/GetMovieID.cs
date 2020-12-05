using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.DAL;


namespace Final_Project.Utilities
{
    public class GetMovieID
    {
        public static Int32 GetNextUI(AppDbContext _context)
        {
            //Set a number where the course numbers should start
            const Int32 START_NUMBER = 3000;

            Int32 intMaxUI; //the current maximum course number
            Int32 intNextUI; //the course number for the next class

            if (_context.Movies.Count() == 0) //there are no courses in the database yet
            {
                intMaxUI = START_NUMBER; //course numbers start at 3001
            }
            else
            {
                intMaxUI = _context.Movies.Max(c => c.UniqueIdentifier); //this is the highest number in the database right now
            }

            //You added courses before you realized that you needed this code
            //and now you have some course numbers less than 3000
            if (intMaxUI < START_NUMBER)
            {
                intMaxUI = START_NUMBER;
            }

            //add one to the current max to find the next one
            intNextUI = intMaxUI + 1;

            //return the value
            return intNextUI;
        }
    }
}
