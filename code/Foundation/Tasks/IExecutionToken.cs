using System;

namespace Swisstalk.Foundation.Tasks
{
    public interface IExecutionToken
    {
        TokenState State { get; }

        void Cancel();
    }
}

