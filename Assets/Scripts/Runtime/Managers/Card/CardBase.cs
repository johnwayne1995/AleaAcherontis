using System;
namespace Managers
{
    [Serializable]
    public class CardBase
    {
        public Guid guid;
        public string name;
        public string iconPath;
        public string bgPath;
    }
}
