namespace Bonheur.HttpRecorder.Models
{
    public class HttpResponseRecord
    {
        public string Body { get; }

        public HeadersRecord Headers { get; }

        public int StatusCode { get; }

        public HttpResponseRecord(int statusCode, HeadersRecord headers, string body)
        {
            StatusCode = statusCode;
            Headers = headers;
            Body = body;
        }
    }
}