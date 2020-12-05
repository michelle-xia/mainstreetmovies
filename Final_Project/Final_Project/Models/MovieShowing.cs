using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public enum TheaterType {One, Two}
    public enum PublishStatus {Unpublished, Published, Canceled}

    public class MovieShowing
    {
        //private List<String>_availableSeats;

        public const int TOTAL_SEATS = 20;

        public Int32 MovieShowingID { get; set; }

        public String FullShowingName
        {
            get
            {
                if (Movie != null)
                {
                    return Movie.Title + " " + StartTime.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        [Required(ErrorMessage ="Start Time Required")]
        [Display(Name ="Start Time ")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time ")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Theater ")]
        public TheaterType TheaterSelection {get; set; }

        //Confirmed Scheduling or not
        [Display(Name = "Is this movie showing published?")]
        public Boolean IsPublished { get; set; }

        [Display(Name ="Is this movie a special?")]
        public Boolean IsSpecial { get; set; }

        public PublishStatus ShowingStatus { get; set; }

        [NotMapped]
        public List<String> DefaultSeats
        {
            get
            {
                List<String> tmp = new List<String>();
                List<String> Letters = new List<string>()
                {
                    "A", "B", "C", "D"
                };

                foreach (String l in Letters)
                {
                    for (int i = 1; i < 6; i++)
                    {
                        String iStr = i.ToString();
                        String temp = l + iStr;
                        tmp.Add(temp);
                    }
                }
                return tmp;
            }
        }

        [Display(Name ="Number of Seats Available ")]
        public int NumberSeatsAvailable
        {
            get { return SeatsAvailable.Count(); }
        }

        [NotMapped]
        [Display(Name ="Available Seats ")]
        public List<String> SeatsAvailable
        {
            get
            {
                List<String> availableSeats = DefaultSeats;
                foreach (Ticket t in Tickets)
                {
                    if (t.MovieOrder.OrderStatus != Status.Cancelled)
                    {
                        availableSeats.Remove(t.Seat);
                    }                  
                }
                return availableSeats;
            }
        }

        // Navigational Properties
        public List<Ticket> Tickets { get; set; }

        public Movie Movie { get; set; }

        public MovieShowing()
        {
            if (Tickets is null)
            {
                Tickets = new List<Ticket>();
            }

        }
    }
}
