using FluentValidation;

namespace StreetService.Features.Street.AddPointToStreet
{
    public class AddPointToStreetValidator : AbstractValidator<AddPointToStreetRequest>
    {
        public AddPointToStreetValidator()
        {
            RuleFor(x=>x.StreetId).NotEmpty();
            RuleFor(x=>x.Point).NotEmpty();
        }
    }
}
