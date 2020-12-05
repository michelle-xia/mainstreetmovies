using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

//TODO: Change this namespace to match your project
namespace Final_Project.Models
{ 
    //NOTE: This is the view model used to allow the user to login
    //The user only needs the email and password to login
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    //NOTE: This is the view model used to register a user
    //When the user registers, they only need to specify the
    //properties listed in this model
    public class RegisterViewModel
    {
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

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //NOTE: Here is the property for phone number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [StringLength(10, ErrorMessage = "The phone number must be 10 digits long. ", MinimumLength = 10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        //TODO: Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        [StringLength(1, ErrorMessage = "The middle initials must be 1 character long. ", MinimumLength = 1)]
        public String MiddleInitial { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        [Display(Name = "Birthday (MM/DD/YYYY Format): ")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public String Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [Display(Name = "State (Initials like 'TX' or 'NV')")]
        [StringLength(2, ErrorMessage = "The state initials must be 2 digits long. ", MinimumLength = 2)]
        public String State { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [Display(Name = "Zip Code")]
        [StringLength(5, ErrorMessage = "The zip code must be 5 digits long. ", MinimumLength =5)]
        public String Zip { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public Boolean MakeEmployee { get; set; }
    }

    public class ChangeAccountViewModel
    {
        public int Age
        {
            get { return CalculateAge(NewBirthday); }
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
        
        [Phone]
        [StringLength(10, ErrorMessage = "The phone number must be 10 digits long. ", MinimumLength = 10)]
        [Display(Name = "New Phone Number")]
        public string NewPhoneNumber { get; set; }

        [Display(Name = "New Birthday (MM/DD/YYYY Format): ")]
        public DateTime NewBirthday { get; set; }

        //public DateTime Birthday { get; set; }

        [Display(Name = "New First Name")]
        public String NewFirst { get; set; }

        [Display(Name = "New Middle")]
        [StringLength(1, ErrorMessage = "The middle initials must be 1 character long. ", MinimumLength = 1)]
        public String NewMiddle { get; set; }

        [Display(Name = "New Last Name")]
        public String NewLast { get; set; }

        [Display(Name = "New Address")]
        public String NewAddress { get; set; }

        [Display(Name = "New City")]
        public String NewCity { get; set; }

        [Display(Name = "New State (Initials like 'TX' or 'NV')")]
        [StringLength(2, ErrorMessage = "The state initials must be 2 digits long. ", MinimumLength = 2)]
        public String NewState { get; set; }

        [Display(Name = "New Zip Code")]
        [StringLength(5, ErrorMessage = "The zip code must be 5 digits long. ", MinimumLength = 5)]
        public String NewZip { get; set; }

    
    }


    //NOTE: This is the view model used to allow the user to 
    //change their password
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }

    //NOTE: This is the view model used to display basic user information
    //on the index page
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }

        public Decimal TotalPopcornPoints { get; set; }

        public String UserID { get; set; }
        public IdentityRole Role { get; set; }


        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public String Address { get; set; }

        public string FullAddress { get; set; }
    }
}
