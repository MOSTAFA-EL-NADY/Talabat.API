namespace Talabat.API.Error
{
    public class ExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ExceptionResponse(int code, string? msg, string? details) : base(code, msg)
        {
            Details = details;
        }
    }
}
