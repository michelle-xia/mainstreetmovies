using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.DAL;

namespace Final_Project.Utilities
{
    public class GetConfirmationNumber
    {
        public static Int32 GetNextConfNumber(AppDbContext _context)
        {
            //Set a number where the course numbers should start
            const Int32 START_NUMBER = 10000;

            Int32 intMaxConfNumber; //the current maximum course number
            Int32 intNextConfNumber; //the course number for the next class

            if (_context.MovieOrders.Count() == 0) //there are no courses in the database yet
            {
                intMaxConfNumber = START_NUMBER; //course numbers start at 3001
            }
            else
            {
                intMaxConfNumber = _context.MovieOrders.Max(c => c.ConfirmationNumber); //this is the highest number in the database right now
            }

            //You added courses before you realized that you needed this code
            //and now you have some course numbers less than 3000
            if (intMaxConfNumber < START_NUMBER)
            {
                intMaxConfNumber = START_NUMBER;
            }

            //add one to the current max to find the next one
            intNextConfNumber = intMaxConfNumber + 1;

            //return the value
            return intNextConfNumber;
        }
    }
}
