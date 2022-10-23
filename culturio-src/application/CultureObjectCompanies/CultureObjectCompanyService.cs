using Culturio.Application.Collections;
using Culturio.Application.Companies.Models;
using Culturio.Application.CultureObjectCompanies.Models;
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

namespace Culturio.Application.CultureObjectCompanies
{
    public class CultureObjectCompanyService : ICultureObjectCompanyService
    {

        private readonly CulturioDbContext _context;
        private readonly ILogger<CultureObjectCompanyService> _logger;

        public CultureObjectCompanyService(CulturioDbContext context, ILogger<CultureObjectCompanyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> CreateCultureObjectCompany(CreateCultureObjectCompanyDto cultureObjectCompany, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(cultureObjectCompany, nameof(cultureObjectCompany));

            CultureObjectCompany cultureObjectCompanyToCreate = new()
            {
                //Id = user.Id,
                Name = cultureObjectCompany.Name,
                TaxId = (int)cultureObjectCompany.TaxId,
                VatId = (int)cultureObjectCompany.VatId,
                Phone = cultureObjectCompany.Phone,
                Address = cultureObjectCompany.Address,
                PostalCode = (int)cultureObjectCompany.PostalCode,
                City = cultureObjectCompany.City,
                State = cultureObjectCompany.State,
                CorrespondencePersonId = cultureObjectCompany.CorrespondencePersonId,
                CompanyLogo = cultureObjectCompany.CompanyLogo,
                IBAN = cultureObjectCompany.IBAN
            };

            _context.CultureObjectCompanies.Add(cultureObjectCompanyToCreate);
            await _context.SaveChangesAsync(token);

            return cultureObjectCompanyToCreate.Id;
        }

        public async Task<bool> CultureObjectCompanyExists(int id, CancellationToken token)
        {
            return await _context.CultureObjectCompanies.Where(cultureObjectCompany => cultureObjectCompany.Id == id).AnyAsync(token);
        }

        public async Task<bool> DeleteCultureObjectCompany(int id, CancellationToken token)
        {
            CultureObjectCompany cultureObjectCompanyToDelete = await _context.CultureObjectCompanies.Where(cultureObjectCompany => cultureObjectCompany.Id == id)
                .FirstOrDefaultAsync(token);
            if (cultureObjectCompanyToDelete is null)
                return false;
            _context.Remove(cultureObjectCompanyToDelete);
            await _context.SaveChangesAsync(token);

            return true;
        }

        public async Task<CultureObjectCompanyDto> GetById(int id, CancellationToken token)
        {
            return await _context.CultureObjectCompanies.Where(cultureObjectCompany => cultureObjectCompany.Id == id)
                .Select(cultureObjectCompany => new CultureObjectCompanyDto
                {
                    Id = cultureObjectCompany.Id,
                    Name = cultureObjectCompany.Name,
                    TaxId = cultureObjectCompany.TaxId,
                    VatId = cultureObjectCompany.VatId,
                    Phone = cultureObjectCompany.Phone,
                    PostalCode = cultureObjectCompany.PostalCode,
                    City = cultureObjectCompany.City,
                    State = cultureObjectCompany.State,
                    CorrespondencePerson = cultureObjectCompany.CorrespondencePerson.FirstName + " " + cultureObjectCompany.CorrespondencePerson.LastName,
                    Address = cultureObjectCompany.Address,
                    CompanyLogo = cultureObjectCompany.CompanyLogo,
                    IBAN = cultureObjectCompany.IBAN,
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task<IPagedList<CultureObjectCompanyDto>> GetCultureObjectCompanies(GetCultureObjectCompanyDto getCultureObjectCompanyDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(getCultureObjectCompanyDto, nameof(getCultureObjectCompanyDto));
            IQueryable<CultureObjectCompany> cultureObjectCompanyQuery = _context.CultureObjectCompanies.AsNoTracking();
            cultureObjectCompanyQuery = ApplyFilters(getCultureObjectCompanyDto, cultureObjectCompanyQuery);

            int totalCount = await cultureObjectCompanyQuery.CountAsync(token);
            cultureObjectCompanyQuery = ApplySortingAndPaging(getCultureObjectCompanyDto, cultureObjectCompanyQuery);

            var cultureObjectCompanies = await cultureObjectCompanyQuery
                .Select(cultureObjectCompany => new CultureObjectCompanyDto
                {
                    Id = cultureObjectCompany.Id,
                    Name = cultureObjectCompany.Name,
                    TaxId = (int)cultureObjectCompany.TaxId,
                    VatId = (int)cultureObjectCompany.VatId,
                    Phone = cultureObjectCompany.Phone,
                    PostalCode = (int)cultureObjectCompany.PostalCode,
                    City = cultureObjectCompany.City,
                    State = cultureObjectCompany.State,
                    CorrespondencePerson = cultureObjectCompany.CorrespondencePerson.FirstName + " " + cultureObjectCompany.CorrespondencePerson.LastName,
                    Address = cultureObjectCompany.Address,
                    CompanyLogo = cultureObjectCompany.CompanyLogo,
                    IBAN = cultureObjectCompany.IBAN,


        })
                .ToListAsync(cancellationToken: token);

            return new PagedList<CultureObjectCompanyDto>(cultureObjectCompanies, getCultureObjectCompanyDto.Page ?? 1, getCultureObjectCompanyDto.PageSize ?? int.MaxValue, totalCount);
        }

        private IQueryable<CultureObjectCompany> ApplySortingAndPaging(GetCultureObjectCompanyDto getCultureObjectCompanyDto, IQueryable<CultureObjectCompany> cultureObjectCompanyQuery)
        {
            cultureObjectCompanyQuery = getCultureObjectCompanyDto.SortOrder switch
            {
                "asc" => cultureObjectCompanyQuery.OrderBy(x => x.Name),
                "desc" => cultureObjectCompanyQuery.OrderByDescending(x => x.Name),
                _ => cultureObjectCompanyQuery.OrderByDescending(x => x.Id)
            };

            if (getCultureObjectCompanyDto.Page.HasValue && getCultureObjectCompanyDto.PageSize.HasValue)
            {
                cultureObjectCompanyQuery = cultureObjectCompanyQuery.Skip((getCultureObjectCompanyDto.Page.Value - 1) * getCultureObjectCompanyDto.PageSize.Value)
                    .Take(getCultureObjectCompanyDto.PageSize.Value);
            }

            return cultureObjectCompanyQuery;
        }

        private IQueryable<CultureObjectCompany> ApplyFilters(GetCultureObjectCompanyDto getCultureObjectCompanyDto, IQueryable<CultureObjectCompany> cultureObjectCompanyQuery)
        {
            if (getCultureObjectCompanyDto.CorrespondencePersonId.HasValue)
            {
                cultureObjectCompanyQuery = cultureObjectCompanyQuery.Where(cultureObjectCompany => cultureObjectCompany.CorrespondencePersonId == getCultureObjectCompanyDto.CorrespondencePersonId.Value);
            }

            if (!string.IsNullOrWhiteSpace(getCultureObjectCompanyDto.SearchValue))
            {
                cultureObjectCompanyQuery = cultureObjectCompanyQuery.Where(cultureObjectCompany => cultureObjectCompany.Name.StartsWith(getCultureObjectCompanyDto.SearchValue) || cultureObjectCompany.City.StartsWith(getCultureObjectCompanyDto.SearchValue));
            }

            return cultureObjectCompanyQuery;
        }

        public async Task UpdateCultureObjectCompany(UpdateCultureObjectCompanyDto cultureObjectCompany, CancellationToken token)
        {
            CultureObjectCompany cultureObjectCompanyToUpdate = await _context.CultureObjectCompanies.FindAsync(new object[] { cultureObjectCompany.Id }, cancellationToken: token);

            cultureObjectCompanyToUpdate.Name = cultureObjectCompany.Name;
            cultureObjectCompanyToUpdate.TaxId = (int)cultureObjectCompany.TaxId;
            cultureObjectCompanyToUpdate.VatId = (int)cultureObjectCompany.VatId;
            cultureObjectCompanyToUpdate.Phone = cultureObjectCompany.Phone;
            cultureObjectCompanyToUpdate.Address = cultureObjectCompany.Address;
            cultureObjectCompanyToUpdate.PostalCode = (int)cultureObjectCompany.PostalCode;
            cultureObjectCompanyToUpdate.City = cultureObjectCompany.City;
            cultureObjectCompanyToUpdate.State = cultureObjectCompany.State;
            cultureObjectCompanyToUpdate.CorrespondencePersonId = cultureObjectCompany.CorrespondencePersonId;
            cultureObjectCompanyToUpdate.CompanyLogo = cultureObjectCompany.CompanyLogo;
            cultureObjectCompanyToUpdate.IBAN = cultureObjectCompany.IBAN;

            await _context.SaveChangesAsync(token);
        }
    }
}
