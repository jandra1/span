using Culturio.Application.Collections;
using Culturio.Application.Companies.Models;

namespace Culturio.API.Models
{
    public class GetCompaniesResponseModel
    {
        public IEnumerable<CompanyDto> Companies { get; private set; }
        public int TotalCount { get; private set; }

        public GetCompaniesResponseModel(IPagedList<CompanyDto> companyDtoPagedList)
        {
            Companies = companyDtoPagedList;
            TotalCount = companyDtoPagedList.TotalCount;
        }
    }
}
