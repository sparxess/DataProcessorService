namespace DataProcessorService.Application.Contracts.HttpLogs;

public interface IHttpLogService
{
    /// <summary>
    /// Логгирование http запросов
    /// </summary>
    /// <param name="httpLog">лог</param>
    /// <returns></returns>
    Task LogAsync(HttpLogDto httpLog);
}