namespace Blazr.ListDemo.Data;

public class WeatherForecastService
{
    private List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();

    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastService()
    {
        _weatherForecasts = GetForecastsAsync();
    }

    public async ValueTask<ItemsQueryResult<WeatherForecast>> GetForecastsAsync(ItemsQueryRequest request)
    {
        // this would normally get an `IQueryable` object from the DbContext
        // so I've set it as the In-Memory equivalent - an IEnumerable object
        var query = _weatherForecasts.AsEnumerable();

        // materialize the query to get the total count
        var count = query.Count();

        // add the paging to the query
        query = query
            .Skip(request.StartIndex)
            .Take(request.PageSize);

        // materialize the query into a List object as we would do in a DBContext query
        var items = query.ToList();

        // pretend to be an async DB call
        await Task.Yield();

        // build an ItemsQueryResult
        return new ItemsQueryResult<WeatherForecast>(items, count, true);
    }

    private List<WeatherForecast> GetForecastsAsync()
    {
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
        return Enumerable.Range(1, 252).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToList();
    }
}
