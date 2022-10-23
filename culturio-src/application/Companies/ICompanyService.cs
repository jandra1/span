using Culturio.Application.Collections;
using Culturio.Application.Companies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Companies
{
    public interface ICompanyService
    {
        public Task<IPagedList<CompanyDto>> GetCompanies(GetCompanyDto getCompanyDto, CancellationToken token);
        public Task<CompanyDto> GetById(int id, CancellationToken token);
        public Task<int> CreateCompany(CreateCompanyDto createCompanyDto, CancellationToken token);
        public Task<bool> DeleteCompany(int id, CancellationToken token);
        public Task<bool> CompanyExists(int id, CancellationToken token);
        public Task UpdateCompany(UpdateCompanyDto company, CancellationToken token);
    }
}
