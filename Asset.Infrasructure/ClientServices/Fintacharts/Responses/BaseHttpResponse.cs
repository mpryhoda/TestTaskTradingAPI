using System.Runtime.Serialization;

namespace FinancialTask.Services.Fintacharts.Responses
{
    public class BaseHttpResponse(string Message = "") 
        : HttpResponseMessage
    {
        [DataMember(Name ="message")]
        public string Message { get; set; } = Message;
    }
}
