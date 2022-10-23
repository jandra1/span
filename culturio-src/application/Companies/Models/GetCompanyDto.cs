using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Companies.Models
{
    public class GetCompanyDto
    {
        public bool GetOnlyActive { get; set; } = true;
        public string? SortOrder { get; set; }
        public string? SearchValue { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? CorrespondencePersonId { get; set; }
    }
}
