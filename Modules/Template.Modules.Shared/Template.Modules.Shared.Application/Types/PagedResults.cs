namespace Template.Modules.Shared.Application.Types
{
    public class PagedResults<T> where T : new()
    {
        public int Skip { get; }
        public int Rows { get; }
        public int Total { get; }
        public List<T> Data { get; }

        public PagedResults(int skip, int rows, int total, List<T> data)
        {
            Skip = skip;
            Rows = rows;
            Total = total;
            Data = data;
        }
    }
}
