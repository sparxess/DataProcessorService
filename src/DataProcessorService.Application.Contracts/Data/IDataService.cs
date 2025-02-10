namespace DataProcessorService.Application.Contracts.Data;

public interface IDataService
{
    /// <summary>
    /// Сохранение данных
    /// </summary>
    /// <param name="items">список данных</param>
    /// <returns></returns>
    Task SaveItemsAsync(DataItemsRequestDto items);

    /// <summary>
    /// Получение данных
    /// </summary>
    /// <param name="input">входной параметр с значениями фильтров</param>
    /// <returns></returns>
    Task<DataItemsResponseDto> GetItemsAsync(GetDataItemsInput? input);
}