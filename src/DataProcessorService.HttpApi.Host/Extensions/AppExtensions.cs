using DataProcessorService.HttpApi.Host.Middleware;

namespace DataProcessorService.HttpApi.Host.Extensions;

public static class AppExtensions
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<HttpLoggingMiddleware>();
    }

    public static void ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
