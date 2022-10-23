using Culturio.Application.Collections;
using Culturio.Application.Users.Models;

namespace Culturio.API.Models
{
    public class GetUsersResponseModel
    {
        public IEnumerable<UserDto> Users { get; private set; }
        public int TotalCount { get; private set; }

        public GetUsersResponseModel(IPagedList<UserDto> userDtoPagedList)
        {
            Users = userDtoPagedList;
            TotalCount = userDtoPagedList.TotalCount;
        }
    }
}
