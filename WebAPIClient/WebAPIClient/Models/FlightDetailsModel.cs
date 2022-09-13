using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebAPIClient.Models
{
    public class FlightDetailsModel
    {
        public int FlightNo { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }
        public int Seats { get; set; }
        public float Price { get; set; }
        public int NumberPassenger { get; set; }
        public DateTime Date { get; set; }
        public BookingViewModel Form { get; set; }
    }
}
