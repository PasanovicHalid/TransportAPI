namespace Presentation.Contracts.Common
{
    public class PaginatedResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public List<T> Items { get; set; } = new();
        public PaginatedResponse(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
