namespace Announcements.Test.Domain.Resources
{
    internal class ErrorsSource
    {
        private ErrorsSource() {}

        public static string FieldIsRequired(string name) => string.Format(Errors.Field_IsRequired, name);

        public static string ExpirationDateInvalid  => Errors.ExpirationDate_Invalid;
    }
}
