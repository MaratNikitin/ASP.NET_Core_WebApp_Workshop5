using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExpertsData.Models;

/*
* This class is used for displaying and changing customer data once a user is logged in.
* Author: Scott Holmes
* SAIT, PROJ 207 - Threaded Project
* When: February 2022
*/
namespace TravelExpertsData
{
    public class ProfileManager
    {
        private readonly static List<Customer> _customers; // available at the class level only

        static ProfileManager()  // initializes static list of users
        {
            TravelExpertsContext db = new TravelExpertsContext();
            _customers = db.Customers.ToList();
        }

        /// <summary>
        /// Retrieves a customer object based on the custId passed to it.
        /// </summary>
        /// <param name="custID">the int cust id</param>
        /// <returns>A customer object</returns>
        public static Customer GetCustomerByID(int custID)
        {
            Customer customer = _customers.Single(c => c.CustomerId == custID);
            return customer;
        }
    }
}
