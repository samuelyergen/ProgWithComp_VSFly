using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreApp2021_1;
using vsFly.Models;
using vsFly.Extensions;
using Newtonsoft.Json;

namespace vsFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly WWWingsContext _context;
        

        public FlightsController(WWWingsContext context)
        {
            _context = context;
            
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListFlightModel>>> GetFlightSet()
        {

            //convert flight to flightModel
            var flightList =  await _context.FlightSet.Where(x=>x.Date>=DateTime.Now).ToListAsync();
            List<Models.ListFlightModel> listFlightM = new List<ListFlightModel>();


            foreach(Flight f in flightList)
            {
                short seats = f.Seats;
             
                var bookedSeats = _context.BookingSet.Where(b => b.FlightNo == f.FlightNo);
               
                float totalPriceForFlight = 0;

                foreach(Booking b in bookedSeats)
                {
                    totalPriceForFlight += b.PricePaid;
                }

                int count = bookedSeats.Count();

                if(count < seats)
                {
                    var fM = f.ConvertToListFlightModel(count);
                    fM.TotalTicketPrice = totalPriceForFlight;
                    listFlightM.Add(fM);
                }
   
            }
            return listFlightM;
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightModel>> GetFlight(int id)
        {
            var flight = await _context.FlightSet.FindAsync(id);
            var fM = new FlightModel();

            var count = await _context.BookingSet.Where(x => x.FlightNo == id).CountAsync();
            

            DateTime today = DateTime.Now;
            TimeSpan timeBetweenDates = flight.Date - today;

            //check if the plane is 80% full and increase price
            if ((count * 100) / flight.Seats > 80)
            {
                flight.Price = (flight.Price * 150) / 100;
            }
            else if (timeBetweenDates.TotalDays < 30 && (count * 100) / flight.Seats < 50)
            {
                flight.Price = (flight.Price * 70) / 100;
            }
            //check dates and decrease price
            else if (timeBetweenDates.TotalDays < 60 && (count * 100) / flight.Seats < 20)
            {

                flight.Price = (flight.Price * 80) / 100;

            }

            if (flight == null)
            {
                return NotFound();
            }
            else
            {
                fM = flight.ConvertToFlightModel(count);
            }

            return fM;
        }

        [HttpGet("destinations")]
        public async Task<ActionResult<List<DestinationsModel>>> GetDestinations()
        {
            var destinations = await _context.FlightSet.Select(x => x.Destination).Distinct().ToListAsync();
            var fM = new FlightModel();

            if (destinations == null)
            {
                return NoContent();
            }
            List<DestinationsModel> destinationsModels = new List<DestinationsModel>();
            foreach (var destination in destinations) {
                var averagePrice = await _context.BookingSet.Where(x => x.Flight.Destination == destination).Select(x => x.PricePaid).DefaultIfEmpty().AverageAsync();
                destinationsModels.Add(new DestinationsModel()
                {
                    Destination = destination,
                    AveragePrice = averagePrice
                });
            }

            return destinationsModels;
        }

        [HttpGet("destinations/{destination}")]
        public async Task<ActionResult<List<TicketsModel>>> GetTickets(string destination)
        {
            var flights = await _context.FlightSet.Select(x => x).Where(x => x.Destination == destination).ToListAsync();
            List<TicketsModel> tickets = new List<TicketsModel>();
            foreach (var flight in flights)
            {
                var bookings = await _context.BookingSet.Where(x => x.FlightNo == flight.FlightNo).ToListAsync();
                foreach (var booking in bookings)
                {
                    var passenger = await _context.PassengerSet.Where(x => x.PersonID == booking.PassengerID).FirstOrDefaultAsync();
                    tickets.Add(booking.ConvertToTicketsModel(flight, passenger));
                }
            }
            return tickets;
        }
    }
}
