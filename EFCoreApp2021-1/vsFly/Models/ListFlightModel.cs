using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vsFly.Models
{
    public class ListFlightModel
    {
        public int FlightNo { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public int Seats { get; set; }

        public int NumberPassenger { get; set; }

        public float Price { get; set; }

        public DateTime Date { get; set; }

        public float TotalTicketPrice { get; set; }
    }
}
