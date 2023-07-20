namespace Announcements.Test.Shared.Interfaces
{
    public interface IResult<T>
    {
        bool Succeeded { get; set; }

        T? Data { get; set; }

        Exception? Exception { get; set; }

        List<string> Messages { get; set; }

        int Code { get; set; }
    }
}
