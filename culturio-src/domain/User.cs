using Culturio.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Company user:
 * 1. End-user
 * 5. Company admin
 * 6. Platform admin
 */

namespace Culturio.Domain
{
    public class User : BaseUserEntity
    {
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? PostalCode { get; set; }
        public string? City { get; set; }
        public int? SexId { get; set; }
        public virtual Sex Sex { get; set; }
        public string? DefaultLanguage { get; set; }
        public bool? TermsAccepted { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public string? QRcode { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int? CultureObjectId { get; set; }
        public virtual CultureObject CultureObject { get; set; }
    }
}
