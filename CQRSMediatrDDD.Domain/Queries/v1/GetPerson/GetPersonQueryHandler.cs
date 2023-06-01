using AutoMapper;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;

namespace CQRSMediatrDDD.Domain.Queries.v1.GetPerson;

public class GetPersonQueryHandler : BaseHandler
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public GetPersonQueryHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task <GetPersonQueryResponse?> HandleAsync(GetPersonQuery command, CancellationToken cancellationToken)
    {
        var databaseEntity = await _repository.FindByIdAsync(command.Id, cancellationToken);

        if(databaseEntity is not null) return _mapper.Map<GetPersonQueryResponse?>(databaseEntity);

        //TODO $"Person with id = {command.Id} does not exist."
        return null;
    }
}