namespace Announcements.Test.Domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
