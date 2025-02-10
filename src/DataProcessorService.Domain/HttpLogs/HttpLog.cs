namespace DataProcessorService.Domain.HttpLogs;

public class HttpLog
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название метода
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// Путь запроса
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    public string RequestBody { get; set; }

    /// <summary>
    /// Тело ответа
    /// </summary>
    public string ResponseBody { get; set; }

    /// <summary>
    /// Время выполенния
    /// </summary>
    public DateTime Time { get; set; }
}