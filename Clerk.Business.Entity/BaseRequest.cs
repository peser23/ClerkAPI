namespace Clerk.Business.Entity
{

    public class SearchResult<T>
    {
        public T Items { get; set; }
        public int Count { get; set; }
    }

    public class BaseRequest
    {
        public string Guid { get; set; }
        public string Version { get; set; } = "v1";

    }

    public class SearchRequest : BaseRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string SearchText { get; set; }
    }
}
