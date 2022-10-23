using Culturio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Users.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Sex { get; set; }        
        public string? DefaultLanguage { get; set; }
        public bool? TermsAccepted { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public string? Company { get; set; }
        public string? CultureObject { get; set; }
        public string? QRcode { get; set; }
    }
}
