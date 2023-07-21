using Announcements.Test.Domain.Common;
using Announcements.Test.Domain.Common.Exceptions;
using File = Announcements.Test.Domain.Common.ValueObjects.File;

namespace Announcements.Test.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        public int Number { get; set; }

        public string Text
        {
            get => _text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomainException($"The field {nameof(Text)} is required.");

                _text = value;
            }
        }
        private string _text = string.Empty;

        public File Image { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedAt { get; } = DateTime.UtcNow;

        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value <= CreatedAt)
                    throw new DomainException("The expiration date must be greater than  the creation date.");

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
