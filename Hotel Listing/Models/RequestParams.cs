namespace Hotel_Listing.Models
{
    public class RequestParams
    {
        const int maxPagesize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > maxPagesize ? maxPagesize : value;
            }
        }
    }
}
