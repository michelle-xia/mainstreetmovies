using Final_Project.DAL;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Controllers
{
    public class SeedController : Controller
    {
        //You will need an instance of the AppDbContext class for this code to work
        //Create a private variable to hold the AppDbContext object
        private readonly AppDbContext _context;

        //Create a constructor for this class that accepts an instance of AppDbContext
        //The code in Startup.cs configures the project to provide an instance of AppDbContext
        //when Controller classes are instantiated.
        public SeedController(AppDbContext dbContext)
        {
            //Set the private variable equal to the instance that was
            //passed into the constructor
            _context = dbContext;
        }
        
        //This is the action method for the Seeding page with the two buttons
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeedAllGenres()
        {
            //this code may throw an exception, so we need to be in a Try/Catch block 
            try
            {
                //call the SeedCategories method from your Seeding folder
                //you will need to pass in the instance of AppDbContext
                //that you set in the constructor
                Seeding.SeedGenres.SeedAllGenres(_context);
            }
            catch (Exception ex)
            {
                //add the error messages to a list of strings
                List<String> errorList = new List<String>();

                //Add the outer message
                errorList.Add(ex.Message);

                //Add the message from the inner exception
                errorList.Add(ex.InnerException.Message);

                //Add additional inner exception messages, if there are any
                if (ex.InnerException.InnerException != null)
                {
                    errorList.Add(ex.InnerException.InnerException.Message);
                }

                return View("Error", errorList);

            }

            //everything is okay - send user to confirmation page
            return View("Confirm");
        }


        public IActionResult SeedAllMovies()
        {
            //this code may throw an exception, so we need to be in a Try/Catch block 
            try
            {
                //call the SeedCategories method from your Seeding folder
                //you will need to pass in the instance of AppDbContext
                //that you set in the constructor
                Seeding.SeedMovies.SeedAllMovies(_context);
            }
            catch (Exception ex)
            {
                //add the error messages to a list of strings
                List<String> errorList = new List<String>();

                //Add the outer message
                errorList.Add(ex.Message);

                //Add the message from the inner exception
                errorList.Add(ex.InnerException.Message);

                //Add additional inner exception messages, if there are any
                if (ex.InnerException.InnerException != null)
                {
                    errorList.Add(ex.InnerException.InnerException.Message);
                }

                return View("Error", errorList);

            }

            //everything is okay - send user to confirmation page
            return View("Confirm");
        }

        public IActionResult SeedAllPrices()
        {
            //this code may throw an exception, so we need to be in a Try/Catch block 

            //call the SeedCategories method from your Seeding folder
            //you will need to pass in the instance of AppDbContext
            //that you set in the constructor
            Seeding.SeedPrices.SeedAllPrices(_context);
            return View("Confirm");

        }

        public IActionResult SeedAllShowings()
        {
            //this code may throw an exception, so we need to be in a Try/Catch block 
            try
            {
                //call the SeedCategories method from your Seeding folder
                //you will need to pass in the instance of AppDbContext
                //that you set in the constructor
                Seeding.SeedShowings.SeedAllShowings(_context);
            }
            catch (Exception ex)
            {
                //add the error messages to a list of strings
                List<String> errorList = new List<String>();

                //Add the outer message
                errorList.Add(ex.Message);

                //Add the message from the inner exception
                errorList.Add(ex.InnerException.Message);

                //Add additional inner exception messages, if there are any
                if (ex.InnerException.InnerException != null)
                {
                    errorList.Add(ex.InnerException.InnerException.Message);
                }

                return View("Error", errorList);

            }

            //everything is okay - send user to confirmation page
            return View("Confirm");
        }
    }
}
