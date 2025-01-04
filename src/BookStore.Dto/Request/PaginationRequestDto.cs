namespace BookStore.Dto.Request
{
    public class PaginationRequestDto
    {
		private readonly int MaxRecordsPerPage = 50;
		public int Page { get; set; } = 1;
		private int RecordsPerPage_ = 10;

		public int RecordsPerPage
		{
			get { return RecordsPerPage_; }
			set { RecordsPerPage_ = (value > MaxRecordsPerPage) ? MaxRecordsPerPage : value; }
		}

	}
}
