using Announcements.Test.Domain.Common;
using Announcements.Test.Domain.Common.Exceptions;
using Announcements.Test.Domain.Resources;

namespace Announcements.Test.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new DomainException(ErrorsSource.FieldIsRequired(nameof(Name)));

                _name = value;
            }
        }
        private string _name = string.Empty;

        public bool IsAdmin { get; set; }

        public IReadOnlyCollection<Announcement> Announcements => _announcements ?? throw new NotLoadedException(nameof(Announcements));

        private readonly List<Announcement> _announcements;


#pragma warning disable CS8618
        protected User() { }
#pragma warning restore CS8618

        internal User(string name, bool isAdmin)
        {
            _announcements = new List<Announcement>();
            Name = name;
            IsAdmin = isAdmin;
        }
    }
}
