namespace DataProcessorService.Application.Contracts.Data;

public class DataItemResponseDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Код
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; set; }
}
