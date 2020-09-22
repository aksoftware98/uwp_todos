using ToDos.Models.Response;

namespace ToDos.Services
{
    public class BaseService
    {
        protected IOperationResponse<T> NotFound<T>(string message)
        {
            return new NotFoundOperationResponse<T>()
            {
                Message = message
            };
        }

        protected IOperationResponse<T> Success<T>(string message, T content)
        {
            return new SuccessOperatoinResponse<T>()
            {
                Message = message,
                Content = content,
            };
        }
    }
}
