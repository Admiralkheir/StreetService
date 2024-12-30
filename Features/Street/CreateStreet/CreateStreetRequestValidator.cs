using FluentValidation;

namespace StreetService.Features.Street.CreateStreet
{
    public class CreateStreetRequestValidator : AbstractValidator<CreateStreetRequest>
    {
        public CreateStreetRequestValidator()
        {
            RuleFor(x=>x.Capacity).NotEmpty();
            RuleFor(x=>x.StreetName).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Geometry).NotEmpty();
        }
    }
}
