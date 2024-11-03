using System;
namespace Managers
{
    [Serializable]
    public class PokerCard : CardBase
    {
        public string id;
        public int basePoint;
        
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
    }
}
