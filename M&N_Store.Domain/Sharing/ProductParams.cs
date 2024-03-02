namespace M_N_Store.Domain.Sharing
{
    public class ProductParams
    {
        public int MaxPageSize { get; set; } = 15;
        private int _pageSize = 3;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageNumber { get; set; }
        public string Sort { get; set; }
        public int? CategoryId { get; set; }

        private string _search;
        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }
    }
}
