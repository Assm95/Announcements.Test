namespace Announcements.EF.Exceptions
{
    internal class NotLoadedException : Exception
    {
        public NotLoadedException(string propertyName) : base($"Property is not loaded: {propertyName}") { }
    }
}
