using Interfaces;
namespace Managers.Effects
{
    public abstract class BuffBase : IEffect
    {
        public virtual void StartEffect()
        {
        }
        
        public virtual void Dispose()
        {
        }
    }
}
