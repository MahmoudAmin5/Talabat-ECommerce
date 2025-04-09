namespace Talabat.APIs.Errors
{
    public class ApiExceptionErrorResponse:ApiResponse
    {
        public ApiExceptionErrorResponse(int statuscode, string? message = null, string? details=null) : base(statuscode, message)
        {
            Details = details;
        }

        public string? Details { get; set; }

    }
}
