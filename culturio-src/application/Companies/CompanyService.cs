using Culturio.Application.Collections;
using Culturio.Application.Companies.Models;
using Culturio.Application.Persistence;

using Culturio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Culturio.Application.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly CulturioDbContext _context;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(CulturioDbContext context, ILogger<CompanyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CompanyExists(int id, CancellationToken token)
        {
            return await _context.Companies.Where(company => company.Id == id).AnyAsync(token);
        }

        public async Task<int> CreateCompany(CreateCompanyDto company, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(company, nameof(company));

            Company companyToCreate = new()
            {
                Name = company.Name,
                TaxId = company.TaxId,
                VatId = company.VatId,
                Phone = company.Phone,
                Address = company.Address,
                PostalCode = company.PostalCode,
                City = company.City,
                State = company.State,
                CorrespondencePersonId = company.CorrespondencePersonId
    };

            _context.Companies.Add(companyToCreate);
            await _context.SaveChangesAsync(token);

            return companyToCreate.Id;
        }

        public async Task<bool> DeleteCompany(int id, CancellationToken token)
        {
            Company companyToDelete = await _context.Companies.Where(company => company.Id == id)
                .FirstOrDefaultAsync(token);
            if (companyToDelete is null)
                return false;
            
            _context.Companies.Remove(companyToDelete);
            await _context.SaveChangesAsync(token);

            return true;
        }

        public async Task<CompanyDto> GetById(int id, CancellationToken token)
        {
            return await _context.Companies.Where(company => company.Id == id)
                .Select(company => new CompanyDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    TaxId = company.TaxId,
                    VatId = company.VatId,
                    Phone = company.Phone,
                    Address = company.Address,
                    PostalCode = company.PostalCode,
                    City = company.City,
                    State = company.State,                    
                    CorrespondencePerson = company.CorrespondencePerson.FirstName + " " + company.CorrespondencePerson.LastName,
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task<IPagedList<CompanyDto>> GetCompanies(GetCompanyDto getCompanyDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(getCompanyDto, nameof(getCompanyDto));
            IQueryable<Company> companyQuery = _context.Companies.AsNoTracking();
            companyQuery = ApplyFilters(getCompanyDto, companyQuery);

            int totalCount = await companyQuery.CountAsync(token);
            companyQuery = ApplySortingAndPaging(getCompanyDto, companyQuery);

            var companies = await companyQuery
                .Select(company => new CompanyDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    TaxId = company.TaxId,
                    VatId = company.VatId,
                    Phone = company.Phone,
                    Address = company.Address,
                    PostalCode = company.PostalCode,
                    City = company.City,
                    State = company.State,
                    CorrespondencePerson = company.CorrespondencePerson.FirstName + " " + company.CorrespondencePerson.LastName,
                })
                .ToListAsync(cancellationToken: token);

            return new PagedList<CompanyDto>(companies, getCompanyDto.Page ?? 1, getCompanyDto.PageSize ?? int.MaxValue, totalCount);
        }

        private IQueryable<Company> ApplySortingAndPaging(GetCompanyDto getCompanyDto, IQueryable<Company> companyQuery)
        {
            companyQuery = getCompanyDto.SortOrder switch
            {
                "asc" => companyQuery.OrderBy(x => x.Name),
                "desc" => companyQuery.OrderByDescending(x => x.Name),
                _ => companyQuery.OrderByDescending(x => x.Id)
            };

            if (getCompanyDto.Page.HasValue && getCompanyDto.PageSize.HasValue)
            {
                companyQuery = companyQuery.Skip((getCompanyDto.Page.Value - 1) * getCompanyDto.PageSize.Value)
                    .Take(getCompanyDto.PageSize.Value);
            }

            return companyQuery;
        }

        private static IQueryable<Company> ApplyFilters(GetCompanyDto getCompanyDto, IQueryable<Company> companyQuery)
        {

            if (getCompanyDto.CorrespondencePersonId.HasValue)
            {
                companyQuery = companyQuery.Where(company => company.CorrespondencePersonId == getCompanyDto.CorrespondencePersonId.Value);
            }

            if (!string.IsNullOrWhiteSpace(getCompanyDto.SearchValue))
            {
                companyQuery = companyQuery.Where(company => company.Name.StartsWith(getCompanyDto.SearchValue) || company.City.StartsWith(getCompanyDto.SearchValue));
            }

            return companyQuery;
        }

        public async Task UpdateCompany(UpdateCompanyDto company, CancellationToken token)
        {
            Company companyToUpdate = await _context.Companies.FindAsync(new object[] { company.Id }, cancellationToken: token);

            companyToUpdate.Name = company.Name;
            companyToUpdate.TaxId = company.TaxId;
            companyToUpdate.VatId = company.VatId;
            companyToUpdate.Phone = company.Phone;
            companyToUpdate.Address = company.Address;
            companyToUpdate.PostalCode =company.PostalCode;
            companyToUpdate.City = company.City;
            companyToUpdate.State = company.State;
            companyToUpdate.CorrespondencePersonId = company.CorrespondencePersonId;

            await _context.SaveChangesAsync(token);
        }
    }
}
