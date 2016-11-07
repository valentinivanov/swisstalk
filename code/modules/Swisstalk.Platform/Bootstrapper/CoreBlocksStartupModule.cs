using System;
using Swisstalk.Core.Blocks.Aspect;
using Swisstalk.Core.Blocks.CompletionTracking;
using Swisstalk.Core.Blocks.Execution;
using Swisstalk.Core.Blocks.MetaScript;
using Swisstalk.Core.Blocks.Notifications;
using Swisstalk.Core.Blocks.Scope;
using Swisstalk.Core.Blocks.Support.Discovery;
using Swisstalk.Core.Blocks.Support.Discovery.Aspect;
using Swisstalk.Core.Blocks.Timer;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Platform.Bootstrapper
{
	public class CoreBlocksStartupModule
	{
		private ActiveObjectResolutionContext _coreActiveContext;
		private SuspendableResolutionContext _coreSuspendableContext;
		private DisposableResolutionContext _coreDisposableContext;

		private IBlockResolutionContext _resolutionContext;

		public CoreBlocksStartupModule()
		{
			_coreActiveContext = new ActiveObjectResolutionContext(new BlockResolutionContext());
			_coreSuspendableContext = new SuspendableResolutionContext(_coreActiveContext);
			_coreDisposableContext = new DisposableResolutionContext(_coreSuspendableContext);

			//topmost wrapper here
			_resolutionContext = _coreDisposableContext;
		}

		public void Start()
		{
			NotificationBus notificationBlock = new NotificationBus(CoreBlockIds.Notification);
			Executor executorBlock = new Executor(CoreBlockIds.Executor);
			AspectStorage aspectBlock = new AspectStorage(CoreBlockIds.Aspect);
			ScopeStorage scopeBlock = new ScopeStorage(CoreBlockIds.Scope);
			CompletionTracker completionTrackerBlock = new CompletionTracker(CoreBlockIds.CompletionTracker, executorBlock);
			AlarmClock alarmClockBlock = new AlarmClock(CoreBlockIds.AlarmClock, executorBlock);
			MetaScriptEngine metaScriptEngine = new MetaScriptEngine(CoreBlockIds.Animation);

			_resolutionContext.Append(notificationBlock);
			_resolutionContext.Append(executorBlock);
			_resolutionContext.Append(aspectBlock);
			_resolutionContext.Append(scopeBlock);
			_resolutionContext.Append(completionTrackerBlock);
			_resolutionContext.Append(alarmClockBlock);
			_resolutionContext.Append(metaScriptEngine);

			BlockProvider.Registrar.Append(_resolutionContext);
		}

		public IActiveObject ActiveServices
		{
			get
			{
				return _coreActiveContext.CompositeObject;
			}
		}

		public ISuspendable SuspendableServices
		{
			get
			{
				return _coreSuspendableContext.CompositeObject;
			}
		}

		public IDisposable DisposableServices
		{
			get
			{
				return _coreDisposableContext.CompositeObject;
			}
		}
	}
}
