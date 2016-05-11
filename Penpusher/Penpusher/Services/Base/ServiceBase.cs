using System.Collections.Generic;

namespace Penpusher.Services.Base
{
    public abstract class ServiceBase<T>
    {
        public abstract IEnumerable<T> Find(string value);
    }
}