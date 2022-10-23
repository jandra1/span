using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Users.Models
{
    public class UserAuthDto
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Role { get; set; }
    }
}
