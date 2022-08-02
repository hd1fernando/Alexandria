using Alexandria.Bussiness.Entitties;
using Alexandria.Bussiness.Interfaces.Notifications;
using Alexandria.Bussiness.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Alexandria.Bussiness.Services;
public abstract class BaseService
{
    private readonly INotifier _notifier;

    public BaseService(INotifier notifier)
        => _notifier = notifier;

    protected void SendNotification(string message)
        => _notifier.Handler(new Notification(message));

    protected void SendNotification(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
            SendNotification(error.ErrorMessage);
    }

    protected bool DoValidation<TValidation, TEntity, TKey>(TValidation validation, TEntity entity)
        where TValidation : AbstractValidator<TEntity>
        where TEntity : Entity<TKey>
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid)
            return true;

        SendNotification(validator);
        return false;
    }

    protected async Task<bool> DoValidationAsync<TValidation, TEntity, TKey>(TValidation validation, TEntity entity)
        where TValidation : AbstractValidator<TEntity>
        where TEntity : Entity<TKey>
    {
        var validator = await validation.ValidateAsync(entity);

        if (validator.IsValid)
            return true;

        SendNotification(validator);
        return false;
    }
}