using System.Globalization;
using Announcements.EF.Exceptions;
using Announcements.EF.Resources;
using DelegateDecompiler;

namespace Announcements.EF.Models
{
    public class Announcement
    {
        public Guid Id { get; } = Guid.NewGuid();

        public int Number { get; set; }

        public string Text
        {
            get => _text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomainException(ErrorsSource.FieldIsRequired(nameof(Text)));

                _text = value;
            }
        }
        private string _text = string.Empty;

        public File Image
        {
            get => _image;
            set
            {
                if (value.Data.Length == 0)
                    throw new DomainException(ErrorsSource.FieldIsRequired(nameof(Image)));

                _image = value;
            }
        }

        private File _image = null!;

        public int Rating { get; set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value <= CreatedAt)
                    throw new DomainException(ErrorsSource.ExpirationDateInvalid);

                _expirationDate = value;
            }
        }
        private DateTime _expirationDate;

        public Guid UserId { get; }

        public User User { get; }
        
#pragma warning disable CS8618
        protected Announcement() { }
#pragma warning restore CS8618

        public Announcement(User user, int number, string text, File image, int rating, DateTime expirationDate)
        {
            User = user;
            UserId = user.Id;
            Number = number;
            Text = text;
            Image = image;
            Rating = rating;
            ExpirationDate = expirationDate;
        }
    }
}
