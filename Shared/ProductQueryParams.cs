namespace Shared
{
    public class ProductQueryParams
    {
        private const int DefalutPageSize = 5;
        private const int MaxPageSize = 10;

        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions sort { get; set; }
        public string? search { get; set; }
        public int pageNumber { get; set; } = 1;

        private int pageSize = DefalutPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
