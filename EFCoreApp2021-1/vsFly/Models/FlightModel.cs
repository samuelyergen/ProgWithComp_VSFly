using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vsFly.Models
{
    public class FlightModel
    {
        //from client perspective it is enough information
        //to break the cycle
        public int FlightNo { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; } 

        public int NumberPassenger { get; set; }

        public int Seats { get; set; }

        public float Price { get; set; }

        public DateTime Date { get; set; }
    }
}
