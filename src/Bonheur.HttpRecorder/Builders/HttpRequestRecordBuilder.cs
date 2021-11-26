using System.Text;
using Bonheur.HttpRecorder.Models;
using Microsoft.Extensions.Primitives;

namespace Bonheur.HttpRecorder.Builders
{
    public class HttpRequestRecordBuilder
    {
        private string? _body;
        private HttpRequest? _request;

        private HttpRequestRecordBuilder()
        {
        }

        public static HttpRequestRecordBuilder Create()
        {
            return new HttpRequestRecordBuilder();
        }

        public HttpRequestRecord? Build()
        {
            if (_request == null)
            {
                return default;
            }

            var scheme = _request.Scheme;
            var method = _request.Method;
            var host = _request.Host.Value;
            var path = _request.Path.Value ?? string.Empty;
            var query = BuildQueryStringRecord(_request);
            var headers = BuildHeadersRecord(_request);
            var body = _body ?? string.Empty;

            return new HttpRequestRecord(scheme, method, host, path, query, headers, body);
        }

        public HttpRequestRecordBuilder FromHttpRequest(HttpRequest request)
        {
            _request = request;

            return this;
        }

        public HttpRequestRecordBuilder WithBody(string? body)
        {
            _body = body;

            return this;
        }

        private static HeadersRecord BuildHeadersRecord(HttpRequest request)
        {
            return new HeadersRecord(request.Headers.Select(CloneItem));
        }

        private static QueryStringRecord BuildQueryStringRecord(HttpRequest request)
        {
            return new QueryStringRecord(request.Query.Select(CloneItem));
        }

        private static KeyValuePair<string, StringValues> CloneItem(KeyValuePair<string, StringValues> src)
        {
            var value = new StringValues(src.Value.ToArray());

            return new KeyValuePair<string, StringValues>(src.Key, value);
        }
    }
}