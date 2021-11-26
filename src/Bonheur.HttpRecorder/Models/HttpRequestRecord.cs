namespace Bonheur.HttpRecorder.Models
{
    public class HttpRequestRecord
    {
        public string Body { get; }

        public HeadersRecord Headers { get; }

        public string Host { get; }

        public string Method { get; set; }

        public string Path { get; }

        public QueryStringRecord QueryString { get; }

        public string Scheme { get; }

        public HttpRequestRecord(string scheme, string method, string host, string path, QueryStringRecord queryString, HeadersRecord headers, string body)
        {
            Scheme = scheme;
            Method = method;
            Host = host;
            Path = path;
            QueryString = queryString;
            Headers = headers;
            Body = body;
        }
    }
}