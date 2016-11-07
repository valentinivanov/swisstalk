using Swisstalk.Core.Blocks.Support.Discovery.Metadata;

namespace Swisstalk.Platform.Bootstrapper
{
    public static class CoreBlockIds
    {
        public static BlockId Notification = new BlockId("Core Notifications Block Instance");
		public static BlockId AlarmClock = new BlockId("Core Alarm Clock Block Instance");
		public static BlockId Aspect = new BlockId("Core Aspect Block Instance");
		public static BlockId CompletionTracker = new BlockId("Core Completion Tracker Block Instance");
		public static BlockId Executor = new BlockId("Core Executor Block Instance");
		public static BlockId Scope = new BlockId("Core Scope Block Instance");
		public static BlockId Animation = new BlockId("Animation Player Instance");
    }
}
