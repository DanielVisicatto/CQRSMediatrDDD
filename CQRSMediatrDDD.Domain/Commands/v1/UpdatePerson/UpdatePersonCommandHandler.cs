using AutoMapper;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Domain.Core.v1.Entities.v1;
using EasyNetQ;

namespace CQRSMediatrDDD.Domain.Commands.v1.UpdatePerson;

public class UpdatePersonCommandHandler : BaseHandler
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _repository;

    public UpdatePersonCommandHandler(
        INotificationContext notificationContext,
        IPersonRepository repository,
        IBus bus,
        IMapper mapper) : base(notificationContext)
    {
        _repository = repository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task HandleAsync(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var dataBaseEntity = await _repository.FindByIdAsync(command.Id, cancellationToken);
        if(dataBaseEntity is null)
        {
            NotificationContext.AddNotification($"Person with id = {command.Id} does not exist.");
            return;
        }

        var entity = _mapper.Map<Person>(command);
        await _bus.SendReceive.SendAsync("update-person-queue", entity, cancellationToken);
    }
}