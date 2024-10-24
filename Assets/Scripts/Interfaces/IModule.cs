namespace Interfaces
{
    public interface IModule
    {
        void Start();

        void Dispose();

        void Update();

        void LateUpdate();
        
        void FixedUpdate();

        void Pause();

        void Continue();
    }
}
