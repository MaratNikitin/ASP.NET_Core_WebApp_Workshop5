using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

/*
 * This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information, 
    register a new user, a registered user can login/logout, update own data, see own
    packages' data
 * This file is used for authenticating a user. 
 * Author: Marat Nikitin
 * SAIT, PROJ 207 - Threaded Project 
 * When: February 2022
 */

namespace TravelExpertsData
{
    public class UserManager
    {
        private readonly static List<Customer> _customers; // making it available at the class level

        static UserManager()  // initializes static list of users
        {
            TravelExpertsContext db = new TravelExpertsContext();
            _customers = db.Customers.ToList();
        }


        /// <summary>
        /// User (a customer) is authenticated based on credentials and a user returned if exists or null if not.
        /// </summary>
        /// <param name="username">Username as string</param>
        /// <param name="password">Password as string</param>
        /// <returns>A Customer class object or null.</returns>
        /// <remarks>
        /// 
        /// </remarks>
        public static Customer Authenticate(string username, string password)
        {
            var user = _customers.SingleOrDefault(usr => usr.CustEmail == username
                                                    && usr.CustPassword == password);
            return user; //this will either be null or an object
        }
    }
}
