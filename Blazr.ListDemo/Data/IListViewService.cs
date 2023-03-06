namespace Blazr.ListDemo.Data;

public interface IListViewService<TRecord> : IPagingService
{
    public event EventHandler? ListChanged;

    public IEnumerable<TRecord> Items { get; }

    public void NotifyListChanged(object? sender);
}
