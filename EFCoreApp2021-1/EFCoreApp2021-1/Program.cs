using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp2021_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //the two context are the same
            //compiler choose the right type with var
            var ctx = new WWWingsContext();

            //WWWingsContext ctx2 = new WWWingsContext();

           var e = ctx.Database.EnsureCreated();

            if (e)
            {
                Console.WriteLine("Database has been created");
            } else
            {
                Console.WriteLine("Database already exists");
            }

            Console.WriteLine("done .");


            //add a pilot
            Pilot pi = new Pilot { FlightHours = 8 , Surname = "Carlson", GivenName = "Carl", Salary = 4000 };
            ctx.PilotSet.Add(pi);
            ctx.SaveChanges();

            //DISPLAY ALL THE FLIGHTS
            //LINQ
            foreach (Flight flight in ctx.FlightSet)
            {   
                //parametized string, date is arguemnt 0, destination is 1 and seats 2
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2}", flight.Date, flight.Destination, flight.Seats);
            }

            Console.WriteLine("--------------------------------------------------------------------------------");

            //=> is lambda expression
            //lambda expression is based on anonymous functions
            //with contstraint
            var flights = ctx.FlightSet.Where(f => f.Departure == "GVA" && f.Seats > 100);

            foreach(Flight flight in flights)
            {
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2}", flight.Date, flight.Destination, flight.Seats);
            }

            Console.WriteLine("--------------------------------------------------------------------------------");

            //constraints with query
            var query = from f in ctx.FlightSet
                        where f.Seats > 100
                        //can use string methods (type methods in general)
                        && f.Destination.StartsWith("L")
                        select f;

            //attack the DB here
            foreach(Flight flight in query)
            {
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2}", flight.Date, flight.Destination, flight.Seats);
            }


            //a user change the database
            //data changes because not use cache


            Console.WriteLine("--------------------------------------------------------------------------------");


            foreach (Flight flight in query)
            {
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2}", flight.Date, flight.Destination, flight.Seats);
            }
            //Console.ReadKey();

            //add a flight

            Flight ft = new Flight { Date = DateTime.Now, Departure = "NNN", Destination = "VVV", Seats = 101 , PilotId = 1};
            //other way
            //Flight ftt = new Flight { Date = DateTime.Now, Departure = "BBB", Destination = "VVV", Seats = 101, Pilot = pi };

            //attach the new flight in the cache
            //nothing added in the database
            ctx.FlightSet.Add(ft);

            //change in the database here
            ctx.SaveChanges();

            //to see the id just created
            Console.WriteLine(ft.FlightNo);


            //not really false, just miss the SaveChanges()
            //Flight ff = new Flight();
            //ff.Departure = "BGK";
            //ff.Destination = "GVA";
            //ff.Date = new DateTime(2022,5,4);
            //var q = ctx.FlightSet.Add(ff);


            Console.WriteLine("--------------------------------------------------------------------------------");


            foreach (Flight flight in ctx.FlightSet)
            {
                //parametized string, date is arguemnt 0, destination is 1 and seats 2
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2}", flight.Date, flight.Destination, flight.Seats);
            }

            Console.WriteLine("--------------------------------------------------------------------------------");

            //delete flight with id > 3
            //mine
            var fls = ctx.FlightSet.Where(f => f.FlightNo > 10);

            //also possible
            //ctx.RemoveRange(fls);
            //most efficient
            //ctx.Database.ExecuteSqlRaw("DELETE FROM FlightSet WHERE FlightNo > 3");

            foreach(Flight fff in fls)
            {
                ctx.FlightSet.Remove(fff);
                
            }

            //Soluce
         /*   var queryy = from f in ctx.FlightSet
                        where f.FlightNo > 3                     
                        select f;

            foreach(Flight flight in queryy)
            {
                ctx.FlightSet.Remove(flight);
            }*/

            ctx.SaveChanges();

            Console.WriteLine("--------------------------------------------------------------------------------");

            //update
          /*  var flis = ctx.FlightSet.Where(i => i.FlightNo == 2);
            
            foreach(Flight fs in flis)
            {
                fs.Seats += 1;
                ctx.FlightSet.Update(fs);
            }*/

            //soluce
            //better than mine
            //find(2) select with the primary key 2
            Flight updatable = ctx.FlightSet.Find(2);

            updatable.Seats += 1;

            ctx.SaveChanges();

            Console.WriteLine("--------------------------------------------------------------------------------");

            //select and display all flights with pilot givename and salary
           /* foreach (Flight flight in ctx.FlightSet)
            {
      
                //parametized string, date is arguemnt 0, destination is 1 and seats 2
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2} Pilot : {3} Salary : {4}", flight.Date, flight.Destination, flight.Seats, flight.Pilot.GivenName, flight.Pilot.Salary);
            }*/

            //soluce
            //Pilot is null with these query
            var q3 = from f in ctx.FlightSet
                     select f;

            foreach(Flight flight in q3)
            {   //load the pilot
                ctx.Entry(flight).Reference(x => x.Pilot).Load();
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2} Pilot : {3} Salary : {4}", flight.Date, flight.Destination, flight.Seats, flight.Pilot.GivenName, flight.Pilot.Salary);
            }

            //other soluce with JOIN
            var q4 = from f in ctx.FlightSet.Include(x => x.Pilot)
                     //constraint here
                     select f;

            foreach (Flight flight in q4)
            {                 
                Console.WriteLine("Date : {0} Destination : {1} Seats : {2} Pilot : {3} Salary : {4}", flight.Date, flight.Destination, flight.Seats, flight.Pilot.GivenName, flight.Pilot.Salary);
            }

            Console.WriteLine("--------------------------------------------------------------------------------");

            //start from pilot instead of flight
           /* var q5 = from p in ctx.PilotSet
                     select p;*/

            var q6 = from p in ctx.PilotSet.Include(x => x.FlightAsPilotSet)
                     select p;

            foreach (Pilot pilot in q6)
            {

                //don't need because of the include (join)
               // ctx.Entry(pilot).Collection(x => x.FlightAsPilotSet).Load();

                Console.WriteLine("{0} {1} {2} {3}", pilot.GivenName, pilot.Salary, pilot.FlightHours, pilot.FlightAsPilotSet.Count());
                
                foreach(Flight flight in pilot.FlightAsPilotSet)
                {
                    Console.WriteLine(" - {0} {1} {2}", flight.Date, flight.Destination, flight.Seats);
                }
            }

            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("test many to many----------------------------------------------------");

            //add a many to many relationship (book flight)
            //add passenger and booking
            /*Passenger pass = new Passenger { GivenName = "test", Weight = 80, Surname = "test" };
             ctx.PassengerSet.Add(pass);
             Passenger passe = new Passenger { GivenName = "Scott", Weight = 70, Surname = "Carlson" };
             ctx.PassengerSet.Add(passe);
             Booking book = new Booking { Flight = ctx.FlightSet.Find(5), Passenger = ctx.PassengerSet.Find(4)};
             ctx.BookingSet.Add(book);
             ctx.SaveChanges();*/
            Console.WriteLine("flight and their passengers----------------------------------------------------");
            var q7 = from b in ctx.FlightSet.Include(x => x.BookingSet)
                     select b;

            foreach(Flight flight in q7)
            {
                Console.WriteLine("{0} {1}", flight.Date, flight.Departure);

                foreach(Booking b in flight.BookingSet)
                {
                    var q8 = from p in ctx.PassengerSet
                             where p.PersonID == b.PassengerID
                             select p;

                    foreach(Passenger p in q8)
                    {
                        Console.WriteLine(" - {0} {1}", p.Surname, p.Weight);
                    }                   
                }      
            }
            Console.WriteLine("passengers and their flights----------------------------------------------------");

            var q9 = from passen in ctx.PassengerSet.Include(x => x.BookingSet)
                     select passen;

            foreach(Passenger passenger in q9)
            {
                Console.WriteLine("{0} {1} {2}", passenger.Surname, passenger.GivenName, passenger.Weight);

                foreach (Booking b in passenger.BookingSet)
                {
                    var q10 = from boo in ctx.FlightSet
                              where boo.FlightNo == b.FlightNo
                              select boo;

                    foreach(Flight flight in q10)
                    {
                        Console.WriteLine(" - {0} {1} {2}", flight.Departure, flight.Date, flight.Destination);
                    }
                }               
            }

            Console.WriteLine("correction many to many----------------------------------------------------");
            Console.WriteLine("passengers and their flights----------------------------------------------------");

            var q11 = from passenger in ctx.PassengerSet
                      select passenger;

            foreach(Passenger passenger in q11)
            {
                Console.WriteLine("{0} {1} {2}", passenger.Surname, passenger.GivenName, passenger.BookingSet.Count());

                foreach(Booking booking in passenger.BookingSet)
                {
                    Console.WriteLine(" - {0} {1} {2}", booking.Flight.Date, booking.Flight.Departure, booking.Flight.Pilot.GivenName);
                }
            }
        }
    }
}
