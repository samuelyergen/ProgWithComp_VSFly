using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIClient.Models
{
    public class BookingViewModel
    {
            [Required]
            public int FlightNo { get; set; }
            [Required]
            public float Price { get; set; }
            [Required]
            public PassengerModel Passenger { get; set; }
    }

}
