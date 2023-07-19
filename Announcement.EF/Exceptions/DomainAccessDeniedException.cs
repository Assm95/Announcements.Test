namespace Announcements.EF.Exceptions
{
    public class DomainAccessDeniedException : Exception
    {
        public DomainAccessDeniedException(string message) : base(message) { }
    }
}
