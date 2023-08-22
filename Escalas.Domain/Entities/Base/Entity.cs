using Flunt.Notifications;

namespace Escalas.Domain.Entities.Base;

public class Entity<T> : Notifiable<Notification>
{
    public T Id { get; } = default!;
}
