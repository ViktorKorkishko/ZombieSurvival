using System;
using Zenject;

namespace Core.Lifetime
{
    public abstract class SelfInitializableModel : IInitializable, IDisposable
    {
        public bool Initialized { get; protected set; }
        
        public abstract void Initialize();
        
        public virtual void Dispose() { }
    }
}
