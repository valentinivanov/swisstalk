using System;

namespace Swisstalk.Foundation.Metadata.Reflection
{
    public interface IActiveObject
    {
        void Update(TimeSpan delta);
    }
}
