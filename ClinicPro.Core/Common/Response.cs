namespace ClinicPro.Core.Common
{

    public class Response<T> : Response
    {
        public T? Data { get; set; }
    }

    public class Response
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "Success";
    }

    
}
