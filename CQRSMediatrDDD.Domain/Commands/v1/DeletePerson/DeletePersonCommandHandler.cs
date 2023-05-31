using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;

namespace CQRSMediatrDDD.Domain.Commands.v1.DeletePerson;

public class DeletePersonCommandHandler : BaseHandler
{
    private readonly IPersonRepository _repository;

    public DeletePersonCommandHandler(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task Handleasync(DeletepersonCommand command, CancellationToken cancellationToken)
    {
        await _repository.RemoveAsync(command.Id, cancellationToken);
    }
}
