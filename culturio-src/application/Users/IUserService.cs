using Culturio.Application.Collections;
using Culturio.Application.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Users
{
    public interface IUserService
    {
        public Task<IPagedList<UserDto>> GetUsers(GetUserDto getUserDto, CancellationToken token);
        public Task<UserDto> GetById(int id, CancellationToken token);
        public Task<int> CreateUser(CreateUserDto createUserDto, CancellationToken token);
        public Task UpdateUser(UpdateUserDto updateUserDto, CancellationToken token);
        public Task<bool> DeleteUser(int id, CancellationToken token);
        public Task<bool> UserExists(int id, CancellationToken token);
        public Task<StringDto> GetQRcode(int id, CancellationToken token);
        public Task<StringDto> CheckQRcode(string qrcode, CancellationToken token);
        public Task<UserAuthDto> GetUserInfo(string sub, CancellationToken token);
    }
}
