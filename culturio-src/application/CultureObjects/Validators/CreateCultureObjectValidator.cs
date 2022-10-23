using Culturio.Application.CultureObjects.Models;
using Culturio.Application.Users;
using Culturio.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjects.Validators
{
    public class CreateCultureObjectValidator:AbstractValidator<CreateCultureObjectDto>
    {
        public CreateCultureObjectValidator(IUserService responsiblePersonService)
        {
            RuleFor(cultureObject => cultureObject.Name).NotEmpty();
            RuleFor(cultureObject => cultureObject.Phone).NotEmpty();
            RuleFor(cultureObject => cultureObject.Address).NotEmpty(); 
            RuleFor(cultureObject => cultureObject.PostalCode).NotEmpty(); 
            RuleFor(cultureObject => cultureObject.City).NotEmpty(); 
            RuleFor(cultureObject => cultureObject.State).NotEmpty(); 
            RuleFor(cultureObject => cultureObject.WorkingHours).NotEmpty();
            RuleFor(cultureObject => cultureObject.CultureObjectTypeId).NotEmpty();
            RuleFor(cultureObject => cultureObject.ResponsiblePersonId).MustAsync(responsiblePersonService.UserExists)
                .WithMessage(cultureObject => $"Responsible person with id '{cultureObject.ResponsiblePersonId}' does not exist");
            RuleFor(cultureObject => cultureObject.CultureObjectCompanyId).NotEmpty();
            RuleFor(cultureObject => cultureObject.Notes).NotEmpty().MaximumLength(2000); 
        }
    }
}
