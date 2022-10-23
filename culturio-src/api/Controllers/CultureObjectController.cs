using Culturio.API.Models;
using Culturio.Application.CultureObjects;
using Culturio.Application.CultureObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Culturio.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CultureObjectController : ControllerBase
    {
        private readonly ILogger<CultureObjectController> _logger;
        private readonly ICultureObjectService _cultureObjectService;
        public CultureObjectController(ILogger<CultureObjectController> logger,ICultureObjectService cultureObjectService)
        {
            _logger = logger;
            _cultureObjectService = cultureObjectService;
        }

        [HttpGet(Name = nameof(GetCultureObjects))]
        public async Task<GetCultureObjectsResponseModel> GetCultureObjects([FromQuery] GetCultureObjectsDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching culture objects");

            var cultureObjects = await _cultureObjectService.GetCultureObjects(model, cancellationToken);

            return new GetCultureObjectsResponseModel(cultureObjects);
        }

        [HttpGet("{id}", Name = nameof(GetCultureObject))]
        public async Task<IActionResult> GetCultureObject([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching culture object by id {CultureObjectId}", id);

            CultureObjectDto cultureObject = await _cultureObjectService.GetById(id, cancellationToken);

            return cultureObject is not null ? Ok(cultureObject) : NotFound();
        }

        [HttpPost(Name = nameof(CreateCultureObject))]
        public async Task<IActionResult> CreateCultureObject([FromBody] CreateCultureObjectDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new culture object");

            int cultureObjectId = await _cultureObjectService.CreateCultureObject(model, cancellationToken);

            return CreatedAtAction(nameof(CreateCultureObject), new { Id = cultureObjectId });
        }

        [HttpDelete("{id}", Name = nameof(DeleteCultureObject))]
        public async Task<IActionResult> DeleteCultureObject([FromRoute] int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting culture object with id {CultureObjectID}", id);

            bool cultureObjectDeleted = await _cultureObjectService.DeleteCultureObject(id, cancellationToken);

            return cultureObjectDeleted ? NoContent() : NotFound();
        }

        [HttpPut(Name = nameof(UpdateCultureObject))]
        public async Task<IActionResult> UpdateCultureObject([FromBody] UpdateCultureObjectDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating culture object with id {CultureObjectId}", model.Id);

            await _cultureObjectService.UpdateCultureObject(model, cancellationToken);

            return NoContent();
        }
    }
}
