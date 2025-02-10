using AutoMapper;
using DataProcessorService.Application.Contracts.Data;
using DataProcessorService.Application.Contracts.HttpLogs;
using DataProcessorService.Domain.Data;
using DataProcessorService.Domain.HttpLogs;

namespace DataProcessorService.Application.Mappers;

public class DataProcessorServiceApplicationAutoMapperProfile : Profile
{
    public DataProcessorServiceApplicationAutoMapperProfile()
    {
        MapData();
        MapHttpLog();
    }

    private void MapData()
    {
        CreateMap<DataItemsRequestDto, IList<DataItem>>()
            .ConvertUsing(src => src.Items.Select(x => new DataItem
            {
                Code = x.Key,
                Value = x.Value
            }).ToList());

        CreateMap<DataItem, DataItemResponseDto>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<IList<DataItem>, DataItemsResponseDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
    }

    private void MapHttpLog()
    {
        CreateMap<HttpLogDto, HttpLog>()
            .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method))
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path))
            .ForMember(dest => dest.RequestBody, opt => opt.MapFrom(src => src.RequestBody))
            .ForMember(dest => dest.ResponseBody, opt => opt.MapFrom(src => src.ResponseBody))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(_ => DateTime.Now));
    }
}
