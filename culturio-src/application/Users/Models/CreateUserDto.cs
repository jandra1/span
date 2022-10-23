using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Users.Models
{
    public class CreateUserDto
    {
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? PostalCode { get; set; }
        public string? City { get; set; }
        public int? SexId { get; set; }
        public string? DefaultLanguage { get; set; }
        public bool? TermsAccepted { get; set; }
        public string? QRcode { get; set; }
        //public DateTime DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public int? CompanyId { get; set; }
        public int? CultureObjectId { get; set; }
    }
}
