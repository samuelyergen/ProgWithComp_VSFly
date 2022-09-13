using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPIClient.Models;

namespace WebAPIClient.Services
{
    public class VSFlyServices : IVSFlyServices
    {

        private readonly HttpClient _client;
        private readonly string _baseURI;

        public VSFlyServices(HttpClient client)
        {
            _client = client;
            _baseURI = "https://localhost:44374/api/";
        }

        public async Task<IEnumerable<FlightModel>> GetFlights()
        {
            var uri = _baseURI + "Flights";

            //GetStringAsync ==> direct convert into string
            var responseString = await _client.GetStringAsync(uri); 

            var flightsList = JsonConvert.DeserializeObject<IEnumerable<FlightModel>>(responseString);

            return flightsList;
        }
        public async Task<FlightModel> GetFlight(int id)
        {
            var uri = _baseURI + "Flights/"+ id;

            //GetStringAsync ==> direct convert into string
            var responseString = await _client.GetStringAsync(uri);

            var flight = JsonConvert.DeserializeObject<FlightModel>(responseString);
            
            return flight;
        }

        public async Task<IEnumerable<DestinationViewModel>> GetDestinations()
        {
            var uri = _baseURI + "Flights/Destinations/";

            //GetStringAsync ==> direct convert into string
            var responseString = await _client.GetStringAsync(uri);

            var destinations = JsonConvert.DeserializeObject<IEnumerable<DestinationViewModel>>(responseString);

            return destinations;
        }
        public async Task<IEnumerable<TicketsModel>> GetTickets(string destination)
        {
            var uri = _baseURI + "Flights/Destinations/" + destination+"/";

            //GetStringAsync ==> direct convert into string
            var responseString = await _client.GetStringAsync(uri);

            var tickets = JsonConvert.DeserializeObject<IEnumerable<TicketsModel>>(responseString);

            return tickets;
        }
        public async Task<HttpResponseMessage> AddTicket(BookingViewModel booking) {
            var uri = _baseURI + "Bookings/";

            //GetStringAsync ==> direct convert into string
            var json = JsonConvert.SerializeObject(booking);
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, stringContent);
            

            return response;


        }


    }
}
