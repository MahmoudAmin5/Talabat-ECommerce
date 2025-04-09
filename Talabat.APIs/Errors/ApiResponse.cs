
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statuscode, string? message=null) 
        { 
           StatusCode = statuscode;
           Message = message??GetDefaultMassageForStatusCode(statuscode);
        }

        private string? GetDefaultMassageForStatusCode(int Statuscode)
        {
            return Statuscode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Errors are the path to the dark side, Errors lead to anger, Anger leads to hate, hate leads to career shift ",
                _ => null

            };
        }
    }
}
