using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelExpertsData.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using TravelExpertsData;
using Microsoft.AspNetCore.Mvc.Rendering;

/*
 * This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information, 
    register a new user, a registered user can login/logout, update own data, see own
    packages' data
 *  This is the controller responsible for user authentication
 * Author: Marat Nikitin
 * SAIT, PROJ 207 - Threaded Project 
 * When: February 2022
 */

namespace MVC_TravelExperts.Controllers
{
    public class AccountController : Controller
    {
        // Route: /Account/Login
        public IActionResult Login(string returnUrl = "")
        {
            if (returnUrl != null)
                TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer user) // from the Login form
        {
            Customer usr = UserManager.Authenticate(user.CustEmail, user.CustPassword);
            if (usr == null) // authentication failed
            {
                // saving a message about an authentication error for displaying to a user:
                TempData["Message"] = "Wrong Username-Password combination. Try again.";
                TempData["IsError"] = true;

                return View(); // stay on the login page
            }

            // get customer's id and store it in Session object
            HttpContext.Session.SetInt32("CurrentCustomer", (int)usr.CustomerId);

            // get customer's first name and store it in Session object
            HttpContext.Session.SetString("CurrentCustomerFirstName", usr.CustFirstName.ToString());


            // user != null -- authentication passed
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usr.CustEmail),
                new Claim("FirstName", usr.CustFirstName),
                new Claim("LastName", usr.CustLastName)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies"); // cookies authetication
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("Cookies", claimsPrincipal);

            if (String.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(TempData["ReturnUrl"].ToString());
        }

        /// <summary>
        /// Logging out happens here
        /// </summary>
        /// <returns>A user is redirected to the /Slips page</returns>
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// It's a route for Error 403 - Access Denied
        /// </summary>
        /// <returns> A customized view for the access denied error </returns>
        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            // creating a list of Canadian provinces for a dropdown list:
            List< SelectListItem > provincesList = new List<SelectListItem>()
            {
                new SelectListItem{ Value = "AB", Text = "Alberta" },
                new SelectListItem{ Value = "BC", Text = "British Columbia" },
                new SelectListItem{ Value = "MB", Text = "Manitoba" },
                new SelectListItem{ Value = "NB", Text = "New Brunswick" },
                new SelectListItem{ Value = "NL", Text = "Newfoundland and Labrador" },
                new SelectListItem{ Value = "NS", Text = "Nova Scotia" },
                new SelectListItem{ Value = "NT", Text = "Northwest Territories" },
                new SelectListItem{ Value = "NU", Text = "Nunavut" },
                new SelectListItem{ Value = "ON", Text = "Ontario" },
                new SelectListItem{ Value = "PE", Text = "Prince Edward Island" },
                new SelectListItem{ Value = "QC", Text = "Quebec" },
                new SelectListItem{ Value = "SK", Text = "Saskatchewan" },
                new SelectListItem{ Value = "YT", Text = "Yukon" },
            };
            ViewBag.Provinces = provincesList; // saving list of Canadian provinces in the ViewBag to use
                                               // it later in the dropdown list of the view
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Customer newCustomer)
        {
            try
            {
                MethodsManager1.Add(newCustomer);
                TempData["Message"] = "Your registration was successful. Thank you, " + newCustomer.CustFirstName + "!";
                // it means that TempData["IsError"] will stay null by default
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }
    }
}
