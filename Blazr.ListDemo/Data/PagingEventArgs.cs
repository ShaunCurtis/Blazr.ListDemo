namespace Blazr.ListDemo.Data
{
    public class PagingEventArgs : EventArgs
    {
        public int Page { get; set; }

        public PagingEventArgs(int page)
        {
            Page = page;
        }
    }
}
