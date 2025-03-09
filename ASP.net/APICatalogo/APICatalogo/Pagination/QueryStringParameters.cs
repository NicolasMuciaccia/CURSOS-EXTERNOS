namespace APICatalogo.Pagination
{
    public abstract class QueryStringParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _PageSize = maxPageSize;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
