using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Models
{
    public class MovieReview
    {
        public Int32 MovieReviewID { get; set; }

        [Range(1.0, 5.0)]
        [Display(Name ="Movie Rating ")]
        public int Rating { get; set; }

        [Display(Name ="Review ")]
        [StringLength(280, ErrorMessage = "The {0} cannot exceed 280 characters.")]
        public String FullReview { get; set; }

        [Display(Name ="Has this review been approved? ")]
        public Boolean Approved { get; set; }

        //Navigational Properties
        public Movie Movie { get; set; }
        public AppUser Customer { get; set; }
    }
}
