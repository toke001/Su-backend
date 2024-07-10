namespace WebServer.Dtos
{
    public class PageResultDto<T> : PageQueryDto
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T?> Items { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        //public List<string>? Errors { get; set; }

        public PageResultDto(int totalCount, IReadOnlyList<T> items, int pageNumber, int pageSize, string filter) : base()
        {
            TotalCount = totalCount;
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            //Filter = filter;
            //Errors = errors;
        }
    }
}
