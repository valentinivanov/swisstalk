using Swisstalk.Foundation.Runnable;

namespace Swisstalk.Core.Blocks.Notifications.NotificationQueue
{
    internal class NotificationSendStatement : ISingleArgStatement<NotificationItem>
    {
        public void Execute(NotificationItem obj)
        {
            obj.Topic.Send(obj.Id, obj.Args);
        }
    }
}
