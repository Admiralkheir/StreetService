using MediatR;

namespace StreetService.Features.Street.GetStreet
{
    public record GetStreetRequest(int StreetId) : IRequest<GetStreetResponse>;
}
