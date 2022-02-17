using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_TravelExperts.Models;
using System.Collections.Generic;
using System.Linq;
using TravelExpertsData.Models;
/*
*This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information,
  register a new user, a registered user can login/logout, update own data, see own
    packages' data
 *  This is the controller for handling the task 3 data from the customers, bookings and packages tables 
 *  from the CustomerBookingOackageViewModel
 * Author: Richard Cook
 * SAIT, PROJ 207 - Threaded Project
 * When: February 2022
 */
namespace MVC_TravelExperts.Controllers
{
    public class CustomerBookingPackageController : Controller
    {
        // GET: CustomerBookingPackageController
        public IActionResult List()
        {
            return View();
        }

        /// <summary>
        /// display all packages as per a customer and booking
        /// </summary>
        /// <returns></returns>
        // GET: CustomerBookingPackageController
        [Authorize]
        public ActionResult Index()
        {

            
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            if (customerID == null)
                customerID = 0;
            //generating list objects that have the data from hte packages, customers and booking model class
            List<Package> packageList = CustomerBookingPackageManager.GetPackages();
            List<Customer> customerList = CustomerBookingPackageManager.GetCustomers();
            List<Booking> bookingList = CustomerBookingPackageManager.GetBookings();

            //List<CustomerBookingPackageViewModel> list = new List<CustomerBookingPackageViewModel>();

            //assigning to a list a query result which joins the aforementioned 3 tables 
            List<CustomerBookingPackageViewModel> combinedList =
                (
                //sql query to join the tables and filter per the customer logged in via the where statement
                 from package in packageList
                 join booking in bookingList
                 on package.PackageId equals booking.PackageId
                 join customer in customerList
                 on booking.CustomerId equals customer.CustomerId
                 where customer.CustomerId == customerID
                 select new CustomerBookingPackageViewModel
                 {
                     
                     FName = customer.CustFirstName,
                     LName = customer.CustLastName,
                     Traveler = booking.TravelerCount,
                     PackageName = package.PkgName,
                     PackageBasePrice =package.PkgBasePrice,
                     SubTotal = (decimal)booking.TravelerCount* package.PkgBasePrice
                 }).ToList();
            //a for each loop that navigates the list object and outputs the subtotal to the totalValue variable.  output via the viewbag
            decimal totalValue = 0;
            foreach(CustomerBookingPackageViewModel item in combinedList)
            {
                totalValue += item.SubTotal;
            }
           ViewBag.Total = totalValue.ToString("c");


            return View(combinedList);
        }

        // GET: CustomerBookingPackageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerBookingPackageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerBookingPackageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerBookingPackageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerBookingPackageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerBookingPackageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerBookingPackageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
