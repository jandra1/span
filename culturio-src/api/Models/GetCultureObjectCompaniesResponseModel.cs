using Culturio.Application.Collections;
using Culturio.Application.CultureObjectCompanies.Models;

namespace Culturio.API.Models
{
    public class GetCultureObjectCompaniesResponseModel
    {
        public IEnumerable<CultureObjectCompanyDto> CultureObjectCompanies { get; private set; }
        public int TotalCount { get; private set; }

        public GetCultureObjectCompaniesResponseModel(IPagedList<CultureObjectCompanyDto> cultureObjectCompanyDtoPagedList)
        {
            CultureObjectCompanies = cultureObjectCompanyDtoPagedList;
            TotalCount = cultureObjectCompanyDtoPagedList.TotalCount;
        }
    }
}
