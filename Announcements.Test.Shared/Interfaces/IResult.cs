namespace Announcements.Test.Shared.Interfaces
{
    public interface IResult<T>
    {
        T? Data { get; }

        List<string> Messages { get; }

    }
}
