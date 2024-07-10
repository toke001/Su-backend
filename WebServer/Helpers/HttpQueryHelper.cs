using WebServer.Dtos;

namespace WebServer.Helpers
{
    public static class HttpQueryHelper
    {
        public static void FromQuery(this HttpContext context)
        {
            int page = 1;
            int size = 20;
            string filter = string.Empty;
            if (context.Request.QueryString.HasValue)
            {
                var query = context.Request.QueryString.Value;
                var queryDic = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(query);

                if (queryDic != null)
                {
                    bool pageHasVal = queryDic.TryGetValue("page", out var _page);
                    if (pageHasVal) int.TryParse(_page, out page);

                    bool sizeHasVal = queryDic.TryGetValue("size", out var _size);
                    if (sizeHasVal) int.TryParse(_size, out size);

                    bool filterHasVal = queryDic.TryGetValue("filter", out var _filter);
                    if (filterHasVal) filter = _filter;
                }
            }
            //return new PageQueryDto
            //{
            //    PageNumber = page,
            //    PageSize = size,
            //    Filter = filter
            //};
        }
    }
}
