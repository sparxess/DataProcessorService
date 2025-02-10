namespace DataProcessorService.Application.Contracts.Data;

/// <summary>
/// Входные данные для фильтрации
/// </summary>
public class GetDataItemsInput
{
    /// <summary>
    /// Код
    /// </summary>
    public int? Code { get; set; }
}