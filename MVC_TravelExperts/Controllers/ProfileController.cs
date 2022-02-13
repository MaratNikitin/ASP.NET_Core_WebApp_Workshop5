using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;
using TravelExpertsData.Models;

/*
* This is the controller responsible for updating a users data once they are logged in
* Author: Scott Holmes
* SAIT, PROJ 207 - Threaded Project
* When: February 2022
*/

namespace MVC_TravelExperts.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Profile", "Profile");
        }

        // When a user clicks to view their profile
        [Route("[controller]s/{id?}")]
        public IActionResult Profile(string id = "My Profile")
        {
            // Get the customer object using their cust id
            int? custId = HttpContext.Session.GetInt32("CurrentCustomer");
            // make sure we have a cust id, and if not, get it from cookies
            if(custId == null)
            {
                custId = ProfileManager.GetCustIdFromEmail(User.Identity.Name); // get the cust id from the email in cookies
            }

            Customer currentCustomer = ProfileManager.GetCustomerByID((int)custId); 

            // Make a list of values we want to filter by and pass it to the ViewBag
            List<String> filters = new List<string> { "My Profile", "Update My Profile", "Update Password" };
            ViewBag.Filters = filters;
            ViewBag.SelectedFilter = id;

            // return the view the customer wants to see
            if (id == "My Profile") // view their information
                return View(currentCustomer);

            else if(id == "Update My Profile") // they want to change their info
            {
                return View("UpdateProfile", currentCustomer);
            }

            else // change password
                return View("ConfirmPassword", currentCustomer);
        }

        // when a user updates their information
        [HttpPost]
        public IActionResult Update(Customer updateCust)
        {
            try
            {
                ProfileManager.UpdateCustomerInfo(updateCust);
                TempData["Message"] = "Your information was successfully updated!";
            }
            catch
            {
                TempData["IsError"] = "There was an unexpected error while trying to update your information: ";
            }
            return RedirectToAction("Index");
        }

        // when a user confirms their old password to change it
        [HttpPost]
        public IActionResult ConfirmPassword(Customer updateCust)
        {
            // get the oldpassword and compare it to the values entered
            string oldPassword = ProfileManager.GetPasswordByID(updateCust.CustomerId);
            if(updateCust.CustPassword == oldPassword) // the passwords match
            {
                return View("ChangePassword"); // send them to the changepassword page
            }
            else // the password does not match
            {
                TempData["IsError"] = true;
                TempData["Message"] = "You did not enter the correct password.";
                return RedirectToAction("Profile", new { id = "Update Password" });
            }
        }

        // when a user changes their password
        [HttpPost]
        public IActionResult ChangePassword(Customer updateCust)
        {
            try
            {
                ProfileManager.UpdateCustomerPassword(updateCust);
                TempData["Message"] = "Your password has been changed";
            }
            catch
            {
                TempData["IsError"] = true;
                TempData["Message"] = "There was an unexpected error while trying to change your password";
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}
