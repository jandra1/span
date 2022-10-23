using Culturio.Application.CultureObjects.Models;
using Culturio.Application.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturio.Application.CultureObjects.Validators
{
    public class GetCultureObjectsDtoValidator:AbstractValidator<GetCultureObjectsDto>
    {
        private static readonly string[] _validSortValues = new[] { "asc", "desc" };

        public GetCultureObjectsDtoValidator(IUserService responsiblePersonService)
        {
            When(x => x.Page.HasValue, () =>
            {
                RuleFor(x => x.PageSize).NotNull();
                RuleFor(x => x.Page).GreaterThan(0);
            });

            When(x => x.PageSize.HasValue, () =>
            {
                RuleFor(x => x.Page).NotNull();
                RuleFor(x => x.PageSize).GreaterThan(0);
            });

            When(x => x.ResponsiblePersonId.HasValue, () =>
            {
                RuleFor(x => x.ResponsiblePersonId).MustAsync((responsiblePersonId, token) => responsiblePersonService.UserExists(responsiblePersonId.Value, token))
                    .WithMessage(getCultureObjectsDto => $"Responsible person with id '{getCultureObjectsDto.ResponsiblePersonId.Value}' does not exist");
            });

            When(x => !string.IsNullOrWhiteSpace(x.SearchValue), () =>
            {
                RuleFor(x => x.SearchValue).MaximumLength(256);
            });

            When(x => !string.IsNullOrWhiteSpace(x.SortOrder), () =>
            {
                RuleFor(x => x.SortOrder).Must(sortOrder => _validSortValues.Contains(sortOrder.ToLowerInvariant()))
                    .WithMessage($"Valid values for '{nameof(GetCultureObjectsDto.SortOrder)}' are: {string.Join(", ", _validSortValues.Select(x => $"'{x}'"))}");
            });
        }
    }
}
