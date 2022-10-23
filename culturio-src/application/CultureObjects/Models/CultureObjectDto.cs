using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjects.Models
{
    public class CultureObjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string WorkingHours { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CultureObjectType { get; set; }
        public string Notes { get; set; }

        //provjerit ovo
        public string ResponsiblePerson { get; set; }
        public string CultureObjectCompany { get; set; }

    }
}
