using EFCoreApp2021_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vsFly.Models
{
    public class BookingViewModel
    {

        public int FlightNo { get; set; }

        public float Price { get; set; }

        public PassengerModel Passenger { get; set; }
    }
}
