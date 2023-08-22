using Flunt.Notifications;

namespace Escalas.Application.Models;
public class ErrorModel
{
    public ErrorModel(IReadOnlyCollection<Notification> notifications)
    {
        Errors.AddRange(notifications.Select(x => x.Message));
    }
    public List<string> Errors { get; } = new();
}
