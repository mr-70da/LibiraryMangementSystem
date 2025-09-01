

using System.Net;

namespace LibraryManagementSystem.Application.DTOs
{
    public class GeneralResponse<T>
    {

        public GeneralResponse() { }
      
        public GeneralResponse(T data , bool isSuccess , String message, HttpStatusCode statusCode)
        {
            Data = data;
            Success = isSuccess;
            Message = message;
            this.statusCode = statusCode;
        }
        public HttpStatusCode statusCode { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
