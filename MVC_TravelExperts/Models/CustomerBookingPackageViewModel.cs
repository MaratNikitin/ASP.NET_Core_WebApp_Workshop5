using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
/*
*This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information,
  register a new user, a registered user can login/logout, update own data, see own
    packages' data
 *  This is the view model for handling the task 3 data from the customers, 
 *  from the list object List<CustomerBookingPackageViewModel> called in the CustomerBookingPackageController
 * Author: Richard Cook
 * SAIT, PROJ 207 - Threaded Project
 * When: February 2022
 */
namespace MVC_TravelExperts.Models
{
    public class CustomerBookingPackageViewModel
    {
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Display(Name = "# of Travelers")]
        public double? Traveler { get; set; }

        [Display(Name = "Package Name")]
        public string PackageName { get; set; }

        [Display(Name = "Package Base Price")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal PackageBasePrice { get; set; }

        // you could even make the numeric properties with type string
        // this class is only used for passing to the view

        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal SubTotal{ get; set; }

        

      
    }
}
