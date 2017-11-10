using System;

namespace Swisstalk.Foundation.Metadata
{
    public interface IActiveObject
    {
        void Update(TimeSpan delta);
    }
}
