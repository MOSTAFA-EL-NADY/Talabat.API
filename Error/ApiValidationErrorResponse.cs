namespace Talabat.API.Error
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse(string? msg) : base(400, msg)
        {
            Errors = new List<string>();
        }



    }
}
