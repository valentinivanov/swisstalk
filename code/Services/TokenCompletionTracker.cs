using Swisstalk.Foundation.Tasks;
using Swisstalk.Foundation.Tasks.Activity;
using System;

namespace Swisstalk.Services
{
    public delegate void OnTokenComplete(object cookie, IExecutionToken targetToken);

    public class TokenCompletionTracker
    {
        private class WaitExecutionTokenActivity : IActivity
        {
            private readonly IExecutionToken token;
            private readonly OnTokenComplete completionHandler;
            private readonly object cookie;

            public WaitExecutionTokenActivity(IExecutionToken token,
                                              OnTokenComplete completionHandler,
                                              object cookie)
            {
                this.token = token;
                this.completionHandler = completionHandler;
                this.cookie = cookie;
            }

            public void Start()
            {
            }

            public void Stop()
            {
                completionHandler(cookie, token);
            }

            public bool Update(TimeSpan delta)
            {
                return token.State.IsComplete();
            }

            public void Dispose()
            {
            }
        }

        private readonly IExecutor executor;

        public TokenCompletionTracker(IExecutor executor)
        {
            this.executor = executor;
        }

        public IExecutionToken Track(IExecutionToken targetToken, 
                                     OnTokenComplete completionHandler, 
                                     object cookie)
        {
            return executor.Execute(new WaitExecutionTokenActivity(targetToken, completionHandler, cookie));
        }

        public IExecutionToken Track(IExecutionToken targetToken,
                                     OnTokenComplete completionHandler)
        {
            return Track(targetToken, completionHandler, null);
        }
    }
}
