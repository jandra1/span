using Culturio.API.Models;
using Culturio.Application.Companies;
using Culturio.Application.Companies.Models;
using Culturio.Application.CultureObjectCompanies;
using Culturio.Application.CultureObjectCompanies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Culturio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CultureObjectCompanyController : ControllerBase
    {
        private readonly ILogger<CultureObjectCompanyController> _logger;
        private readonly ICultureObjectCompanyService _cultureObjectCompanyService;

        public CultureObjectCompanyController(ILogger<CultureObjectCompanyController> logger, ICultureObjectCompanyService companyService)
        {
            _logger = logger;
            _cultureObjectCompanyService = companyService;
        }

        [HttpGet(Name = nameof(GetCultureObjectCompanies))]
        public async Task<GetCultureObjectCompaniesResponseModel> GetCultureObjectCompanies([FromQuery] GetCultureObjectCompanyDto model, CancellationToken token)
        {
            _logger.LogInformation("Fetching culture object companies");

            var cultureObjectCompanies = await _cultureObjectCompanyService.GetCultureObjectCompanies(model, token);

            return new GetCultureObjectCompaniesResponseModel(cultureObjectCompanies);
        }

        [HttpGet("{id}", Name = nameof(GetCultureObjectCompany))]
        public async Task<IActionResult> GetCultureObjectCompany([FromRoute] int id, CancellationToken token)
        {
            _logger.LogInformation("Fetching culture object company by id {CultureObjectCompanyId}", id);

            CultureObjectCompanyDto company = await _cultureObjectCompanyService.GetById(id, token);

            return company is not null ? Ok(company) : NotFound();
        }

        [HttpPost(Name = nameof(CreateCultureObjectCompany))]
        public async Task<IActionResult> CreateCultureObjectCompany([FromBody] CreateCultureObjectCompanyDto model, CancellationToken token)
        {
            _logger.LogInformation("Creating a new culture object company");

            int companyId = await _cultureObjectCompanyService.CreateCultureObjectCompany(model, token);

            return CreatedAtAction(nameof(CreateCultureObjectCompany), new { Id = companyId });
        }

        [HttpDelete("{id}", Name = nameof(DeleteCultureObjectCompany))]
        public async Task<IActionResult> DeleteCultureObjectCompany([FromRoute] int id, CancellationToken token)
        {
            _logger.LogInformation("Deleting culture object company with id {CultureObjectCompanyId}", id);

            bool companyDeleted = await _cultureObjectCompanyService.DeleteCultureObjectCompany(id, token);

            return companyDeleted ? NoContent() : NotFound();
        }

        [HttpPut(Name = nameof(UpdateCultureObjectCompany))]
        public async Task<IActionResult> UpdateCultureObjectCompany([FromBody] UpdateCultureObjectCompanyDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating culture object company with id {CultureObjectCompanyId}", model.Id);

            await _cultureObjectCompanyService.UpdateCultureObjectCompany(model, cancellationToken);

            return NoContent();
        }
    }
}
