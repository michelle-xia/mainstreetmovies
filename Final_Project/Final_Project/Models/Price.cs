using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Final_Project.Models
{
    public class Price
    {
        public Int32 PriceID { get; set; }

        // Before noon on weekdays
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal MatineePrice { get; set; }

        // 12 - 5pm Tuesdays inclusive
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal TuesdayPrice { get; set; }

        //After noon weekdays excluding Tuesdays and Fridays
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal WeekdayPrice { get; set; }

        // Fri after 12pm, Sat, Sun all day
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal WeekendPrice { get; set; }

        //If Movie is a special
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal SpecialPrice { get; set; }

    }
}
