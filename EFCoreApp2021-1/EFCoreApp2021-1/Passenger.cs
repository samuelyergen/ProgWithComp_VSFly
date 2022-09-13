using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreApp2021_1
{
    public class Passenger:Person
    {
        public int Weight { get; set; }

        public virtual ICollection<Booking> BookingSet { get; set; }

    }
}
