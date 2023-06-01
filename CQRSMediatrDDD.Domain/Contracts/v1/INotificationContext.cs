using CQRSMediatrDDD.Domain.ValueObjects.v1;

namespace CQRSMediatrDDD.Domain.Contracts.v1;

public interface INotificationContext
{
    IReadOnlyCollection<Notification> Notifications { get; }
    bool HasNotifications { get; }
    void AddNotification(string notification);
}
