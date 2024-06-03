namespace Core.Lifetime
{
    public abstract class InitializableModel
    {
        public bool Initialized { get; private set; }
        public abstract void Initialize();
    }

    public abstract class InitializableModel<T> where T : InitData
    {
        public bool Initialized { get; private set; }
        public abstract void Initialize(T initData);
    }
    
    public abstract class InitData { }
}
