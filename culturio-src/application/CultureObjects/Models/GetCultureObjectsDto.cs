using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjects.Models
{
    public class GetCultureObjectsDto
    {
        public string? SortOrder { get; set; }
        public string? SearchValue { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? ResponsiblePersonId { get; set; }
        public int? CultureObjectCompanyId { get; set; }

        //provjerit ovo
    }
}
