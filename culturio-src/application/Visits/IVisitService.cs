using Culturio.Application.Collections;
using Culturio.Application.Users.Models;
using Culturio.Application.Visits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Visits
{
    public interface IVisitService
    {
        public Task<IPagedList<VisitDto>> GetVisits(GetVisitDto getVisitDto, CancellationToken token);
        public Task<IPagedList<VisitDto>> GetByUserId(GetVisitDto getVisitDto, int id, CancellationToken token);
        public Task<int> CreateVisit(CreateVisitDto createVisitDto, CancellationToken token);
    }
}
