using System.Net;

namespace Asset.API.DTO.Responses
{
    public class BaseApiResponseDto
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; } = "";
    }
}