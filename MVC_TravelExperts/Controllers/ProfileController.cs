using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Profile(string id = "My Info")
        {
            // Get the customer object using their cust id
            int custId = (int)HttpContext.Session.GetInt32("CurrentCustomer");
            Customer currentCustomer = ProfileManager.GetCustomerByID(custId); 

            // turn the phone numbers into the correct display format (123-456-7890) and put them in the ViewBag
            string custHomePhone = currentCustomer.CustHomePhone.ToString();
            string custBusPhone = currentCustomer.CustBusPhone.ToString();
            ViewBag.HomePhone = custHomePhone.Substring(0, 3) + '-' + custHomePhone.Substring(3, 3) + '-' + custHomePhone.Substring(6, 4);
            ViewBag.BusPhone = custBusPhone.Substring(0, 3) + '-' + custBusPhone.Substring(3, 3) + '-' + custBusPhone.Substring(6, 4);

            // Make a list of values we want to filter by and pass it to the ViewBag
            List<String> filters = new List<string> { "My Info", "Change Password" };
            ViewBag.Filters = filters;
            ViewBag.SelectedFilter = id;

            // return the view the customer wants to see
            if (id == "My Info") // view their information
                return View(currentCustomer);

            else // change password
                return View("Password", currentCustomer);
        }
    }
}
