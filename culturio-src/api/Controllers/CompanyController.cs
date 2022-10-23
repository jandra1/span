using Culturio.API.Models;
using Culturio.Application.Companies;
using Culturio.Application.Companies.Models;
using Culturio.Application.Users;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Culturio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        [HttpGet(Name = nameof(GetCompanies))]
        public async Task<GetCompaniesResponseModel> GetCompanies([FromQuery] GetCompanyDto model, CancellationToken token)
        {
            _logger.LogInformation("Fetching companies");

            var companies = await _companyService.GetCompanies(model, token);

            return new GetCompaniesResponseModel(companies);
        }

        [HttpGet("{id}", Name = nameof(GetCompany))]
        public async Task<IActionResult> GetCompany([FromRoute] int id, CancellationToken token)
        {
            _logger.LogInformation("Fetching company by id {CompanyId}", id);

            CompanyDto company = await _companyService.GetById(id, token);

            return company is not null ? Ok(company) : NotFound();
        }

        [HttpPost(Name = nameof(CreateCompany))]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto model, CancellationToken token)
        {
            _logger.LogInformation("Creating a new company");

            int companyId = await _companyService.CreateCompany(model, token);

            return CreatedAtAction(nameof(CreateCompany), new { Id = companyId });
        }

        [HttpDelete("{id}", Name = nameof(DeleteCompany))]
        public async Task<IActionResult> DeleteCompany([FromRoute] int id, CancellationToken token)
        {
            _logger.LogInformation("Deleting company with id {CompanyId}", id);

            bool companyDeleted = await _companyService.DeleteCompany(id, token);

            return companyDeleted ? NoContent() : NotFound();
        }

        [HttpPut(Name = nameof(UpdateCompany))]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating company with id {CompanyId}", model.Id);

            await _companyService.UpdateCompany(model, cancellationToken);

            return NoContent();
        }
    }
}
