using AutoMapper;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Domain.Entities.v1;

namespace CQRSMediatrDDD.Domain.Commands.v1.CreatePerson;

public class CreatePersonCommandHandler : BaseHandler
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public async Task<Guid> HandleAsync (CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var currentPerson = await _repository.FindByDocumentAsync(command.Cpf, cancellationToken);
        if(currentPerson is not null) return Guid.Empty;

        var entity = _mapper.Map<Person>(command);
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}