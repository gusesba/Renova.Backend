namespace Renova.Service.Config
{
    public abstract class BasePaginatedRequest<TResponse> : BaseRequest<TResponse>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; } = "asc";
    }
}
