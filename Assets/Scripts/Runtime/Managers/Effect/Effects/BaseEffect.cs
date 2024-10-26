using Interfaces;
namespace Managers.Effects
{
    public abstract class BaseEffect : IEffect
    {
        public virtual void StartEffect()
        {
        }
        
        public virtual void Dispose()
        {
        }
    }
}
