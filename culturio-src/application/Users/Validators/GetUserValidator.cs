using Culturio.Application.Users.Models;
using FluentValidation;

namespace Culturio.Application.Users.Validators
{
    public class GetUserValidator : AbstractValidator<GetUserDto>
    {
        private static readonly string[] _validSortValues = new[] { "asc", "desc" };

        public GetUserValidator()
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

            When(x => !string.IsNullOrWhiteSpace(x.SearchValue), () =>
            {
                RuleFor(x => x.SearchValue).MaximumLength(256);
            });

            When(x => !string.IsNullOrWhiteSpace(x.SortOrder), () =>
            {
                RuleFor(x => x.SortOrder).Must(sortOrder => _validSortValues.Contains(sortOrder.ToLowerInvariant()))
                    .WithMessage($"Valid values for '{nameof(GetUserDto.SortOrder)}' are: {string.Join(", ", _validSortValues.Select(x => $"'{x}'"))}");
            });
        }
    }
}
