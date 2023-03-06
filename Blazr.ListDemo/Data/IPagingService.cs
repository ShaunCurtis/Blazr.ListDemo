namespace Blazr.ListDemo.Data;

public interface IPagingService
{
    public event EventHandler<PagingEventArgs>? PageChanged;

    public ListState ListState { get; }

    public void NotifyPageChanged(object? sender, PagingEventArgs e);
}
