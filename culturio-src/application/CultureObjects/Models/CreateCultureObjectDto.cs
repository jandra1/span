using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjects.Models
{
    public class CreateCultureObjectDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string WorkingHours { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CultureObjectTypeId { get; set; }
        public string Notes { get; set; }

        //provjerit ovo
        public int ResponsiblePersonId { get; set; }
        public int CultureObjectCompanyId { get; set; }
    }
}
