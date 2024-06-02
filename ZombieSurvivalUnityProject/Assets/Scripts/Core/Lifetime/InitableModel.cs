namespace Core.Lifetime
{
    public abstract class InitableModel
    {
        public bool Inited { get; private set; }

        public abstract void Initialize();
    }
}
