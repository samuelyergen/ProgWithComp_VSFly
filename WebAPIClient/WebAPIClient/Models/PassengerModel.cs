using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIClient.Models
{
    public class PassengerModel
    {
        public int PersonId { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
    }
}
