using Swisstalk.Core.Blocks.Support.Discovery.Metadata;
using Swisstalk.Core.Blocks.Notifications.Metadata;

namespace Swisstalk.Core.Blocks.Notifications
{
	public interface INotificationBus : IBlock
	{
		INotificationTopic GetTopic(TopicId topicId);
	}
}
