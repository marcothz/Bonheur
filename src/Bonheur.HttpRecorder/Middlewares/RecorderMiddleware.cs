using System.Text;
using Bonheur.HttpRecorder.Builders;

namespace Bonheur.HttpRecorder.Middlewares;

public class RecorderMiddleware
{
    private readonly RequestDelegate _next;

    public RecorderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var requestRecord = HttpRequestRecordBuilder.Create()
            .FromHttpRequest(context.Request)
            .WithBody(await GetRequestBody(context.Request))
            .Build();

        var originalBodyStream = context.Response.Body;

        await using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        var responseRecord = HttpResponseRecordBuilder.Create()
            .FromHttpResponse(context.Response)
            .WithBody(await GetResponseBody(context.Response))
            .Build();

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private static async Task<string> GetRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);

        var bodyAsText = Encoding.UTF8.GetString(buffer);

        request.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }

    private static async Task<string> GetResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);

        var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();

        response.Body.Seek(0, SeekOrigin.Begin);

        return bodyAsText;
    }
}