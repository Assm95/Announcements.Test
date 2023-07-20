using Announcements.Test.Domain.Common.Exceptions;

namespace Announcements.Test.Application.Common.Exceptions
{
    internal class ExceptionWrapper<T>
    {
        public static async Task<T> Catch(Func<Task<T>> func)
        {
            try
            {
                return await func.Invoke();
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (DomainException e)
            {
                throw new BadRequestException(e.Message);
            }
            catch (Exception e)
            {
               throw new ApplicationException(e.Message, e.InnerException);
            }
        }
    }
}
