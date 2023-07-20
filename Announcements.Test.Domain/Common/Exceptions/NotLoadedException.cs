namespace Announcements.Test.Domain.Common.Exceptions
{
    public class NotLoadedException : Exception
    {
        public NotLoadedException(string propertyName) : base($"Property is not loaded: {propertyName}") { }
    }
}
