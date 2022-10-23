using System;
namespace Culturio.Application.Visits.Models
{
    public class CreateVisitDto
    {
        public int UserId { get; set; }
        public int CultureObjectId { get; set; }
        public DateTime TimeOfVisit { get; set; }
    }
}

