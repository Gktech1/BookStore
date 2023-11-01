

using System.Net;

namespace Domain.DTOs
{
    public class GenericResponse<T>
    {
        public string ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}
