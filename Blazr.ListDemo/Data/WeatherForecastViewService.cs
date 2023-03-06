namespace Blazr.ListDemo.Data;

public class WeatherForecastViewService : IListViewService<WeatherForecast>, IPagingService
{
    private WeatherForecastService _weatherForecastService;

    public ListState ListState { get; private set; } = new ListState();

    public event EventHandler? ListChanged;
    public event EventHandler<PagingEventArgs>? PageChanged;

    public IEnumerable<WeatherForecast> Items { get; private set; } = Enumerable.Empty<WeatherForecast>();

    public WeatherForecastViewService(WeatherForecastService weatherForecastService)
        => _weatherForecastService= weatherForecastService;

    private async ValueTask GetItemsAsync()
    {
        var result = await _weatherForecastService.GetForecastsAsync(this.ListState.request);
        if (result.Succcesful)
        {
            this.Items = result.Items;
            this.ListState.TotalCount = result.TotalCount;
            this.NotifyListChanged(this);
        }
    }

    public void NotifyListChanged(object? sender)
        => ListChanged?.Invoke(sender, EventArgs.Empty);

    public async void NotifyPageChanged(object? sender, PagingEventArgs e)
    {
        this.ListState.Page = e.Page;
        await this.GetItemsAsync();
        PageChanged?.Invoke(sender, e);
    }
}
