using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Companies.Models
{
    public class UpdateCompanyDto : CreateCompanyDto
    {
        public int Id { get; set; }
    }
}
