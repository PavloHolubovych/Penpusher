using System.Collections.Generic;

namespace Penpusher
{
    abstract class ServiceBase<T>
    {
        public abstract IEnumerable<T> Find(string value);
    }
}