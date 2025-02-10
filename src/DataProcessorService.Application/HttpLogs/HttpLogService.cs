using AutoMapper;
using DataProcessorService.Application.Contracts.HttpLogs;
using DataProcessorService.Domain.HttpLogs;

namespace DataProcessorService.Application.HttpLogs;

public class HttpLogService : IHttpLogService
{
    private readonly IHttpLogRepository _requestLogRepository;
    private readonly IMapper _mapper;

    public HttpLogService(IHttpLogRepository requestLogRepository,
        IMapper mapper)
    {
        _requestLogRepository = requestLogRepository;
        _mapper = mapper;
    }

    public async Task LogAsync(HttpLogDto httpLog)
    {
        var log = _mapper.Map<HttpLog>(httpLog);

        await _requestLogRepository.AddAsync(log);
    }
}