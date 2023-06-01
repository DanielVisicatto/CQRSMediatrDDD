using CQRSMediatrDDD.Domain.Contracts.v1;

namespace CQRSMediatrDDD.Domain.Core.v1;

public abstract class BaseHandler
{
    protected readonly INotificationContext NotificationContext;

    protected BaseHandler(INotificationContext notificationContext)
    {
        NotificationContext = notificationContext;
    }
}