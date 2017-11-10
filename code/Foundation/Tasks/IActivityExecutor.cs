using Swisstalk.Foundation.Tasks.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swisstalk.Foundation.Tasks
{
    public interface IActivityExecutor
    {
        IExecutionToken Execute(IActivity activity);
    }
}
