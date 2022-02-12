using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData.Models
{
    public class BookingManager
    {

        //probably dont need this, but i included it just in case
        public static List<Booking> GetAll()
        {
            TravelExpertsContext db = new TravelExpertsContext();
            List<Booking> bookings = db.Bookings.Include(r => r.BookingId)
                .Include(r => r.BookingDate)
                .Include(r => r.BookingNo)
                .Include(r => r.TravelerCount)
                .Include(r => r.CustomerId)
                .Include(r => r.TripTypeId)
                .Include(r => r.PackageId).ToList();
                
            return bookings;
        }


        /// <summary>
        /// joins the customer, bookings and packages tables in order to filter out the customers booking information
        /// joins are done via the navigation properties in the booking model and thus sql join code isnt necessary
        /// linq lambas should work here
        /// </summary>
        /// <returns></returns>
        public static List<Booking> GetBookingsByCustomer(int id=0)
        {
            List<Booking> bookings = null;
            TravelExpertsContext db = new TravelExpertsContext();
            if (id == 0) //no filtering
            {
                bookings = db.Bookings.Include(r => r.Customer)
                    .Include(r => r.Package)
                    .Include(r => r.Package.PkgName)
                    .Include(r => r.Package.PkgBasePrice)
                    .ToList();
            }
            else //filter by Customer, calculates the subtotal , grand total will be a viewbag pointing to a separate tag in the html
            //which i will do later
            {
                bookings = db.Bookings.Where(r => r.CustomerId == id).
                    Include(r => r.Customer)
                    .Include(r => r.Package)
                    .Include(r => r.Package.PkgName)
                    .Include(r => r.Package.PkgBasePrice)
                    .Include(r=>((decimal)r.TravelerCount * r.Package.PkgBasePrice ))
                    .ToList();
            }
            return bookings;


        }


    }
}
