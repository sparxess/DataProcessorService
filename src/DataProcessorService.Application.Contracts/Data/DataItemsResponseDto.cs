namespace DataProcessorService.Application.Contracts.Data;

public class DataItemsResponseDto
{
    /// <summary>
    /// Список данных для получения
    /// </summary>
    public IList<DataItemResponseDto> Items { get; set; }
}
