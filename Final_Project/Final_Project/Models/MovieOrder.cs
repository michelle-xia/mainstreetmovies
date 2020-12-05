using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Final_Project.Models
{
    public enum Status { Unfinished, Cancelled, Past, Future }
    public class MovieOrder
    {
        const Decimal SALES_TAX_RATE = 0.0825m;

        public int MovieOrderID { get; set; }
        
        [Display(Name = "Confirmation Number ")]
        public int ConfirmationNumber { get; set; }

        [Display(Name ="Order Confirmed? ")]
        public Boolean isConfirmed { get; set; }

        [Display(Name="Email address of gift recipient (optional) ")]
        public String GiftRecipient { get; set; }

        [Display(Name = "Popcorn Points Earned ")]
        public int PopcornPointsEarned { get; set; }

        [Display(Name = "Is this order a gift?")]
        public Boolean isGift { get; set; }

        [Display(Name ="Popcorn Points Paid")]
        public int PopcornPointsPaid
        {
            get
            {
                if (UsingPopcornPoints == true)
                {
                    return -1 * PopcornPointsEarned;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int PopcornPointsCost
        {
            get
            {
                if (Tickets.Count() > 0)
                {
                    return 100 * Tickets.Count();
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public Boolean OrderDiscount { get; set; }

        [Display(Name = "Subtotal ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal SubTotal
        {
            get
            {
                /*if (Customer != null)
                {
                    if (Customer.Age >= 60)
                    {
                        SeniorDiscount();
                    }
                }*/              
                Decimal tempSubTotal = 0m;
                foreach (Ticket t in Tickets)
                {
                    tempSubTotal += t.TicketPrice;
                }               
                return tempSubTotal;
            }
        }

        [Display(Name ="Tax (Tax rate 8.25%) ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Tax
        {
            get { return SubTotal * SALES_TAX_RATE; }
        }

        [Display(Name ="Using Popcorn Points? ")]
        public Boolean UsingPopcornPoints { get; set; }

        [Display(Name = "Total ")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public Decimal Total
        {
            get { return SubTotal + Tax; }

        }

        [Display(Name = "Date Ordered ")]
        public DateTime DateOrdered { get; set; }

        [Display(Name = "Order Status ")]
        public Status OrderStatus { get; set; }

        // Navigational Properties
        public AppUser Customer { get; set; }

        public AppUser Recipient { get; set; }

        public List<Ticket> Tickets { get; set; }

        public MovieOrder()
        {
            if (Tickets is null)
            {
                Tickets = new List<Ticket>();
            }
        }

        public void SeniorDiscount()
        {
            int count = 0;
            foreach (Ticket t in Tickets)
            {
                if (count == 2)
                {
                    break;
                }
                if (t.MovieShowing != null)
                {
                    if (t.MovieShowing.IsSpecial == true)
                    {
                        continue;
                    }
                }              
                t.TicketPrice = t.TicketPrice - 2;
                t.SeniorDiscount = true;
                count += 1;
            }
        }

        public Boolean EligibleForPopcornPoints()
        {
            if (Customer.TotalPopcornPoints > Tickets.Count() * 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdatePopcornPoints()
        {
            if(UsingPopcornPoints == false)
            {
                PopcornPointsEarned = Decimal.ToInt32(SubTotal);
            }
            else
            {
                PopcornPointsEarned = -1 * Tickets.Count() * 100;
            }
        }

    }
}
