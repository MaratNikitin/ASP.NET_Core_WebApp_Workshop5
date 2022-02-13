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
        /// <summary>
        /// Retrieves a customer object based on the custId passed to it.
        /// </summary>
        /// <param name="custID">the int cust id</param>
        /// <returns>A customer object</returns>
        public static Customer GetCustomerByID(int custID)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer customer = db.Customers.Single(c => c.CustomerId == custID);
            return customer;
        }

        /// <summary>
        /// Updates a customers information
        /// </summary>
        /// <param name="newCustInfo">customer object with the updated info</param>
        /// <param name="custId">customer id as an int</param>
        public static void UpdateCustomerInfo(Customer newCustInfo)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer oldCustInfo = db.Customers.Where(c => c.CustomerId == newCustInfo.CustomerId).Single();

            oldCustInfo.CustFirstName = newCustInfo.CustFirstName;
            oldCustInfo.CustLastName = newCustInfo.CustLastName;
            oldCustInfo.CustAddress = newCustInfo.CustAddress;
            oldCustInfo.CustCity = newCustInfo.CustCity;
            oldCustInfo.CustProv = newCustInfo.CustProv;
            oldCustInfo.CustPostal = newCustInfo.CustPostal;
            oldCustInfo.CustCountry = newCustInfo.CustCountry;
            oldCustInfo.CustHomePhone = newCustInfo.CustHomePhone;
            oldCustInfo.CustBusPhone = newCustInfo.CustBusPhone;
            oldCustInfo.CustEmail = newCustInfo.CustEmail;

            db.SaveChanges();
        }

        /// <summary>
        /// Updates a customers password
        /// </summary>
        /// <param name="newCustInfo">customer object with the updated info</param>
        /// /// <param name="custId">customer id as an int</param>
        public static void UpdateCustomerPassword(Customer newCustInfo)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer oldCustInfo = db.Customers.Where(c => c.CustomerId == newCustInfo.CustomerId).Single();

            oldCustInfo.CustPassword = newCustInfo.CustPassword;
            oldCustInfo.ConfirmPassword = newCustInfo.ConfirmPassword;

            db.SaveChanges();
        }

        /// <summary>
        /// Gets a customers password given their customer id
        /// </summary>
        /// <param name="custID">customer id as an int</param>
        /// <returns>password as a string</returns>
        public static String GetPasswordByID(int custID)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            String password = db.Customers.Where(c => c.CustomerId == custID).Select(c => c.CustPassword).ToString();
            return password;
        }
    }
}
