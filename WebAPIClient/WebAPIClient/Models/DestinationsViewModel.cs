using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIClient.Models
{
    public class DestinationsViewModel
    {
        public string DestinationSelected { get; set; }
        public IEnumerable<DestinationViewModel> Destinations { get; set; }
        public IEnumerable<TicketsModel> Tickets { get; set; }
    }
}
