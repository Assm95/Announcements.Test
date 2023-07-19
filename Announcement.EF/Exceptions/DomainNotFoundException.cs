namespace Announcements.EF.Exceptions
{
    public class DomainNotFoundException : Exception
    {
        public DomainNotFoundException(string entityName) : base($"{entityName} not found")
        {

        }
    }
}
