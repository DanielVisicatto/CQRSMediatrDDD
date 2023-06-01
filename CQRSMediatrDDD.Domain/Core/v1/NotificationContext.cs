using CQRSMediatrDDD.Domain.Contracts.v1;
using CQRSMediatrDDD.Domain.ValueObjects.v1;

namespace CQRSMediatrDDD.Domain.Core.v1;

public class NotificationContext : INotificationContext
{
    private readonly List<Notification> _notifications;

    public NotificationContext()
    {
        _notifications = new List<Notification>();
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;
    public bool HasNotifications => _notifications.Any();

    public void AddNotification(string notification)
    {
        _notifications.Add(new Notification(notification));
    }
}
