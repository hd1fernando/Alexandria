using Alexandria.Bussiness.Interfaces.Notifications;

namespace Alexandria.Bussiness.Notifications;

public class Notifier : INotifier
{
    private IList<Notification> _notifications;
    public Notifier()
        => _notifications = new List<Notification>();

    public IList<Notification> GetNotifications()
        => _notifications;

    public void Handler(Notification notification)
        => _notifications.Add(notification);

    public bool HasNoNotification()
        => HasNoNotification() == false;

    public bool HasNotification()
        => _notifications.Any();
}
