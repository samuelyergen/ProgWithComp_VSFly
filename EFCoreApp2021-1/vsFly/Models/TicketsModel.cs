using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vsFly.Models
{
    public class TicketsModel
    {
        public int FlightID { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
    }
}
