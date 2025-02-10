namespace DataProcessorService.Application.Contracts.HttpLogs;

public class HttpLogDto
{
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
}