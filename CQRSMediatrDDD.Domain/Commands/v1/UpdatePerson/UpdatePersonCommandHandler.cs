using AutoMapper;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Domain.Entities.v1;
using CQRSMediatrDDD.Domain.ValueObjects.v1;

namespace CQRSMediatrDDD.Domain.Commands.v1.UpdatePerson;

internal class UpdatePersonCommandHandler : BaseHandler
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public UpdatePersonCommandHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> HandleAsync(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var currentPerson = await _repository.FindByIdAsync(command.Id, cancellationToken);

        if (currentPerson is null) return Guid.Empty;

        var personUpdates = new Person(
            currentPerson.Id,
            new Name(command.Name),
            new Document(command.Cpf),
            new Email(command.Email),
            command.BirthDate,
            currentPerson.CreatedAt);

        await _repository.UpdateAsync(personUpdates, cancellationToken);
        return currentPerson.Id;
    }
}