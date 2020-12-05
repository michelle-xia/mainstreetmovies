using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Models;
using System.ComponentModel.DataAnnotations;


namespace Final_Project.Models
{
    public class Ticket
    {
        public Int32 TicketId { get; set; }

        [Display(Name = "Price ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal TicketPrice { get; set; }

        [Display(Name = "Seat ")]
        [Required(ErrorMessage ="Please select a seat")]
        public String Seat { get; set; }

        [Display(Name ="Discount ")]
        public String Discount { get; set; }

        [Display(Name ="Senior Discount")]
        public Boolean SeniorDiscount { get; set; }

        // Navigational Properties
        public MovieShowing MovieShowing { get; set; }
        public MovieOrder MovieOrder { get; set; }

        public Boolean ValidSeat()
        {
            //List<Ticket> AvailableTickets = MovieShowing.SeatsAvailable;
            if (MovieShowing.SeatsAvailable.Contains(Seat))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
