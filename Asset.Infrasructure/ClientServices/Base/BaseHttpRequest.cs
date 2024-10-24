using FinancialTask.Services.Fintacharts.Responses;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web;

namespace FinancialTask.ClientServices.Base
{
    public class BaseHttpRequest<T> : HttpRequestMessage
        where T : BaseHttpResponse, new()
    {
        static HttpClient _client = new HttpClient();
        KeyValuePair<string, string>[]? _parameters;

        protected void SetParameters(KeyValuePair<string, string>[] values)
        {
            _parameters = values;
        }

        public async Task<BaseHttpResponse> GetResponse()
        {
            try
            {
                HttpResponseMessage response = await SendHttpRequest();
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<T>(responseContent) ?? new T();
                responseObject.StatusCode = response.StatusCode;

                return responseObject;
            }
            catch (Exception ex)
            {
                return new T()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public void AddTokenInAuthenticationHeader(string token)
        {
            this.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private async Task<HttpResponseMessage> SendHttpRequest()
        {
            if (Method == HttpMethod.Get)
            {
                SetQueryParameters();
            }
            else if (Method == HttpMethod.Post)
            {
                SetBodyParameters();
            }
            else
            {
                throw new NotImplementedException($"No implemented http method: {Method} ");
            }

            return await _client.SendAsync(this);
        }

        private void SetQueryParameters()
        {
            if (_parameters != null && RequestUri is not null)
            {
                UriBuilder uriBuilder = new UriBuilder(RequestUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                foreach (var param in _parameters)
                {
                    if (!string.IsNullOrEmpty(param.Value))
                    {
                        query[param.Key] = param.Value;
                    }
                }
                uriBuilder.Query = query.ToString();
                RequestUri = uriBuilder.Uri;
            }
        }

        private void SetBodyParameters()
        {
            if (_parameters != null)
            {
                var content = new FormUrlEncodedContent(_parameters);
                Content = content;
            }
        }
    }
}