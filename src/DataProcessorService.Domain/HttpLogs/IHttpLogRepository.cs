
namespace DataProcessorService.Domain.HttpLogs;

public interface IHttpLogRepository
{
    /// <summary>
    /// Добавление информации о запросах и ответов http методов
    /// </summary>
    /// <param name="log">лог запроса</param>
    /// <returns></returns>
    Task AddAsync(HttpLog log);
}