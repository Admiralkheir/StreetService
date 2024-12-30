using FluentValidation;

namespace StreetService.Features.Street.GetStreet
{
    public class GetStreetValidator : AbstractValidator<GetStreetRequest>
    {
        public GetStreetValidator()
        {
            RuleFor(x=>x.StreetId).NotEmpty();
        }
    }
}
