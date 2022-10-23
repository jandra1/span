using Culturio.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Culturio.Domain
{
    public class Visit: BaseEntity
    {
        public int UserId { get; set; }
        public int CultureObjectId { get; set; }
        public DateTime TimeOfVisit { get; set; }
    }
}

