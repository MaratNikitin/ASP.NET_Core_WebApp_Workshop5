using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_TravelExperts.Models;

/*
 * This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information, 
    register a new user, a registered user can login/logout, update own data, see own
    packages' data
 *  This is the Home controller responsible for serving the Home Page
 * Author: ASP.NET Core MVC
 * SAIT, PROJ 207 - Threaded Project 
 * When: February 2022
 */

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
