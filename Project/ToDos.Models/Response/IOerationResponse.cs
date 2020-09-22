using System;
using System.Collections.Generic;
using System.Text;

namespace ToDos.Models.Response
{
    public interface IOperationResponse<T>
    {

        string Message { get; set; }

        T Content { get; set; }

    }

    public class NotFoundOperationResponse<T> : IOperationResponse<T>
    {
        public string Message { get; set; }
        public T Content { get; set; }
    }

    public class SuccessOperatoinResponse<T> : IOperationResponse<T>
    {
        public string Message { get; set; }
        public T Content { get; set; }

    }
}
