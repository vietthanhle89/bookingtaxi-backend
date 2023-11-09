
namespace bookingtaxi_backend.Model
{
    public class ResponseObject
    {
    }

    public class ErrorResponse { 
        public string Message { get; set; }
        

        public ErrorResponse(string message) {
            Message = message;
        }

    }
}
