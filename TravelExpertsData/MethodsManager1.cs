/*
 * This web app creates a website of a fictional travel company called Travel Experts.
    The website allows users to learn about the company, see contact information, 
    register a new user, a registered user can login/logout, update own data, see own
    packages' data
 * This file is a repository of methods for working with all data of the TravelExperts database 
 * Author: Marat Nikitin
 * SAIT, PROJ 207 - Threaded Project 
 * When: February 2022
 */

namespace TravelExpertsData.Models
{
    public static class MethodsManager1
    {
        /// <summary>
        /// Adds a new customer to DB
        /// </summary>
        /// <param name="newCustomer">New customer to add</param>
        public static void Add(Customer newCustomer)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            newCustomer.CustomerId = 0; // it is needed to get around that problem of
                                // inserting no CustomerID for aut0 creating CustomerID
            db.Customers.Add(newCustomer);
            db.SaveChanges();
        }
    }
}
