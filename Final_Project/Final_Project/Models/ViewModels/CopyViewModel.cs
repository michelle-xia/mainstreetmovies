using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Final_Project.Models;

namespace Final_Project.Models
{
    public class CopyViewModel
    {
        [Display(Name ="Theater to copy from")]
        public TheaterType CopyFromTheater { get; set; }

        [Display(Name = "Theater to copy to")]
        public TheaterType CopyToTheater { get; set; }

        [Display(Name ="Date to Copy Schedule From")]
        [Required(ErrorMessage ="Select a Date to Copy From")]
        public DateTime CopyFromDate { get; set; }

        [Display(Name = "Date to Copy Schedule To")]
        [Required(ErrorMessage = "Select a Date to Copy To")]
        public DateTime CopyToDate { get; set; }

        public CopyViewModel()
        {
        }
    }
}
