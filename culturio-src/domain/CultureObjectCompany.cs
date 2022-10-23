using Culturio.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Domain
{
    public class CultureObjectCompany : BaseEntity
    {
        public string Name { get; set; }
        public int TaxId { get; set; }
        public int VatId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }


        //Correspondence person
        public int CorrespondencePersonId { get; set; }
        public virtual User CorrespondencePerson { get; set; }

        public string CompanyLogo { get; set; }
        public string IBAN { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<CultureObject> CultureObjects { get; set; }

        public CultureObjectCompany()
        {
            Users = new HashSet<User>();
            CultureObjects = new HashSet<CultureObject>();
        }
    }
}
