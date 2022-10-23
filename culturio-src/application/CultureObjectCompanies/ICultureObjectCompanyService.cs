using Culturio.Application.Collections;
using Culturio.Application.CultureObjectCompanies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjectCompanies
{
    public interface ICultureObjectCompanyService
    {
        public Task<IPagedList<CultureObjectCompanyDto>> GetCultureObjectCompanies(GetCultureObjectCompanyDto getCultureObjectCompanyDto, CancellationToken token);
        public Task<CultureObjectCompanyDto> GetById(int id, CancellationToken token);
        public Task<int> CreateCultureObjectCompany(CreateCultureObjectCompanyDto createCultureObjectCompanyDto, CancellationToken token);
        public Task<bool> DeleteCultureObjectCompany(int id, CancellationToken token);
        public Task<bool> CultureObjectCompanyExists(int id, CancellationToken token);
        public Task UpdateCultureObjectCompany(UpdateCultureObjectCompanyDto company, CancellationToken token);
    }
}
