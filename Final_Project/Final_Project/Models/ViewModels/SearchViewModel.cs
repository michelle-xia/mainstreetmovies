using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public enum CustomerRatingBounds { GreaterThan, LessThan };

    public class SearchViewModel
    {
        [Display(Name ="Title to search for: ")]
        public String SearchTitle { get; set; }

        [Display(Name = "Tagline to search for: ")]
        public String SearchTagline { get; set; }

        //This will be a SelectList
        [Display(Name = "Genre to search for: ")]
        public int SearchGenre { get; set; }

        [Display(Name = "Release Year to search for: ")]
        public String SearchReleaseYear { get; set; }

        [Display(Name = "Date to search after (leave end blank to only search after this date): ")]
        public DateTime? SearchDateStart { get; set; }

        [Display(Name = "Date to search before (leave start blank to only search before this date): ")]
        public DateTime? SearchDateEnd { get; set; }

        //This will be a Radio
        [Display(Name = "MPAA Rating to search for: ")]
        public MPAARatings? SearchMPAARating { get; set; }

        [Range(1.0, 5.0, ErrorMessage ="Criteria must be between 1.0 and 5.0")]
        [Display(Name = "Customer Rating Minimum to search for (leave blank if only searching by Minimum): ")]
        public Decimal? MinimumRating { get; set; }

        [Range(1.0, 5.0, ErrorMessage = "Criteria must be between 1.0 and 5.0")]
        [Display(Name = "Customer Rating Maximum to search for (leave blank if only searching by Maximum): ")]
        public Decimal? MaximumRating { get; set; }

        public CustomerRatingBounds CustomerRatingBound { get; set; }

        [Display(Name = "Actor/actress to search for: ")]
        public String SearchActor { get; set; }
    }
}
