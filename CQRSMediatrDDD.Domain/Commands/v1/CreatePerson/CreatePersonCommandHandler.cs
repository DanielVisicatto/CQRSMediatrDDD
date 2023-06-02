using AutoMapper;
using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using CQRSMediatrDDD.Domain.Entities.v1;
using EasyNetQ;

namespace CQRSMediatrDDD.Domain.Commands.v1.CreatePerson;

public class CreatePersonCommandHandler : BaseHandler
{
    private IBus _bus;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(INotificationContext notificationContext, IMapper mapper, IBus bus) : base (notificationContext)
    {
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<Guid> HandleAsync (CreatePersonCommand command, CancellationToken cancellationToken)
    {      
        var entity = _mapper.Map<Person>(command);
        await _bus.SendReceive.SendAsync("create-person-queue", entity, cancellationToken);
        return entity.Id;
    }
}