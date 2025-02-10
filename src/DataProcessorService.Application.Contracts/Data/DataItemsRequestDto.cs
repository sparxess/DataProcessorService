namespace DataProcessorService.Application.Contracts.Data;

public class DataItemsRequestDto
{
    /// <summary>
    /// Коллекция входных данных для сохранения
    /// </summary>
    public IDictionary<int, string> Items { get; set; }
}