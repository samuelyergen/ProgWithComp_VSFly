using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreApp2021_1
{
    public class Booking
    {
        public int FlightNo { get; set; } //declare in context
        public int PassengerID { get; set; }
        public float PricePaid { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Passenger Passenger { get; set; }


    }
}
