using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vsFly.Extensions
{
    public static class ConverterExtensions
    {

        public static Models.FlightModel ConvertToFlightModel (this EFCoreApp2021_1.Flight f, int numberPassenger)
        {
            Models.FlightModel fM = new Models.FlightModel();

            fM.FlightNo = f.FlightNo;
            fM.Date = f.Date;
            fM.Departure = f.Departure;
            fM.Destination = f.Destination;
            fM.Price = f.Price;
            fM.Seats = f.Seats;
            fM.NumberPassenger = numberPassenger;

            return fM;
        }

        public static Models.ListFlightModel ConvertToListFlightModel (this EFCoreApp2021_1.Flight f, int numberPassenger)
        {
            Models.ListFlightModel lFM = new Models.ListFlightModel();

            lFM.FlightNo = f.FlightNo;
            lFM.Date = f.Date;
            lFM.Departure = f.Departure;
            lFM.Destination = f.Destination;
            lFM.Price = f.Price;
            lFM.Seats = f.Seats;
            lFM.NumberPassenger = numberPassenger;

            return lFM;
        }

        public static EFCoreApp2021_1.Booking ConvertToBooking(this Models.BookingViewModel bvm)
        {
            EFCoreApp2021_1.Booking booking = new EFCoreApp2021_1.Booking();
            booking.FlightNo = bvm.FlightNo;
            booking.PassengerID = bvm.Passenger.PersonID;

            return booking;

        }

        public static EFCoreApp2021_1.Booking ConvertToBook(this Models.BookingModel bm)
        {
            EFCoreApp2021_1.Booking booking = new EFCoreApp2021_1.Booking();
            booking.FlightNo = bm.FlightNo;
            booking.PassengerID = bm.PassengerID;
            booking.PricePaid = bm.PricePaid;

            return booking;

        }

        public static EFCoreApp2021_1.Flight ConvertToFlight(this Models.FlightModel fm)
        {
            EFCoreApp2021_1.Flight f = new EFCoreApp2021_1.Flight();

            f.FlightNo = fm.FlightNo;
            f.Departure = fm.Departure;
            f.Destination = fm.Destination;
            f.Date = fm.Date;

            return f;
        }

        public static Models.PassengerModel ConvertToPassengerModel(this EFCoreApp2021_1.Passenger p)
        {
            Models.PassengerModel pm = new Models.PassengerModel();

            pm.PersonID = p.PersonID;
            pm.Surname = p.Surname;
            pm.GivenName = p.GivenName;

            return pm;
        }

        public static EFCoreApp2021_1.Passenger ConvertToPassenger(this Models.PassengerModel pm)
        {
            EFCoreApp2021_1.Passenger p = new EFCoreApp2021_1.Passenger();

            p.PersonID = pm.PersonID;
            p.Surname = pm.Surname;
            p.GivenName = pm.GivenName;

            return p;
        }
        public static Models.TicketsModel ConvertToTicketsModel(this EFCoreApp2021_1.Booking b, EFCoreApp2021_1.Flight f, EFCoreApp2021_1.Passenger p)
        {
            Models.TicketsModel tm = new Models.TicketsModel();

            tm.FlightID = b.FlightNo;
            tm.Price = b.PricePaid;

            tm.Destination = f.Destination;

            tm.GivenName = p.GivenName;
            tm.Surname = p.Surname;

            return tm;
        }

    }
}
