using Culturio.Application.Collections;
using Culturio.Application.CultureObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjects
{
    public interface ICultureObjectService
    {
        public Task<IPagedList<CultureObjectDto>> GetCultureObjects(GetCultureObjectsDto getCultureObjectsDto,CancellationToken token);
        public Task<CultureObjectDto> GetById(int id, CancellationToken token);
        public Task<int> CreateCultureObject(CreateCultureObjectDto cultureObject, CancellationToken token);
        public Task UpdateCultureObject(UpdateCultureObjectDto cultureObject, CancellationToken token);
        public Task<bool> DeleteCultureObject(int id, CancellationToken token);
        public Task<bool> CultureObjectExists(int id, CancellationToken token);
    }
}
