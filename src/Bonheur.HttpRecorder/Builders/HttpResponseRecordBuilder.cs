using System.Text;
using Bonheur.HttpRecorder.Models;
using Microsoft.Extensions.Primitives;

namespace Bonheur.HttpRecorder.Builders
{
    public class HttpResponseRecordBuilder
    {
        private string? _body;
        private HttpResponse? _response;

        private HttpResponseRecordBuilder()
        {
        }

        public static HttpResponseRecordBuilder Create()
        {
            return new HttpResponseRecordBuilder();
        }

        public HttpResponseRecord? Build()
        {
            if (_response == null)
            {
                return default;
            }

            var statusCode = _response.StatusCode;
            var headers = BuildHeadersRecord(_response);
            var body = _body ?? string.Empty;

            return new HttpResponseRecord(statusCode, headers, body);
        }

        public HttpResponseRecordBuilder FromHttpResponse(HttpResponse response)
        {
            _response = response;

            return this;
        }

        public HttpResponseRecordBuilder WithBody(string? body)
        {
            _body = body;

            return this;
        }

        private static HeadersRecord BuildHeadersRecord(HttpResponse response)
        {
            return new HeadersRecord(response.Headers.Select(CloneItem));
        }

        private static KeyValuePair<string, StringValues> CloneItem(KeyValuePair<string, StringValues> src)
        {
            var value = new StringValues(src.Value.ToArray());

            return new KeyValuePair<string, StringValues>(src.Key, value);
        }
    }
}