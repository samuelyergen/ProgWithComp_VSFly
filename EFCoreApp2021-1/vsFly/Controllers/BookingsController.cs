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

namespace vsFly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly WWWingsContext _context;

        public BookingsController(WWWingsContext context)
        {
            _context = context;
        }

        

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.BookingSet.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        
        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingViewModel bookingInfo)
        {
            BookingModel bm = new BookingModel();
            PassengerModel pm = new PassengerModel();            

            bm.FlightNo = bookingInfo.FlightNo;
            bm.PricePaid = bookingInfo.Price;

            pm.Surname = bookingInfo.Passenger.Surname;
            pm.GivenName = bookingInfo.Passenger.GivenName;
           
            if (! _context.PassengerSet.Any(e => e.Surname == pm.Surname && e.GivenName == pm.GivenName))
            {
                var passenger = pm.ConvertToPassenger();
                _context.PassengerSet.Add(passenger);
                await _context.SaveChangesAsync();
                bm.PassengerID = passenger.PersonID;
            }
            else
            {
                var pa = await _context.PassengerSet.Where(p => p.Surname == pm.Surname && p.GivenName == pm.GivenName).FirstOrDefaultAsync();
                bm.PassengerID = pa.PersonID;
            }

            if (_context.BookingSet.Any(x => x.FlightNo == bm.FlightNo && bm.PassengerID == x.PassengerID))
                return StatusCode(409, "User already have a ticket.");
            _context.BookingSet.Add(bm.ConvertToBook());

            await _context.SaveChangesAsync();
         
            return CreatedAtAction("GetBooking", new { id = bm.FlightNo }, bm);
        }
    
    }
}
