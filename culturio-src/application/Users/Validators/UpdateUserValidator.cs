using Culturio.Application.CultureObjectCompanies;
using Culturio.Application.Users.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.Users.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator(IUserService userService)
        {
            RuleFor(x => x.Id).MustAsync(userService.UserExists).WithMessage(x => $"User with id '{x.Id}' does not exist");
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(256);
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(256);
            RuleFor(user => user.Email).NotEmpty().MaximumLength(256);
            RuleFor(user => user.RoleId).NotEmpty();
        }
    }
}
