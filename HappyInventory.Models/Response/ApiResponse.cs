using HappyInventory.Helpers.AppConstant;
using System.Net;

namespace HappyInventory.Models.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public HttpStatusCode StatusCode { get; set; }

        public ApiResponse(bool success, T data = default, string message = null,
            List<string> errors = null,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Success = success;
            Data = data;
            Message = message;
            if (errors != null)
                Errors = errors;
            StatusCode = statusCode;
        }

        public static ApiResponse<T> SuccessResponse(T data, 
            string message = ResponseMessages.SuccessUserCreated,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse<T>(true, data, message, null, statusCode);
        }

        public static ApiResponse<T> ErrorResponse(List<string> errors,
            string message = ResponseMessages.ErrorFailedToCreateUser,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse<T>(false, default, message, errors, statusCode);
        }
    }
}
