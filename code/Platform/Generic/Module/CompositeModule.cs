using Swisstalk.Foundation.Composition;
using System.Collections.Generic;

namespace Swisstalk.Platform.Generic.Module
{
    public class CompositeModule : ReadOnlyCompositeObject<IModule>, IModule
    {
        public CompositeModule(IEnumerable<IModule> objects) : base(objects)
        {
        }

        public CompositeModule(params IModule[] objects) : base(objects)
        {
        }

        public void Activate()
        {
            foreach (IModule module in Composite)
            {
                module.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (IModule module in Composite)
            {
                module.Deactivate();
            }
        }
    }
}
