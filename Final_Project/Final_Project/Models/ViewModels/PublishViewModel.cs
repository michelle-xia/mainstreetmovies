using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class PublishViewModel
    {
        [Display(Name ="Schedule Date to Publish")]
        [Required(ErrorMessage ="You must select a date to publish.")]
        public DateTime DateSelected { get; set; }

        public PublishViewModel()
        {
        }
    }
}
