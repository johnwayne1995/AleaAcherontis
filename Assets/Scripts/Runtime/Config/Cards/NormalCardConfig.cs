using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Config
{
    public class PokerCardsConfig : ScriptableObject
    {
        public List<PokerCard> normalCards = new List<PokerCard>();
    }
}
