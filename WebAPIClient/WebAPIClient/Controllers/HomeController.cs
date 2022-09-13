using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIClient.Models;
using WebAPIClient.Services;

namespace WebAPIClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVSFlyServices _vsFly;

        public HomeController(ILogger<HomeController> logger, IVSFlyServices vsFly)
        {
            _logger = logger;
            _vsFly = vsFly;
        }

        public async Task<IActionResult> Index()
        {
           var listofFlights = await _vsFly.GetFlights();
            return View(listofFlights);
        }
        public async Task<IActionResult> Details(int id) {
            var flight = await _vsFly.GetFlight(id);
            FlightDetailsModel flightDetails = new FlightDetailsModel
            {
 
                FlightNo = flight.FlightNo,
                Date = flight.Date,
                Departure = flight.Departure,
                Destination = flight.Destination,
                Price = flight.Price,
                Seats = flight.Seats,
                NumberPassenger = flight.NumberPassenger,
                Form = new BookingViewModel
                {
                    FlightNo = flight.FlightNo,
                    Price = flight.Price
                }
            };
            return View(flightDetails);
        }
        public async Task<IActionResult> BuyTicket(FlightDetailsModel flightDetails) {
            var result = await _vsFly.AddTicket(flightDetails.Form);
            if (result.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "The user has successfuly bought a ticket.";
                return RedirectToAction("Index");
            }
            else
            {
                if (result.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["WarningMessage"] = "User has already a ticket.";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Content;
                }
                return RedirectToAction("Details", new { id = flightDetails.Form.FlightNo});
            }
        }

        public async Task<IActionResult> Destinations(string destination)
        {
            DestinationsViewModel destinationsModel = new DestinationsViewModel();

            destinationsModel.Destinations = await _vsFly.GetDestinations();
            if (destination != null)
            {
                destinationsModel.DestinationSelected = destination;
                destinationsModel.Tickets = await _vsFly.GetTickets(destination);
            }
            return View(destinationsModel);
        }
  
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
