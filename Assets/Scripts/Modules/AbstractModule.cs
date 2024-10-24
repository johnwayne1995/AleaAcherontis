using Interfaces;

namespace Modules
{
    public abstract class AbstractModule<T> : IModule where T : new()
    {
        public static T Instance = new T();

        public virtual bool NeedUpdate
        {
            get { return false; }
        }

        public virtual bool NeedFixedUpdate
        {
            get { return false; }
        }

        public virtual void Start()
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Pause()
        {
        }

        public virtual void Continue()
        {
        }

    }
}
