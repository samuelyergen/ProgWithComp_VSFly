using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vsFly.Models
{
    public class BookingModel
    {

        public int FlightNo { get; set; } //declare in context
        public int PassengerID { get; set; }
        public float PricePaid { get; set; }
    }
}
