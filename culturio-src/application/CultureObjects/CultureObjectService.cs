using Culturio.Application.CultureObjects.Models;
using Culturio.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Culturio.Domain;
using Culturio.Application.Collections;

namespace Culturio.Application.CultureObjects
{
    public class CultureObjectService : ICultureObjectService
    {
        private readonly CulturioDbContext _dbContext;
        public CultureObjectService(CulturioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateCultureObject(CreateCultureObjectDto cultureObject, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(cultureObject, nameof(cultureObject));

            var user = await _dbContext.Users.FindAsync(cultureObject.ResponsiblePersonId);

            CultureObject cultureObjectToCreate = new()
            {
                Name = cultureObject.Name,
                Phone = cultureObject.Phone,
                Address = cultureObject.Address,
                PostalCode = cultureObject.PostalCode,
                City = cultureObject.City,
                State = cultureObject.State,
                WorkingHours = cultureObject.WorkingHours,
                Latitude = cultureObject.Latitude,
                Longitude = cultureObject.Longitude,
                CultureObjectTypeId = cultureObject.CultureObjectTypeId,
                Notes = cultureObject.Notes,
                ResponsiblePersonId=cultureObject.ResponsiblePersonId,
                CultureObjectCompanyId=cultureObject.CultureObjectCompanyId
            };
            user!.CultureObject = cultureObjectToCreate;
            
            _dbContext.Add(cultureObjectToCreate);

            await _dbContext.SaveChangesAsync(token);

            return cultureObjectToCreate.Id;
        }

        public async Task<bool> CultureObjectExists(int id, CancellationToken token)
        {
            return await _dbContext.CultureObjects.Where(cultureObject => cultureObject.Id == id).AnyAsync(token);
        }

        public async Task<bool> DeleteCultureObject(int id, CancellationToken token)
        {
            CultureObject cultureObjectToDelete = await _dbContext.CultureObjects.Where(cultureObject => cultureObject.Id == id).FirstOrDefaultAsync(token);
            if (cultureObjectToDelete is null)
                return false;

            _dbContext.Remove(cultureObjectToDelete);
            await _dbContext.SaveChangesAsync(token);
            return true;
        }

        public async Task<CultureObjectDto> GetById(int id, CancellationToken token)
        {
            return await _dbContext.CultureObjects.Where(x => x.Id == id)
                .Select(x => new CultureObjectDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Address = x.Address,
                    PostalCode = x.PostalCode,
                    City = x.City,
                    State = x.State,
                    WorkingHours = x.WorkingHours,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    CultureObjectType = x.CultureObjectType.Name,
                    Notes = x.Notes,
                    ResponsiblePerson = x.ResponsiblePerson.FirstName+" "+x.ResponsiblePerson.LastName,
                    CultureObjectCompany=x.CultureObjectCompany.Name
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task<IPagedList<CultureObjectDto>> GetCultureObjects(GetCultureObjectsDto getCultureObjectsDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(getCultureObjectsDto, nameof(getCultureObjectsDto));

            IQueryable<CultureObject> cultureObjectsQuery = _dbContext.CultureObjects.AsNoTracking();

            cultureObjectsQuery = ApplyFilters(getCultureObjectsDto, cultureObjectsQuery);

            int totalCount = await cultureObjectsQuery.CountAsync(token);
            cultureObjectsQuery = ApplySortingAndPaging(getCultureObjectsDto, cultureObjectsQuery);

            var cultureObjects = await cultureObjectsQuery
                .Select(cultureObject => new CultureObjectDto
                {
                    Id = cultureObject.Id,
                    Name = cultureObject.Name,
                    Phone = cultureObject.Phone,
                    Address = cultureObject.Address,
                    PostalCode = cultureObject.PostalCode,
                    City = cultureObject.City,
                    State = cultureObject.State,
                    WorkingHours = cultureObject.WorkingHours,
                    Longitude = cultureObject.Longitude,
                    Latitude = cultureObject.Latitude,
                    CultureObjectType = cultureObject.CultureObjectType.Name,
                    Notes = cultureObject.Notes,
                    ResponsiblePerson = cultureObject.ResponsiblePerson.FirstName + " " + cultureObject.ResponsiblePerson.LastName,
                    CultureObjectCompany=cultureObject.CultureObjectCompany.Name
                })
                .ToListAsync(cancellationToken: token);

            return new PagedList<CultureObjectDto>(cultureObjects, getCultureObjectsDto.Page ?? 1, getCultureObjectsDto.PageSize ?? int.MaxValue, totalCount);
        }

        private static IQueryable<CultureObject> ApplySortingAndPaging(GetCultureObjectsDto getCultureObjectsDto, IQueryable<CultureObject> cultureObjectsQuery)
        {
            cultureObjectsQuery = getCultureObjectsDto.SortOrder switch
            {
                "asc" => cultureObjectsQuery.OrderBy(x => x.Name),
                "desc" => cultureObjectsQuery.OrderByDescending(x => x.Name),
                _ => cultureObjectsQuery.OrderByDescending(x => x.Id)
            };

            if (getCultureObjectsDto.Page.HasValue && getCultureObjectsDto.PageSize.HasValue)
            {
                cultureObjectsQuery = cultureObjectsQuery.Skip((getCultureObjectsDto.Page.Value - 1) * getCultureObjectsDto.PageSize.Value)
                    .Take(getCultureObjectsDto.PageSize.Value);
            }

            return cultureObjectsQuery;
        }

        private static IQueryable<CultureObject> ApplyFilters(GetCultureObjectsDto getCultureObjectsDto, IQueryable<CultureObject> cultureObjectsQuery)
        {
            if (getCultureObjectsDto.ResponsiblePersonId.HasValue)
            {
                cultureObjectsQuery = cultureObjectsQuery.Where(cultureObject => cultureObject.ResponsiblePersonId == getCultureObjectsDto.ResponsiblePersonId.Value);
            }
            
            if (getCultureObjectsDto.CultureObjectCompanyId.HasValue)
            {
                cultureObjectsQuery = cultureObjectsQuery.Where(cultureObject => cultureObject.CultureObjectCompanyId == getCultureObjectsDto.CultureObjectCompanyId.Value);
            }

            if (!string.IsNullOrWhiteSpace(getCultureObjectsDto.SearchValue))
            {
                cultureObjectsQuery = cultureObjectsQuery.Where(cultureObject => cultureObject.Name.StartsWith(getCultureObjectsDto.SearchValue));       
            }
            return cultureObjectsQuery;
        }

        public async Task UpdateCultureObject(UpdateCultureObjectDto cultureObject, CancellationToken token)
        {
            CultureObject cultureObjectToUpdate = await _dbContext.CultureObjects.FindAsync(new object[] { cultureObject.Id }, cancellationToken: token);

            cultureObjectToUpdate.Name = cultureObject.Name;
            cultureObjectToUpdate.Phone = cultureObject.Phone;
            cultureObjectToUpdate.Address = cultureObject.Address;
            cultureObjectToUpdate.PostalCode = cultureObject.PostalCode;
            cultureObjectToUpdate.City = cultureObject.City;
            cultureObjectToUpdate.State = cultureObject.State;
            cultureObjectToUpdate.WorkingHours = cultureObject.WorkingHours;
            cultureObjectToUpdate.Latitude = cultureObject.Latitude;
            cultureObjectToUpdate.Longitude = cultureObject.Longitude;
            cultureObjectToUpdate.CultureObjectTypeId = cultureObject.CultureObjectTypeId;
            cultureObjectToUpdate.Notes = cultureObject.Notes;
            cultureObjectToUpdate.ResponsiblePersonId = cultureObject.ResponsiblePersonId;
            cultureObjectToUpdate.CultureObjectCompanyId=cultureObject.CultureObjectCompanyId;

            await _dbContext.SaveChangesAsync(token);
        }
    }
}
