using System.Collections.Generic;

namespace Penpusher.Services.Base
{
    abstract class ServiceBase<T>
    {
        public abstract IEnumerable<T> Find(string value);
    }
}