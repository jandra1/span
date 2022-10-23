using Culturio.Application.Collections;
using Culturio.Application.CultureObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Culturio.API.Models
{
    public class GetCultureObjectsResponseModel
    {
        public IEnumerable<CultureObjectDto> CultureObjects { get; private set; }
        public int TotalCount { get; private set; }

        public GetCultureObjectsResponseModel(IPagedList<CultureObjectDto> cultureObjectDtoPagedList)
        {
            CultureObjects = cultureObjectDtoPagedList;
            TotalCount = cultureObjectDtoPagedList.TotalCount;
        }
    }
}
