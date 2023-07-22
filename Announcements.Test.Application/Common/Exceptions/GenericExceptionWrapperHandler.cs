using Announcements.Test.Domain.Common.Exceptions;
using MediatR.Pipeline;

namespace Announcements.Test.Application.Common.Exceptions
{
    internal class GenericExceptionWrapperHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TException : Exception 
        where TRequest : notnull
    {
        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
            CancellationToken cancellationToken)
        {
            switch (exception)
            {
                case NotFoundException:
                case BadRequestException: throw exception;
                case DomainException: throw new BadRequestException(exception.Message);
                default:
                    throw new ApplicationException(exception.Message, exception);
            }
        }
    }
}
