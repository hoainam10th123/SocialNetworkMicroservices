

namespace EventBus.Messages.Events
{
    public class NotificationEvent : IntegrationBaseEvent
    {
        public string UserNameNhan { get; set; } = null!;
        public string UserNameComment { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string PostId { get; set; } = null!;
    }
}
