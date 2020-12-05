using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Models
{
    public class Genre
    {
        public int GenreId { get; set; }

        [Display(Name ="Genre Name ")]
        public String GenreName { get; set; }

        //Navigational Properties
        public List<Movie> Movies { get; set; }

        public Genre()
        {
            if (Movies is null)
            {
                Movies = new List<Movie>();
            }
        }
    }
}
