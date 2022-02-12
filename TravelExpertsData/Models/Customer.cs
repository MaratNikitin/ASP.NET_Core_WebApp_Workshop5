using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

/*
*This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information,
   register a new user, a registered user can login/logout, update own data, see own
    packages' data
 * This class was created by Entity Framework Core and then modified to ensure data
    validation in the customer registration form
 * Author: Marat Nikitin
 * SAIT, PROJ 207 - Threaded Project
 * When: February 2022
*/

namespace TravelExpertsData.Models
{
    [Index(nameof(AgentId), Name = "EmployeesCustomers")]
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CreditCards = new HashSet<CreditCard>();
            CustomersRewards = new HashSet<CustomersReward>();
        }

        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "First Name")] //this quoted name is displayed in a column header 
        public string CustFirstName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Last Name")] //this quoted name is displayed in a column header 
        public string CustLastName { get; set; }

        [Required]
        [StringLength(75)]
        [Display(Name = "Home Address")]
        public string CustAddress { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string CustCity { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Province")]
        public string CustProv { get; set; }

        [Required]
        [StringLength(7)]
        [RegularExpression(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][\s][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]",
         ErrorMessage = "Please enter a valid Canadian postal code in 'A0A 0A0' format")]
        [Display(Name = "Postal Code")]
        public string CustPostal { get; set; }

        [StringLength(25)]
        [Display(Name = "Country")]
        public string CustCountry { get; set; }

        [StringLength(20)]
        [RegularExpression(@"\d{10}",
         ErrorMessage = "Please enter phone # in XXXXXXXXXX format (10 digits only)")]
        [Display(Name = "Home Phone")]
        public string CustHomePhone { get; set; }

        [Required]
        [RegularExpression(@"\d{10}",
         ErrorMessage = "Please enter phone # in XXXXXXXXXX format (10 digits only)")]
        [StringLength(20)]
        [Display(Name = "Cell (Bus) Phone")]
        public string CustBusPhone { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$",
            ErrorMessage = "Please enter a correct e-mail")] // got this RegEx from Stack Overflow
        [Display(Name = "E-mail")]
        public string CustEmail { get; set; }

        [StringLength(20)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter password")]
        [Compare("ConfirmPassword")]
        public string CustPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Display(Name = "Confirm Password")]
        [NotMapped] // excluding from DB tracking
        public string ConfirmPassword { get; set; }

        [Display(Name = "Agent ID")]
        public int? AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        [InverseProperty("Customers")]
        public virtual Agent Agent { get; set; }
        [InverseProperty(nameof(Booking.Customer))]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty(nameof(CreditCard.Customer))]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty(nameof(CustomersReward.Customer))]
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}
