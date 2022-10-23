using Culturio.Application.Users.Models;
using FluentValidation;


namespace Culturio.Application.Users.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(256);
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(256);
            RuleFor(user => user.Email).NotEmpty().MaximumLength(256);
            RuleFor(user => user.RoleId).NotEmpty();

        }
    }
}
