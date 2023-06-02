using AutoMapper;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Domain.Entities.v1;
using MediatR;

namespace CQRSMediatrDDD.Domain.Queries.v1.GetPerson;

public class GetPersonQueryHandler : BaseHandler, IRequestHandler<GetPersonQuery, GetPersonQueryResponse>
{
    private readonly ICacheRepository<Person> _cacheRepository;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public GetPersonQueryHandler(INotificationContext notificationContext, IMapper mapper, IPersonRepository repository,ICacheRepository<Person> cacheRepository)
        : base (notificationContext)
    {
        _repository = repository;
        _mapper = mapper;
        _cacheRepository = cacheRepository;
    }

    public async Task <GetPersonQueryResponse?> Handle(GetPersonQuery command, CancellationToken cancellationToken)
    {
        var cachedEntity = await _cacheRepository.GetAsync(command.Id.ToString());
        if(cachedEntity != null) return _mapper.Map<GetPersonQueryResponse?>(cachedEntity);

        var databaseEntity = await _repository.FindByIdAsync(command.Id, cancellationToken);
        if(databaseEntity is not null) return _mapper.Map<GetPersonQueryResponse?>(databaseEntity);

        //TODO $"Person with id = {command.Id} does not exist."
        return null;
    }
}