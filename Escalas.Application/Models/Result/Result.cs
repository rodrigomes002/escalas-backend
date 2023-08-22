using Flunt.Notifications;

namespace Escalas.Application.Models.Result;

public class Result<T> : Notifiable<Notification>
{
    private Result(T @object)
    {
        Object = @object;
    }

    private Result(IReadOnlyCollection<Notification> notifications, bool notFound = false)
    {
        Object = default!;
        Notfound = notFound;
        AddNotifications(notifications);
    }

    public bool Success => !Notifications.Any();
    public bool Notfound { get; set; }
    public T Object { get; }

    public static Result<T> Ok(T @object)
    {
        return new Result<T>(@object);
    }

    public static Result<T> Error(IReadOnlyCollection<Notification> notifications)
    {
        return new Result<T>(notifications);
    }

    public static Result<T> Error(string message)
    {
        return new Result<T>(new List<Notification> { new() { Message = message } });
    }

    public static Result<T> NotFoundResult()
    {
        return new Result<T>(new List<Notification>(), true);
    }
}