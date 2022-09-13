using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreApp2021_1
{
    public class Flight
    {
        public Flight() { }

        [Key]
        public int FlightNo { get; set; }

        //restrictions for the database
        [StringLength(50), MinLength(3)]
        public string Departure { get; set; }

        [StringLength(50), MinLength(3)]
        public string Destination { get; set; }

        public DateTime Date { get; set; }
       
        [Required]
        public float Price { get; set; }
        
        // ? give the ability to put a null
        [Required]
        public short Seats { get; set; }

        [ForeignKey("PilotId")]
        public virtual Pilot Pilot { get; set; } //main relationship
        public int PilotId { get; set; }

        public virtual ICollection<Booking> BookingSet { get; set; }
    }
}
