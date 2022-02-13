using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information, 
    register a new user, a registered user can login/logout, update own data, see own
    packages' data
 *  This is the class responsible for handling the task 3 data from the customers, bookings and packages tables
 * Author: Richard Cook
 * SAIT, PROJ 207 - Threaded Project 
 * When: February 2022
 */
namespace TravelExpertsData.Models
{
    public class CustomerBookingPackageManager
    {

        public static List<Booking> GetBookings()
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Bookings.ToList();
        }

        public static List<Customer> GetCustomers()
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Customers.ToList();
        }

        public static List<Package> GetPackages()
        {
            TravelExpertsContext db = new TravelExpertsContext();
            return db.Packages.ToList();
        }
       
    }

}
