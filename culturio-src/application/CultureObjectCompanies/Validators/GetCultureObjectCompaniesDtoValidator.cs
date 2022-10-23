using Culturio.Application.Companies.Models;
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
    public class GetCultureObjectCompaniesDtoValidator : AbstractValidator<GetCultureObjectCompanyDto>
    {
        private static readonly string[] _validSortValues = new[] { "asc", "desc" };
        public GetCultureObjectCompaniesDtoValidator(IUserService correspondencePersonService)
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

            When(x => x.CorrespondencePersonId.HasValue, () =>
            {
                RuleFor(x => x.CorrespondencePersonId).MustAsync((correspondencePersonId, token) => correspondencePersonService.UserExists(correspondencePersonId.Value, token))
                    .WithMessage(getCultureObjectCompaniesDto => $"Correspondence person with id '{getCultureObjectCompaniesDto.CorrespondencePersonId.Value}' does not exist");
            });

            When(x => !string.IsNullOrWhiteSpace(x.SearchValue), () =>
            {
                RuleFor(x => x.SearchValue).MaximumLength(256);
            });

            When(x => !string.IsNullOrWhiteSpace(x.SortOrder), () =>
            {
                RuleFor(x => x.SortOrder).Must(sortOrder => _validSortValues.Contains(sortOrder.ToLowerInvariant()))
                    .WithMessage($"Valid values for '{nameof(GetCompanyDto.SortOrder)}' are: {string.Join(", ", _validSortValues.Select(x => $"'{x}'"))}");
            });
        }
    }
}
