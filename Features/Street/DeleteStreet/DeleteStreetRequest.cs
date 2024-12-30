using MediatR;

namespace StreetService.Features.Street.DeleteStreet
{
    public record DeleteStreetRequest(int StreetId) : IRequest<DeleteStreetResponse>;
}
