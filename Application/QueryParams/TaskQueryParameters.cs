using Domain.Enums;

namespace Application.QueryParams
{
    public class TaskQueryParameters
    {
        public TaskMgmtStatus? Status { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? TeamId { get; set; }
        public DateTime? DueDate { get; set; }

        // Pagination defaults
        private int _page = 1;
        private int _pageSize = 10;

        public int Page
        {
            get => _page;
            set => _page = (value < 1) ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : (value < 1 ? 1 : value);
        }
        private const int MaxPageSize = 50;
    }
}