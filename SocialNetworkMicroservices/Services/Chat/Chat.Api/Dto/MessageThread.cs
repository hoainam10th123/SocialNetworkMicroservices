namespace Chat.Api.Dto
{
    public class MessageThread
    {
        public MessageThread(int totalItems, int pageSize, List<MessageDto> items)
        {
            TotalPage = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            PageSize = pageSize;
            Messages = items;
        }
        public int TotalPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
