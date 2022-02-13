using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_TravelExperts.Models;
using TravelExpertsData;
using TravelExpertsData.Models;

namespace MVC_TravelExperts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //// Check if there is no session data and there is user data in cookies (i.e. a situation where we close the app while logged in)
            //if (HttpContext.Session.GetInt32("CurrentCustomer") == null)
            //{
            //    // get the customer from cookies and pass it to the LoginAsync action in the AccountController
            //    Customer cust = ProfileManager.GetCustomerFromEmail(User.Identity.Name);

            //    return RedirectToAction("LoginAsync", "Account", new { user = cust });
            //}
            //else
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
