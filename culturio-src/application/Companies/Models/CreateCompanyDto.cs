using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Companies.Models
{
    public class CreateCompanyDto
    {
        public string Name { get; set; }
        public int TaxId { get; set; }
        public int VatId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int CorrespondencePersonId { get; set; }
    }
}
