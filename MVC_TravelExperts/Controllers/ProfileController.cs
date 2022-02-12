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
        // When a user clicks to view their profile
        public IActionResult Index()
        {
            int custId = (int)HttpContext.Session.GetInt32("CurrentCustomer");
            Customer currentCustomer = ProfileManager.GetCustomerByID(custId); // Get the customer object using their cust id
            return View(currentCustomer);
        }
    }
}
