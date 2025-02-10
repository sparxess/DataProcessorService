using DataProcessorService.Application.Contracts.HttpLogs;

namespace DataProcessorService.HttpApi.Host.Middleware;

public class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HttpLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var requestLogService = scope.ServiceProvider.GetRequiredService<IHttpLogService>();
            var originalBodyStream = context.Response.Body;

            using (var requestBodyStream = new MemoryStream())
            {
                await CopyRequestBodyAsync(context.Request.Body, requestBodyStream);
                context.Request.Body = requestBodyStream;

                using (var responseBodyStream = new MemoryStream())
                {
                    context.Response.Body = responseBodyStream;

                    await _next(context);

                    await LogRequestAndResponseAsync(requestLogService, context, requestBodyStream, responseBodyStream, originalBodyStream);
                }
            }
        }
    }

    private async Task CopyRequestBodyAsync(Stream originalBody, MemoryStream requestBodyStream)
    {
        await originalBody.CopyToAsync(requestBodyStream);
        requestBodyStream.Seek(0, SeekOrigin.Begin);
    }

    private async Task LogRequestAndResponseAsync(IHttpLogService requestLogService, HttpContext context, MemoryStream requestBodyStream, MemoryStream responseBodyStream, Stream originalBodyStream)
    {
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        await responseBodyStream.CopyToAsync(originalBodyStream);

        requestBodyStream.Seek(0, SeekOrigin.Begin);
        var requestBody = await new StreamReader(requestBodyStream).ReadToEndAsync();

        var httpLog = new HttpLogDto
        {
            Method = context.Request.Method,
            Path = context.Request.Path,
            RequestBody = requestBody,
            ResponseBody = responseBody
        };

        await requestLogService.LogAsync(httpLog);
    }
}
