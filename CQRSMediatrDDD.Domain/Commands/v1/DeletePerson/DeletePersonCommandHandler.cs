using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.Core.v1;
using EasyNetQ;

namespace CQRSMediatrDDD.Domain.Commands.v1.DeletePerson;

public class DeletePersonCommandHandler : BaseHandler
{
    private readonly IBus _bus;

    public DeletePersonCommandHandler(INotificationContext notificationContext, IBus bus) : base (notificationContext)
    {
        _bus = bus;
    }

    public async Task Handleasync(DeletepersonCommand command, CancellationToken cancellationToken)
    {
        await _bus.SendReceive.SendAsync("delete-person-queue", command, cancellationToken);
    }
}