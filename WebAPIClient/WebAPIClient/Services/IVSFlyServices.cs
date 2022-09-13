using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPIClient.Models;

namespace WebAPIClient.Services
{
    public interface IVSFlyServices
    {   //Task means asynchronous
        public Task<IEnumerable<FlightModel>> GetFlights();

        public Task<FlightModel> GetFlight(int id);
        public Task<IEnumerable<DestinationViewModel>> GetDestinations();
        public Task<IEnumerable<TicketsModel>> GetTickets(string destination);
        public Task<HttpResponseMessage> AddTicket(BookingViewModel booking);
    }
}
