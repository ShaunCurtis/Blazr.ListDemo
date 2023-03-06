namespace Blazr.ListDemo;

public record ItemsQueryRequest(int StartIndex, int PageSize);

public record ItemsQueryResult<TRecord>(IEnumerable<TRecord> Items, int TotalCount, bool Succcesful, string? Message = null) where TRecord : class;

public class ListState
{
    public int PageSize { get; set; } = 10;
    public int Page { get; set; } = 0;
    public int StartIndex => this.Page * this.PageSize;
    public int LastPage => ((int)Math.Ceiling(((decimal)this.TotalCount / this.PageSize))) - 1;
    public ItemsQueryRequest request => new ItemsQueryRequest(this.StartIndex, this.PageSize);
    public int TotalCount { get; set; } = 0;
}
