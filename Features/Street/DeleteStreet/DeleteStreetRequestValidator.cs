using FluentValidation;

namespace StreetService.Features.Street.DeleteStreet
{
    public class DeleteStreetRequestValidator : AbstractValidator<DeleteStreetRequest>
    {
        public DeleteStreetRequestValidator()
        {
            RuleFor(x=>x.StreetId).NotEmpty();
        }
    }
}
