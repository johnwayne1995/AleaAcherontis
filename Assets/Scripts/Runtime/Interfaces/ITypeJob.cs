using System.Collections.Generic;
using Managers;
namespace Interfaces
{
    public interface ITypeJob
    {
        void SetDependenies(ref List<DependentJob> jobs);
    }
}
