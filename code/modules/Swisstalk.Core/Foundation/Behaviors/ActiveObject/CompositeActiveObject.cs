using System;
using Swisstalk.Foundation.Composition;
using Swisstalk.Foundation.Metadata.Reflection;

namespace Swisstalk.Foundation.Behaviors.ActiveObject
{
    public class CompositeActiveObject : UniqueReversibleComposition<IActiveObject>, IActiveObject
    {
        public void Update(TimeSpan delta)
        {
            foreach (var activeObject in Composite)
            {
                activeObject.Update(delta);
            }
        }
    }
}
