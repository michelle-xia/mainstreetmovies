using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{

    public class ReportViewModel
    {
        // total seats sold
        // total revenue
        // or both
        [Display(Name ="Movie Title")]
        public String MovieTitle { get; set; }

        [Display(Name ="Customer Name")]
        public String CustomerName { get; set; }

        [Display(Name = "Total Seats Sold")]
        public int SeatsSold { get; set; }

        [Display(Name = "Total Revenue")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal TotalRevenue { get; set; }

        [Display(Name = "Showing Date")]
        public DateTime ShowingDate { get; set; }

        [Display(Name ="Date Purchased")]
        public DateTime DatePurhcased { get; set; }
        // limited by date range
        // limited by movies
        // limited by MPAA Rating
        // limited by time of day

        // seats sold and revenue by customers
        // display transactions and cancellations

        // all tickets sold by popcorn points
        // movie title, date of movie, customer name, and date purchased


    }
}

