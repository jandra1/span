using Culturio.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Domain
{
    public class CultureObject : BaseEntity
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
        public virtual CultureObjectType CultureObjectType { get; set; }
        public string Notes { get; set; }
        
        //Responsible person
        public int ResponsiblePersonId { get; set; }
        public virtual User ResponsiblePerson { get; set; }

        public int CultureObjectCompanyId{ get; set; }
        public virtual CultureObjectCompany CultureObjectCompany { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public CultureObject()
        {
            Users = new HashSet<User>();
        }
    }
}
