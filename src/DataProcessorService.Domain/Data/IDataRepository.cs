namespace DataProcessorService.Domain.Data;

public interface IDataRepository
{
    /// <summary>
    /// Сохранение списка данных
    /// </summary>
    /// <param name="items">данные</param>
    /// <returns></returns>
    Task SaveItemsAsync(IList<DataItem> items);

    /// <summary>
    /// Получение списка данных
    /// </summary>
    /// <param name="code">код</param>
    /// <returns></returns>
    Task<IList<DataItem>> GetItemsAsync(int? code);
}