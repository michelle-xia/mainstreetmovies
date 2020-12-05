using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public enum TimeofDay { morning, afternoon, evening, night }
    public enum DateOrTime { DateRange, TimeOfDay}
    public class TotalReportFilterViewModel
    {
        [Display(Name = "Filter by movie: ")]
        public int? MovieId { get; set; }

        public int? TotalSeatsSold { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal? TotalRevenue { get; set; }

        [Display(Name = "Filter by MPAA Rating: ")]
        public MPAARatings? MPAARating { get; set; }

        [Display(Name = "Filter by Range of Start Date or Time: (Leave the end field blank if you want to filter just from the start) ")]
        public DateTime? DateRangeStart { get; set; }

        [Display(Name = "Filter by Range of End Date or Time: (Leave the start field blank if you want to filter just from the end) ")]
        public DateTime? DateRangeEnd { get; set; }

        [Display(Name ="Filter by Date or Filter by Time of Day?")]
        public DateOrTime ChoiceTime { get; set; }

        //[Display(Name = "Filter by Time of Day: ")]
        //public TimeofDay? TimeOfDay { get; set; }
    }
}
