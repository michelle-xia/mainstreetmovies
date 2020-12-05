using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class AppUser : IdentityUser
    {
        //TODO: Add any additional fields you need for your user here
        //UserID, email, password, and phone number inherit from identity class
        DateTime TodayDate = DateTime.Now.Date;
        public IdentityRole Role { get; set; }

        [Display(Name = "First Name ")]
        [Required(ErrorMessage = "First name is required.")]
        public String FirstName { get; set; }

        [Display(Name = "Middle Initial ")]
        [StringLength(1, ErrorMessage = "The middle initials must be 1 character long. ", MinimumLength = 1)]
        public String MiddleInitial { get; set; }

        [Display(Name = "Last Name ")]
        [Required(ErrorMessage = "Last name is required.")]
        public String LastName { get; set; }

        [Display(Name ="Full Name ")]
        public String FullName
        {
            get
            {
                if (MiddleInitial != "")
                {
                    return FirstName + " " + MiddleInitial + " " + LastName;
                }
                else
                {
                    return FirstName + " " + LastName;
                }
            }
        }

        public int UniqueIdentifier { get; set; }
    
        [Display(Name = "Address ")]
        [Required(ErrorMessage = "Address is required.")]
        public String Address { get; set; }

        [Display(Name = "City ")]
        [Required(ErrorMessage = "Address is required.")]
        public String City { get; set; }

        //need to add validation for making sure it's a real state, maybe creating some type of enum
        [Display(Name = "State (Initials like TX or NV): ")]
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(2, ErrorMessage = "The state initials must be 2 digits long. ", MinimumLength = 2)]
        public String State { get; set; }

        [Display(Name = "Zip Code ")]
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(5, ErrorMessage = "The zip code must be 5 digits long. ", MinimumLength = 5)]
        public String Zip { get; set; }

        [Display(Name = "Full Address ")]
        public String FullAddress
        {
            get
            {
                string fa = " ";
                if (Address != "")
                {
                    fa = fa + Address;
                }
                if (City != "")
                {
                    fa = fa + " " + City;
                }
                if (State != "")
                {
                    fa = fa + " " + State;
                }
                if (Zip != "")
                {
                    fa = fa + " " + Zip;
                }
                return fa;
            }
        }


        [Display(Name = "Birthday ")]
        [DisplayFormat(DataFormatString ="{0:MMM dd, yyyy}")]
        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public int Age
        {
            get { return CalculateAge(Birthday); }
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        public int DefaultPopcornPoints { get; set; }

        [Display(Name = "Popcorn Points ")]
        public int TotalPopcornPoints
        {
            get { return DefaultPopcornPoints + MovieOrders.Sum(m => m.PopcornPointsEarned); }
        }
        
        [Display(Name = "Active Account Status ")]
        public bool Active { get; set; }

        //Navigational Properties
        public List<MovieReview> MovieReviews { get; set; }

        [InverseProperty("Customer")]
        public List<MovieOrder> MovieOrders { get; set; }

        [InverseProperty("Recipient")]
        public List<MovieOrder> OrdersReceived { get; set; }

        public AppUser()
        {
            if (MovieReviews == null)
            {
                MovieReviews = new List<MovieReview>();
            }

            if (MovieOrders is null)
            {
                MovieOrders = new List<MovieOrder>();
            }

            if (OrdersReceived is null)
            {
                OrdersReceived = new List<MovieOrder>();
            }
        }
        
    }
}
