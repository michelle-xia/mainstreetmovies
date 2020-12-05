using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public enum MPAARatings { G, PG, [Display(Name = "PG-13")]PG13, R, [Display(Name ="NC-17")]NC17, Unrated}

    public class Movie
    {
        [Key]
        public int ID { get; set; }
        public int MovieId { get; set; }
        
        [Display(Name = "MovieUID")]
        public int UniqueIdentifier { get; set; }

        [Required]
        [Display(Name = "Title ")]
        public String Title { get; set; }

        [Display(Name = "Tagline ")]
        public String Tagline { get; set; }

        [Display(Name = "Release Year ")]
        public String ReleaseYear
        {
            get { return ReleaseDate.Year.ToString(); }
        }

        [Required]
        [Display(Name = "Release Date ")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "MPAA Rating ")]
        public MPAARatings MPAARating { get; set; }

        [Display(Name = "Average Customer Rating ")]
        public Decimal CustomerRating
        {
            get
            {
                List<MovieReview> reviews = MovieReviews;
                reviews.RemoveAll(t => t.Approved == false);
                if (reviews.Count() > 0)
                {                  
                    return Convert.ToDecimal(reviews.Average(r => r.Rating));

                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public Double Similarity { get; set; }

        [Required]
        [Display(Name = "Movie Overview ")]
        public String MovieOverview { get; set; }

        [Required]
        [Display(Name = "Actors ")]
        public String Actors  { get; set; }

        [Required]
        [Display(Name = "Runtime ")]
        public int Runtime { get; set; }

        [Display(Name = "Revenue ")]
        [DisplayFormat(DataFormatString = "{0:c0}")]
        public Decimal? Revenue { get; set; }

        //Navigational Properties
        public List<MovieShowing> MovieShowings { get; set; }

        public List<MovieReview> MovieReviews { get; set; }

        public Genre Genre { get; set; }

        //Constructor
        public Movie()
        {
            if (MovieReviews is null)
            {
                MovieReviews = new List<MovieReview>();
            }

            if (MovieShowings is null)
            {
                MovieShowings = new List<MovieShowing>();
            }
        }
    }
}
