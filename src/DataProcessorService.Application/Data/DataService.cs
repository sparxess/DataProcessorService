using AutoMapper;
using DataProcessorService.Application.Contracts.Data;
using DataProcessorService.Domain.Data;

namespace DataProcessorService.Application.Values;

public class DataService : IDataService
{
    private readonly IDataRepository _dataRepository;
    private readonly IMapper _mapper;

    public DataService(IDataRepository dataRepository,
        IMapper mapper)
    {
        _dataRepository = dataRepository;
        _mapper = mapper;
    }

    public async Task SaveItemsAsync(DataItemsRequestDto dataItems)
    {
        var items = _mapper.Map<IList<DataItem>>(dataItems);

        items = items.OrderBy(x => x.Code).ToList();

        await _dataRepository.SaveItemsAsync(items);
    }

    public async Task<DataItemsResponseDto> GetItemsAsync(GetDataItemsInput? input)
    {
        var items = await _dataRepository.GetItemsAsync(input?.Code);

        var dataItems = _mapper.Map<DataItemsResponseDto>(items);

        return dataItems;
    }
}
