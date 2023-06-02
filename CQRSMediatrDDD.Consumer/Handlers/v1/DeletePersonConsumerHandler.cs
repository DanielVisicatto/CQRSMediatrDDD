using CQRSMediatrDDD.Domain.Commands.v1.DeletePerson;
using CQRSMediatrDDD.Domain.Contracts.v1;
using EasyNetQ;

namespace CQRSMediatrDDD.Consumer.Handlers.v1;

public class DeletePersonConsumerHandler : BackgroundService
{
    private readonly IBus _bus;

    private readonly IPersonRepository _repository;

    public DeletePersonConsumerHandler(IPersonRepository repository, IBus bus)
    {
        _repository = repository;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.SendReceive.ReceiveAsync<DeletepersonCommand>("delete-person-queue",
            async command => { await _repository.RemoveAsync(command.Id, stoppingToken); }).ConfigureAwait(false);
    }
}
