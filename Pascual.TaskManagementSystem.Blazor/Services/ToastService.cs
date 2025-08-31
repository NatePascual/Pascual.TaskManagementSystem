namespace Pascual.TaskManagementSystem.Blazor.Services;

public enum ToastLevel { Success, Info, Warning, Error }

public sealed class ToastMessage
{
    public Guid Id { get; } = Guid.NewGuid();
    public ToastLevel Level { get; init; } = ToastLevel.Info;
    public string Title { get; init; } = "";
    public string Message { get; init; } = "";
    public int AutoCloseMs { get; init; } = 3000;

    public class ToastService
    {
        public event Action<ToastMessage>? OnShow;
        public event Action<Guid>? OnHide;

        public void Show(ToastLevel level, string message, string? title = null, int autoCloseMs = 3000)
            => OnShow?.Invoke(new ToastMessage { Level = level, Message = message, Title = title ?? level.ToString(), AutoCloseMs = autoCloseMs });

        public void ShowSuccess(string message, string title = "Success", int autoCloseMs = 3000) => Show(ToastLevel.Success, message, title, autoCloseMs);
        public void ShowInfo(string message, string title = "Info", int autoCloseMs = 3000) => Show(ToastLevel.Info, message, title, autoCloseMs);
        public void ShowWarning(string message, string title = "Warning", int autoCloseMs = 4000) => Show(ToastLevel.Warning, message, title, autoCloseMs);
        public void ShowError(string message, string title = "Error", int autoCloseMs = 5000) => Show(ToastLevel.Error, message, title, autoCloseMs);

        public void Hide(Guid id) => OnHide?.Invoke(id);
    }
}