using System.Globalization;

namespace Talabat.API.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int code, string? msg)
        {
            StatusCode = code;
            Message = msg ?? getDefultMessage(StatusCode);
        }

        public string? getDefultMessage(int stutusCode)
        {
            return stutusCode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                404 => "page not found",
                500 => "interna server error",
                // defualt
                _ => null

            };
        }
    }
}
