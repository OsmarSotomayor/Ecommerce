using System.Globalization;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null) {
            StatusCode = statusCode;
            Message = message ?? this.getDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        private string getDefaultMessageForStatusCode(int statusCode)
        {
            //usaremos una estructura especial para el switch           
            return statusCode switch
            {
                400 => "You have made a bad request",
                401 => "You are not Authorized",
                404 => "It resource was not found",
                500 => "Error are in the dark side",
                _ => null
            };
        }
    }
}
