

namespace Notification.Application.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string name, object key)
            : base($"Entity \"{name}\" ({key}) bad request.")
        {
        }
    }
}
