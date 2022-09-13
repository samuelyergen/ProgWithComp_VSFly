using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreApp2021_1
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }


    }
}
