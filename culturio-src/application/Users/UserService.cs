using Culturio.Application.Collections;
using Culturio.Application.Persistence;
using Culturio.Application.Services;
using Culturio.Application.Users.Models;
using Culturio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Users
{
    public class UserService : IUserService
    {
        private readonly CulturioDbContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly MsGraphService _graphService;

        public UserService(CulturioDbContext context, ILogger<UserService> logger, MsGraphService graphService)
        {
            _context = context;
            _logger = logger;
            _graphService = graphService;
        }

        public async Task<bool> UserExists(int id, CancellationToken token)
        {
            return await _context.Users.Where(user => user.Id == id && user.IsActive == true).AnyAsync(token);
        }

        public async Task<int> CreateUser(CreateUserDto user, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            var (upn, password, id) = await _graphService.CreateAzureB2CSameDomainUserAsync(user);

            

            User userToCreate = new()
            {
                //Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = upn,
                RoleId = user.RoleId,
                Phone = user.Phone,
                Address = user.Address,
                PostalCode = user.PostalCode,
                City = user.City,
                SexId = user.SexId,
                DefaultLanguage = user.DefaultLanguage,
                TermsAccepted = user.TermsAccepted,
                QRcode = user.QRcode,
                //DateCreated=DateTime.Now.Date, //provjerit ovo
                IsActive = true,
                CompanyId = user.CompanyId,
                CultureObjectId = user.CultureObjectId,
                ExternalId = id,
            };

            _context.Users.Add(userToCreate);
            await _context.SaveChangesAsync(token);
            return userToCreate.Id;
        }

        public async Task<bool> DeleteUser(int id, CancellationToken token)
        {
            User userToDelete = await _context.Users.Where(user => user.Id == id && user.IsActive == true)
                .FirstOrDefaultAsync(token);
            if (userToDelete is null)
                return false;

            userToDelete.IsActive = false;
            userToDelete.CompanyId = null;
            userToDelete.CultureObjectId = null;
            await _context.SaveChangesAsync(token);

            return true;
        }

        public async Task<IPagedList<UserDto>> GetUsers(GetUserDto getUserDto, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(getUserDto, nameof(getUserDto));

            IQueryable<User> userQuery = _context.Users.AsNoTracking();

            userQuery = ApplyFilters(getUserDto, userQuery);

            int totalCount = await userQuery.CountAsync(token);
            userQuery = ApplySortingAndPaging(getUserDto, userQuery);

            var users = await userQuery
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    ExternalId = user.ExternalId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role.Name,
                    Phone = user.Phone,
                    Address = user.Address,
                    PostalCode = user.PostalCode,
                    City = user.City,
                    Sex = user.Sex.Name,
                    DefaultLanguage = user.DefaultLanguage,
                    TermsAccepted = user.TermsAccepted,
                    QRcode = user.QRcode,
                    DateCreated = user.DateCreated,
                    IsActive = user.IsActive,
                    Company = user.Company.Name,
                    CultureObject = user.CultureObject.Name
                })
                .ToListAsync(cancellationToken: token);

            return new PagedList<UserDto>(users, getUserDto.Page ?? 1, getUserDto.PageSize ?? int.MaxValue, totalCount);
        }

        private IQueryable<User> ApplySortingAndPaging(GetUserDto getUserDto, IQueryable<User> userQuery)
        {
            userQuery = getUserDto.SortOrder switch
            {
                "asc" => userQuery.OrderBy(x => x.LastName),
                "desc" => userQuery.OrderByDescending(x => x.LastName),
                _ => userQuery.OrderByDescending(x => x.Id)
            };

            if (getUserDto.Page.HasValue && getUserDto.PageSize.HasValue)
            {
                userQuery = userQuery.Skip((getUserDto.Page.Value - 1) * getUserDto.PageSize.Value)
                    .Take(getUserDto.PageSize.Value);
            }

            return userQuery;
        }

        private static IQueryable<User> ApplyFilters(GetUserDto getUserDto, IQueryable<User> userQuery)
        {
            if (getUserDto.GetOnlyActive)
            {
                userQuery = userQuery.Where(user => (bool)user.IsActive);
            }

            if (getUserDto.RoleId.HasValue)
            {
                userQuery = userQuery.Where(user => user.RoleId == getUserDto.RoleId.Value);
            }

            if (!string.IsNullOrWhiteSpace(getUserDto.SearchValue))
            {
                userQuery = userQuery.Where(user => user.FirstName.StartsWith(getUserDto.SearchValue) || user.LastName.StartsWith(getUserDto.SearchValue));
            }

            return userQuery;
        }

        public async Task<UserDto> GetById(int id, CancellationToken token)
        {
            return await _context.Users.Where(user => user.Id == id)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    ExternalId = user.ExternalId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role.Name,
                    Phone = user.Phone,
                    Address = user.Address,
                    PostalCode = user.PostalCode,
                    City = user.City,
                    Sex = user.Sex.Name,
                    DefaultLanguage = user.DefaultLanguage,
                    TermsAccepted = user.TermsAccepted,
                    QRcode = user.QRcode,
                    DateCreated = user.DateCreated,
                    IsActive = user.IsActive,
                    Company = user.Company.Name,
                    CultureObject = user.CultureObject.Name
                })
                .FirstOrDefaultAsync(token);
        }

        public async Task UpdateUser(UpdateUserDto user, CancellationToken token)
        {
            User userToUpdate = await _context.Users.FindAsync(new object[] { user.Id }, cancellationToken: token);

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.RoleId = user.RoleId;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Address = user.Address;
            userToUpdate.PostalCode = user.PostalCode;
            userToUpdate.City = user.City;
            userToUpdate.SexId = user.SexId;
            userToUpdate.DefaultLanguage = user.DefaultLanguage;
            userToUpdate.TermsAccepted = user.TermsAccepted;
            userToUpdate.QRcode = user.QRcode;
            //userToUpdate.DateCreated = user.DateCreated;//mozda bez ovoga
            userToUpdate.IsActive = user.IsActive;
            userToUpdate.CompanyId = user.CompanyId;
            userToUpdate.CultureObjectId = user.CultureObjectId;

            await _context.SaveChangesAsync(token);
        }

        public async Task<StringDto> GetQRcode(int id, CancellationToken token)
        {
            StringDto result = new StringDto();
            UserDto user = await GetById(id, token);
            int qrLength = 4;
            SHA256 sha256Hash = SHA256.Create();
            String dateTimeNow = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK");
            byte[] resultarr = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(id.ToString() + dateTimeNow));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < qrLength; i++)
            {
                builder.Append(resultarr[i].ToString("x2"));
            }
            result = new StringDto
            {
                output = builder.ToString()
            };

            User userToUpdate = await _context.Users.FindAsync(new object[] { id }, cancellationToken: token);
            userToUpdate.QRcode = result.output;
            await _context.SaveChangesAsync(token);

            return result;
        }

        public async Task<StringDto> CheckQRcode(string qrcode, CancellationToken token)
        {
            User userForCheckingQR = await _context.Users.Where(user => user.QRcode == qrcode && user.IsActive == true)
                .FirstOrDefaultAsync(token);

            StringDto response = new StringDto();

            if (userForCheckingQR is null)
            {
                response.output = "Code is invalid";
                return response;
            }

            response.output = "Code is valid";
            return response;
        }

        public async Task<UserAuthDto> GetUserInfo(string sub, CancellationToken token)
        {
            return await _context.Users.Where(user => user.ExternalId == sub)
                .Select(user => new UserAuthDto
                {
                    Id = user.Id,
                    ExternalId = user.ExternalId,
                    Role = user.Role.Name,
                })
                .FirstOrDefaultAsync(token);
        }
    }
}
