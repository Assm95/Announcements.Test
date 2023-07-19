using Announcements.EF.Const;

namespace Announcements.EF.Resources
{
    internal class ErrorsSource
    {
        private ErrorsSource() {}

        public static string FieldIsRequired(string name) => string.Format(Errors.Field_IsRequired, name);

        public static string ExpirationDateInvalid  => Errors.ExpirationDate_Invalid;

        public static string AnnouncementOwnerInvalid => Errors.AnnouncementOwner_Invalid;

        public static string AnnouncementNumberUnique => Errors.AnnouncementNumber_Unique;

        public static string AnnouncementExist => Errors.Announcement_Exist;

        public static string DateTimeInvalidFormat(string invalidFormat) =>
            string.Format(Errors.DateTime_InvalidFormat, invalidFormat, FormatConst.DateTimeFormat);
    }
}
