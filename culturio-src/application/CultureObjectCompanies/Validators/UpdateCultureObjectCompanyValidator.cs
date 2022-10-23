using Culturio.Application.Companies;
using Culturio.Application.CultureObjectCompanies.Models;
using Culturio.Application.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjectCompanies.Validators
{
    public class UpdateCultureObjectCompanyValidator:AbstractValidator<UpdateCultureObjectCompanyDto>
    {
        public UpdateCultureObjectCompanyValidator(CultureObjectCompanyService cultureObjectCompanyService, IUserService correspondencePersonService)
        {
            RuleFor(x => x.Id).MustAsync(cultureObjectCompanyService.CultureObjectCompanyExists).WithMessage(x => $"Culture object company with id '{x.Id}' does not exist");
            RuleFor(company => company.Name).NotEmpty();
            RuleFor(company => company.TaxId).NotEmpty();
            RuleFor(company => company.VatId).NotEmpty();
            RuleFor(company => company.Phone).NotEmpty();
            RuleFor(company => company.Address).NotEmpty();
            RuleFor(company => company.PostalCode).NotEmpty();
            RuleFor(company => company.City).NotEmpty();
            RuleFor(company => company.State).NotEmpty();
            RuleFor(company => company.CorrespondencePersonId).MustAsync(correspondencePersonService.UserExists)
                .WithMessage(company => $"Correspondence person with id '{company.CorrespondencePersonId}' does not exist");
            RuleFor(company => company.CompanyLogo).NotEmpty();
            RuleFor(company => company.IBAN).NotEmpty();
        }
    }
}
