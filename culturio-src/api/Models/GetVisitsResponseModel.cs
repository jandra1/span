using System;
using Culturio.Application.Collections;
using Culturio.Application.Users.Models;
using Culturio.Application.Visits.Models;

namespace Culturio.API.Models
{
    public class GetVisitsResponseModel
    {
        public int TotalCount { get; private set; }
        public IEnumerable<VisitDto> Visits { get; private set; }
        
        public GetVisitsResponseModel(IPagedList<VisitDto> visitDtoPagedList)
        {
            Visits = visitDtoPagedList;
            TotalCount = visitDtoPagedList.TotalCount;
        }
    
    }
}

