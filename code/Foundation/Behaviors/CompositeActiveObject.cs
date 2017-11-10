using Swisstalk.Foundation.Composition;
using Swisstalk.Foundation.Metadata;
using System;
using System.Collections.Generic;

namespace Swisstalk.Foundation.Behaviors.ActiveObject
{
    public class CompositeActiveObject : CompositeObject<IActiveObject>, IActiveObject
    {
        public CompositeActiveObject() : base()
        {
        }

        public CompositeActiveObject(IEnumerable<IActiveObject> objects) : base(objects)
        {
        }

        public CompositeActiveObject(params IActiveObject[] objects) : base(objects)
        {
        }

        public void Update(TimeSpan delta)
        {
            foreach (var activeObject in Composite)
            {
                activeObject.Update(delta);
            }
        }
    }
}
