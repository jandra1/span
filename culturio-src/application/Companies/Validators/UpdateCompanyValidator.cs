using Culturio.Application.Companies.Models;
using Culturio.Application.CultureObjects;
using Culturio.Application.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Companies.Validators
{
    public class UpdateCompanyValidator:AbstractValidator<UpdateCompanyDto>
    {
        public UpdateCompanyValidator(CompanyService companyService,IUserService correspondencePersonService)
        {
            RuleFor(x => x.Id).MustAsync(companyService.CompanyExists).WithMessage(x => $"Company with id '{x.Id}' does not exist");
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
        }
    }
}
