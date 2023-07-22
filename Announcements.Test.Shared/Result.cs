using Announcements.Test.Shared.Interfaces;

namespace Announcements.Test.Shared
{
    public class Result<T> : IResult<T>
    {
        public T Data { get; }
        public List<string> Messages { get; } = new ();

#pragma warning disable CS8618
        private Result() { }
#pragma warning restore CS8618

        public Result(T data)
        {
            Data = data;
        }

        public Result(List<string> messages)
        {
            Messages = messages;
        }

        public Result(T data, List<string> messages) : this(data)
        {
            Messages = messages;
        }

        public Result(T data, string message) : this(data, new List<string>{ message })
        {
        }
    }
}
