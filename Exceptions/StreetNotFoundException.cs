namespace StreetService.Exceptions
{
    public class StreetNotFoundException : NotFoundException
    {
        public StreetNotFoundException(int id)
            :base($"The street with these identifiers id: {id} was not found")
        {
            
        }
    }
}
