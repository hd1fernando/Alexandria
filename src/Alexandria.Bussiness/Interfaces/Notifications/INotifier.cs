using Alexandria.Bussiness.Notifications;

namespace Alexandria.Bussiness.Interfaces.Notifications;
public interface INotifier
{
    public bool HasNotification();
    public bool HasNoNotification();
    public IList<Notification> GetNotifications();
    public void Handler(Notification notification);
}
